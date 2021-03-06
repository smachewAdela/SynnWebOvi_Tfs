﻿using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class CreditStats : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if(!CurrentUser.IsAdmin)
                ichartcontainer.Visible = CurrentUser.Preferences.UseCharts;
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.CreditData };
                return l;
            }
        }

        private void RefreshView()
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { });
            if (ul.NotEmpty())
            {
                var inactiveItems = ul.Where(x => !x.Active).ToList();
                if (inactiveItems.NotEmpty())
                {
                    var avg = inactiveItems.Average(x => x.TotalSpent);
                    txp1.Text = avg.ToInteger().FormattedString();
                    txp2.Text = inactiveItems.Average(x => x.DaylyValue.ToInteger()).ToInteger().FormattedString();
                }

                var current = DBController.DbMoney.Get(new CreditSearchParameters { Month = DateTime.Now }).FirstOrDefault();
                if (current != null)
                    txp3.Text = current.MonthlyPrediction.ToInteger().FormattedString();
            }
            RefreshGrid(gv);
        }
        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCreditItems";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCreditItems()
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { });
            return ul.OrderByDescending(x => x.Date).ToList();
        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (CreditCardMonthlyData)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblMonthName")).Text = d.Date.HebrewMonthName();
                ((Label)e.Row.FindControl("lblDaylyValue")).Text = d.DaylyValue;
                ((Label)e.Row.FindControl("lblMonthlyPredictions")).Text = d.Active ? d.MonthlyPrediction : d.TotalSpent.FormattedString();
                var txCurrentTotal = (TextBox)e.Row.FindControl("txCurrentTotal");
                txCurrentTotal.Text = d.TotalSpent.FormattedString(); 
                txCurrentTotal.Visible = d.Active;

                var btnAction = ((ImageButton)e.Row.FindControl("btnAction"));
                var btnCloseMonth = ((ImageButton)e.Row.FindControl("btnCloseMonth"));
                btnAction.CommandArgument = btnCloseMonth.CommandArgument = d.Id.ToString();
                btnAction.Visible = btnCloseMonth.Visible = d.Active;
            }
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            CreditCardMonthlyData i = GetItem(e.CommandArgument);
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            var txCurrentTotal = (TextBox)row.FindControl("txCurrentTotal");
            var nVal = Convert.ToInt32(Convert.ToDecimal(txCurrentTotal.Text));
            if (i.Active)
            {
                i.TotalSpent = nVal;
                DBController.DbMoney.Update(i);
                RefreshGrid(gv);
            }
        }

        private CreditCardMonthlyData GetItem(object commandArgument)
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { Id = Convert.ToInt32(commandArgument) });
            return ul.FirstOrDefault();
        }

        protected void btnCloseMonth_Command(object sender, CommandEventArgs e)
        {
            CreditCardMonthlyData i = GetItem(e.CommandArgument);
            i.Active = false;
            DBController.DbMoney.Update(i);
            RefreshView();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        [WebMethod]
        public static List<object> GetChartData()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "חודש", "סך הוצאות חודשי" });

            var dbH = SynnDataProvider.DbProvider.DbMoney;
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { Active = false , FromDate = new DateTime(DateTime.Now.Year,1,1)});
            foreach (var item in ul.OrderBy(x => x.Date))
                chartData.Add(new object[] { item.Date.HebrewMonthName(), item.TotalSpent });

            return chartData;
        }
    }
}