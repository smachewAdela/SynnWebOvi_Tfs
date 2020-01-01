﻿using SynnCore.Controls;
using SynnCore.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebSimplify;
using WebSimplify.Controls;
using WebSimplify.Data;

namespace SynnWebOvi
{
    public class SynnWebFormBase : System.Web.UI.Page
    {

        public static IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        internal const string DecimalFormat = "#.#";
        internal const string DummyMethodName = "GetDummy";
        public LoggedUser CurrentUser
        {
            get
            {
                return (LoggedUser)GetFromSession("ssUser_*");
            }
            set
            {
                StoreInSession("ssUser_*", value);
            }
        }

        public IEnumerable GetDummy()
        {
            List<object> items = new List<object> { 1 };
            return items;
        }

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("he-IL");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            base.InitializeCulture();
        }

        internal void FillEnum(DropDownList cmb, Type type, bool addSelectValue = true)
        {
            AddSelectItemForCombo(cmb);
            foreach (Enum item in Enum.GetValues(type))
            {
                string description = GenericFormatter.GetEnumDescription(item);
                cmb.Items.Add(new System.Web.UI.WebControls.ListItem { Text = description, Value = Convert.ToInt32(item).ToString() });
            }
        }

        internal void ClearInputFields(List<HtmlInputControl> ctrs)
        {
            foreach (HtmlInputControl ctr in ctrs)
            {
                ctr.Value = string.Empty;
            }
        }

        protected virtual List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                return new List<ClientPagePermissions>();
            }
        }
        
        protected virtual bool LoginProvider
        {
            get
            {
                return false;
            }
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    StringBuilder sbOut = new StringBuilder();
        //    StringWriter swOut = new StringWriter(sbOut);
        //    HtmlTextWriter htwOut = new HtmlTextWriter(swOut);
        //    base.Render(htwOut);
        //    CurrentHtml = sbOut.ToString();
        //    writer.Write(CurrentHtml);
        //}

        protected void ClearInputs(params HtmlControl[] p)
        {
            foreach (var input in p)
            {
                if (input is HtmlInputGenericControl)
                {
                    (input as HtmlInputGenericControl).Value = string.Empty;
                }
                else if (input is HtmlInputText)
                {
                    (input as HtmlInputText).Value = string.Empty;
                }
                else if (input is HtmlTextArea)
                {
                    (input as HtmlTextArea).Value = string.Empty;
                }
            }
        }

        protected bool ValidateInputs(params HtmlControl[] p)
        {
            foreach (var input in p)
            {
                if (input is HtmlInputText)
                    if (string.IsNullOrEmpty((input as HtmlInputText).Value))
                        return false;
            }
            return true;
        }

        protected virtual string DefaultNavItem
        {
            get
            {
                return "navmain";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.PreRender += new EventHandler(PagePreRender);
            if (!LoginProvider && CurrentUser == null)
                SynNavigation.Goto(Pages.Login);
            if (!IsPostBack)
            {
                if (RequiredPermissions.NotEmpty())
                    Title = GenericFormatter.GetEnumDescription(RequiredPermissions[0]);
                else
                    Title = "מסך הבית";
                if (!LoginProvider)
                {
                    BuildSiteMapLinks();
                    bool navSelected = false;
                    
                    if (RequiredPermissions.NotEmpty())
                    {
                        var requiredPermis = RequiredPermissions.FirstOrDefault();
                        PageLinkAttribute HasNavLink = GetPageLink(requiredPermis);
                        if (HasNavLink != null)
                        {
                            ((HtmlAnchor)Master.FindControl(HasNavLink.NavLinks.FirstOrDefault())).Attributes.Add("class", "active");
                            navSelected = true;
                        }
                    }

                    if(!navSelected)
                        ((HtmlAnchor)Master.FindControl(DefaultNavItem)).Attributes.Add("class", "active");

                }

                foreach (ClientPagePermissions en in RequiredPermissions)
                {
                    if (!CurrentUser.Allowed(en))
                        SynNavigation.Goto(Pages.Main);
                }
            }
        }

        internal void AlertSuccess()
        {
            AlertMessage("פעולה בוצעה בהצלחה !");
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ThemeHelper.Apply(this);
        }
        void PagePreRender(object sender, EventArgs e)
        {
            RenderModalBackground();
        }

        protected virtual List<Panel> ModalPanels
        {
            get
            {
                return new List<Panel>();
            }
        }

        public Panel MessageBoxx
        {
            get
            {
                return Master.FindControl("messageBoxx") as Panel;
            }
        }
        private void RenderModalBackground()
        {
            if (Master.NotNull())
            {
                var xpanels = new List<Panel> { MessageBoxx };// message box pannel
                xpanels.AddRange(ModalPanels);
                var visiblePanel = xpanels.Where(x => x.Visible).FirstOrDefault();

                if (visiblePanel.NotNull())
                {
                    visiblePanel.Attributes.CssStyle.Add("z-index", (ModalHelper.ZIndex + 1).ToString());
                    visiblePanel.AddModalToPage();
                }
            }
        }
        private void BuildSiteMapLinks()
        {
            foreach (ClientPagePermissions ep in Enum.GetValues(typeof(ClientPagePermissions)))
            {
                PageLinkAttribute attribute = GetPageLink(ep);
                if (attribute != null)
                {
                    foreach (var navlink in attribute.NavLinks)
                    {
                        Master.FindControl(navlink).Visible = CurrentUser.Allowed(ep);
                    }
                }
            }
        }

    

        public PageLinkAttribute GetPageLink(ClientPagePermissions pageP)
        {
            MemberInfo memberInfo = typeof(ClientPagePermissions).GetMember(pageP.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                PageLinkAttribute attribute = (PageLinkAttribute)memberInfo.GetCustomAttributes(typeof(PageLinkAttribute), false).FirstOrDefault();
                return attribute;
            }
            return null;
        }

        public void AlertMessage(string message)
        {
            if (Master.NotNull())
            {
                MessageBoxx.SetHeader("התראה");
                MessageBoxx.SetMessage(message);
                MessageBoxx.Show();
            }
            else
            {
                string scriptmessage = string.Format("alert(\"{0}\");", message);
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", scriptmessage, true);
            }
        }


        protected void AddSelectItemForCombo(DropDownList cmb)
        {
            cmb.Items.Add(new System.Web.UI.WebControls.ListItem { Text = "בחר", Value = "-1" , Selected = true});
        }


        public void SetComboValue(HtmlSelect cmb, string valueToFind)
        {
            var li = cmb.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(x => x.Value == valueToFind).FirstOrDefault();
            cmb.SelectedIndex = cmb.Items.IndexOf(li);
        }

        public void StoreInSession(string key, object obj)
        {
            Session[key] = obj;
        }
        
        public object GetFromSession(string key)
        {
            if (Session[key] != null)
                return Session[key];
            return null;
        }

        public void NotifySucces(string msg = null)
        {
            if (msg != null)
                AlertMessage(msg);
            else
                AlertMessage("הפעולה בוצעה בהצלחה");
        }

        public void RefreshGrid(GridView gv)
        {
            string methodName = GetGridSourceMethodName(gv.ID);
            MethodInfo m = Page.GetType().GetMethod(methodName, new Type[0]);
            gv.DataSource = (IEnumerable)m.Invoke(Page, null);
            gv.DataBind();            
        }
        

        internal virtual string GetGridSourceMethodName(string gridId)
        {
            throw new NotImplementedException();
        }

        public void ShowUserNotification(string messageText)
        {
                    ClientScript.RegisterStartupScript(GetType(), "jNotify", string.Format(@"
        jNotify(
            '{0}',
            {{
                ShowOverlay: {1},
                autoHide: true,
                TimeShown: {3},
                HorizontalPosition: 'right',
                VerticalPosition: 'bottom',
                LongTrip: 40,
                MinWidth: {2}
            }}
          );", messageText.Replace("\n", "<br>"), "false", "200", "2500"),
             true);
        }

        [WebMethod]
        [ScriptMethod()]
        public static void Navigate(string destinationpage)
        {
            string url = string.Empty;

            SynNavigation.Goto(url);
        }
    }

}