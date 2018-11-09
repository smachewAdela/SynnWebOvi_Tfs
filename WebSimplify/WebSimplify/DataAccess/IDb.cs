﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify;
using WebSimplify.Data;

namespace SynnWebOvi
{
    public class IDb
    {
    }

    public interface IDbAuth
    {
        bool ValidateUserCredentials(string userName, string passwword);
        LoggedUser LoadUserSettings(string userName, string passwword);
        List<PermissionGroup> GetPermissionGroup();
        void Add(PermissionGroup g);
        void Add(LoggedUser u);
        List<LoggedUser> GetUsers(UserSearchParameters lp);
        void Update(LoggedUser u);
        LoggedUser GetUser(int ownerId);
        void UpdatePreferences(LoggedUser u);
    }

    public interface IDbLog
    {
        string AddLog(Exception ex);
        void AddLog(string message);
        List<LogItem> GetLogs(LogSearchParameters lsp);
        void AddThemeLog(ThemeLog tl);
        ThemeLog GetLastItem();
    }

    public interface IDbWedd
    {
        List<WeddingGuest> GetGuests(WeddSearchParameters sp);
    }

    public interface IDbShop
    {
        List<ShopItem> Get(ShopSearchParameters shopSearchParameters);
        void ActivateShopItem(ShopSearchParameters shopSearchParameters);
        void DeActivateShopItem(ShopSearchParameters shopSearchParameters);
        void AddNewShopItem(ShopItem n);
    }

    public interface IDbMoney
    {
        void Add(CashMonthlyData i);
        void Add(CreditCardMonthlyData i);
        List<CashMonthlyData> Get(CashSearchParameters cashSearchParameters);
        List<CreditCardMonthlyData> Get(CreditSearchParameters creditSearchParameters);
        void Update(CreditCardMonthlyData i);
        void Update(CashMonthlyData i);
        List<CashMoneyItem> GetCashItems(CashSearchParameters cashSearchParameters);
        void Add(CashMoneyItem i);
    }

    public interface IDbShifts
    {
        void Save(ShiftsSearchParameters sp);
        List<ShiftDayData> GetShifts(ShiftsSearchParameters shiftsSearchParameters);
        void Delete(int id);
        List<WorkHoursData> GetWorkHoursData(WorkHoursSearchParameters workHoursSearchParameters);
        void AddWorkMonthlyData(WorkHoursData wh);
        void UpdateWorkMonthlyData(WorkHoursData wh);
    }


    public interface IDbCalendar
    {
        void Add(CalendarSearchParameters sp);
        List<QuickTask> Get(QuickTasksSearchParameters quickTasksSearchParameters);
        List<MemoItem> Get(CalendarSearchParameters sp);
        void Update(QuickTask item);
        void Add(QuickTask t);
    }

    public interface IDbMigration
    {
        void ExecurteCreateTable(string v);
        void FinishMethod(string stepName);
        List<string> GetAlreadyFinishedSteps();
    }

    public interface IDbUserDictionary
    {
        void Add(DictionarySearchParameters p);
        List<DictionaryItem> PerformSearch(DictionarySearchParameters p);
    }

    public class BaseSearchParameters
    {
        public LoggedUser CurrentUser { get; set; }

        public bool RequirePrivateKeyOnly { get; set; }
        public BaseSearchParameters()
        {
            CurrentUser = AccountData.GetCurrentUser();
        }
    }
    public class LogSearchParameters: BaseSearchParameters
    {
        public LogSearchParameters() : base()
        {
        }

        public string Text { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class ShiftsSearchParameters : BaseSearchParameters
    {
        public ShiftsSearchParameters() : base()
        {
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public ShiftDayData ItemForAction { get; set; }

    }

    public class ShopSearchParameters : BaseSearchParameters
    {
        public ShopSearchParameters() : base()
        {
        }

        public bool? Active { get; internal set; }
        public int? IdToActivate { get; internal set; }
        public int? IdToDeactivate { get; internal set; }
        public string ItemName { get; internal set; }
    }

    public class CreditSearchParameters : BaseSearchParameters
    {
        public CreditSearchParameters() : base()
        {
        }

        public bool? Active { get;  set; }
        public int? Id { get;  set; }
        public DateTime? Month { get;  set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class CashSearchParameters : BaseSearchParameters
    {
        public CashSearchParameters() : base()
        {
        }

        public bool? Active { get; set; }
        public int? Id { get; set; }
        public DateTime? Month { get; set; }
    }

    public class WorkHoursSearchParameters : BaseSearchParameters
    {
        public WorkHoursSearchParameters() : base()
        {
        }

        public bool? Active { get; set; }
        public int? Id { get; set; }
        public DateTime? Month { get; set; }
    }

    public class WeddSearchParameters : BaseSearchParameters
    {
        public WeddSearchParameters() : base()
        {
        }

        public string SearchText { get; set; }
    }

    public class UserSearchParameters : BaseSearchParameters
    {
        public UserSearchParameters() : base()
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Id { get; set; }
    }

    public class DictionarySearchParameters : BaseSearchParameters
    {
        public DictionarySearchParameters() : base()
        {
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public string SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class QuickTasksSearchParameters : BaseSearchParameters
    {
        public QuickTasksSearchParameters() : base()
        {
        }

        public string SearchText { get; set; }
        public bool? Active { get; set; }
        public int? Id { get; internal set; }
    }

    public class CalendarSearchParameters : BaseSearchParameters
    {
        public CalendarSearchParameters() : base()
        {
        }

        public DateTime? FromDate { get;  set; }
        public DateTime? ToDate { get; set; }
        
        internal MemoItem InsertItem { get; set; }
    }

}