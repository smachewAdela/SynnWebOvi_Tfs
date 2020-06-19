using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class QuickTasks : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrids();
            }
        }
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.QuickTasks };
                return l;
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            List<QuickTask> items = DBController.DbCalendar.Get(new QuickTasksSearchParameters {  Active = true });
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (QuickTask)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblTaskName")).Text = d.Name;
                ((Label)e.Row.FindControl("lblTaskDesc")).Text = d.Description;
                ((ImageButton)e.Row.FindControl("btnClose")).CommandArgument = d.Id.ToString();
                ((ImageButton)e.Row.FindControl("btnClose")).Visible = d.Active;
            }
        }

        protected void btnClose_Command(object sender, CommandEventArgs e)
        {
            QuickTask item = DBController.DbCalendar.Get(new QuickTasksSearchParameters { Id =  Convert.ToInt32(e.CommandArgument) }).First();
            item.Active = false;
            DBController.DbCalendar.Update(item);

            RefreshGrids();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txName")).Text = ((TextBox)e.Row.FindControl("txIdesc")).Text = string.Empty;
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txName = ((TextBox)row.FindControl("txName")).Text;
            var txIdesc = ((TextBox)row.FindControl("txIdesc")).Text;
            if (txName.NotEmpty() && txIdesc.NotEmpty())
            {
                QuickTask t = new QuickTask
                {
                    Name = txName,
                    Description = txIdesc,
                    Active = true,
                    CreationDate = DateTime.Now,
                    UserGroupId = CurrentUser.Id
                };
                DBController.DbCalendar.Add(t);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                RefreshGrids();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        private void RefreshGrids()
        {
            RefreshGrid(gv);
            RefreshGrid(gvAdd);
        }
    }
}