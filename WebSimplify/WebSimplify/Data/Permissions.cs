using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum ClientPagePermissions
    {
        [PageLink("navdic")]
        [Description("מילון משתמש")]
        Dictionary,
        [PageLink("navdiary")]
        [Description("יומן")]
        Diary,
        [PageLink("navshifts")]
        [Description("משמרות")]
        Shifts,
        [PageLink("navwed")]
        [Description("חתונה")]
        Wedding,
        [PageLink("navshop")]
        [Description("קניות")]
        Shopping,
        [PageLink("navtask")]
        [Description("משימות")]
        QuickTasks,
        [PageLink("navcredit")]
        [Description("אשראי")]
        CreditData,
        [PageLink("navusers", true)]
        [Description("מנהל משתמשים")]
        SysUsers,
        [PageLink("navmonbal")]
        [Description(@" מעקב הוצאות\הכנסות")]
        MoneyBalance,
        [PageLink("navdev", true)]
        [Description("מנהל משימות פיתוח")]
        SysDev,
        [PageLink("navlog", true)]
        [Description("יומן מערכת")]
        SysLog,
        [PageLink("navcash")]
        [Description(" מעקב מזומן")]
        CashLog,
        [Description("מעקב שעות עבודה")]
        WorkHours,
        [PageLink("navgd", true)]
        [Description("נתוני טבלאות")]
        GenericDataItems,
        [PageLink("navHC")]
        [Description(" יומן בריאות")]
        HealthCare,
        [PageLink("navdepos")]
        [Description("  הפקדות")]
        Deposits,
        [PageLink("navdocs")]
        [Description("  מסמכים")]
        Archive,
        [PageLink("navcars")]
        [Description("  יומן רכב")]
        CarLog,
    }

    public class PageLinkAttribute : Attribute
    {
        public string NavLink { get;  set; }
        public bool RequireAdmin { get; set; }

        public PageLinkAttribute( string linkIs, bool requireAdmin = false)
        {
            RequireAdmin = requireAdmin;
            NavLink = linkIs;
        }
    }
}