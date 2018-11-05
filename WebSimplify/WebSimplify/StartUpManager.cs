﻿using System;
using SynnWebOvi;
using System.Collections.Generic;
using System.Linq;
using WebSimplify.Data;

namespace WebSimplify
{
    internal static class StartUpManager
    {
        internal static IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        internal static void PerformUserStartUp(LoggedUser u)
        {
            if (u.Allowed(ClientPagePermissions.CreditData))
            {
                CreditDataAction(u);
            }
            if (u.Allowed(ClientPagePermissions.CashLog))
            {
                CashDataAction(u);
            }
        }

        private static void CashDataAction(LoggedUser u)
        {
            var date = DateTime.Now;
            var current = DBController.DbMoney.Get(new CashSearchParameters { Month = date }).FirstOrDefault();
            if (current == null)
            {
                var i = new CashMonthlyData
                {
                    Active = true,
                    Date = new DateTime(date.Year, date.Month, 1),
                    TotalSpent = 0,
                    UserGroupId = u.AllowedSharedPermissions[0]
                };
                DBController.DbMoney.Add(i);
            }
        }

        private static void CreditDataAction(LoggedUser u)
        {
            var p = u.Preferences;
            if (p.CreditLogStartDate.NotEmpty() && p.CreditCardPaymentDay > 0)
            {

                var date = p.CreditLogStartDate;
                while (date <= DateTime.Now)
                {
                    var current = DBController.DbMoney.Get(new CreditSearchParameters { Month = date }).FirstOrDefault();
                    if (current == null)
                    {
                        var i = new CreditCardMonthlyData
                        {
                            Active = true,
                            Date = new DateTime(date.Year, date.Month, p.CreditCardPaymentDay),
                            TotalSpent = 0,
                            UserGroupId = u.AllowedSharedPermissions[0]
                        };
                        DBController.DbMoney.Add(i);
                    }
                    date = date.AddMonths(1);
                }
            }
        }
    }
}