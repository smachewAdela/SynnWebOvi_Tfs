using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify;
using WebSimplify.Data;
using WebSimplify.DataAccess;

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
        LoggedUser LoadUserSettings(string userAlias);
        void Update(LoggedUser u);
        LoggedUser GetUser(int ownerId);
      
        List<DevTaskItem> Get(DevTaskItemSearchParameters devTaskItemSearchParameters);
        void Add(DevTaskItem d);
        void Update(DevTaskItem d);
    }

    public interface IDbLog
    {
        string AddLog(Exception ex);
        void AddLog(string message);
        List<LogItem> GetLogs(LogSearchParameters lsp);
        void AddThemeLog(ThemeLog tl);
        ThemeLog GetLastItem();
        List<ThemeScript> GetThemes(ThemeSearchParameters themeSearchParameters);
        void Update(ThemeScript i);
        void Add(ThemeScript i);
    }

    public interface IDbWedd
    {
        void Add(WeddingGuest w);
        List<WeddingGuest> GetGuests(WeddSearchParameters sp);
    }

    public interface IDbShop
    {
        List<ShopItem> Get(ShopSearchParameters shopSearchParameters);
        void ActivateShopItem(ShopSearchParameters shopSearchParameters);
        void DeActivateShopItem(ShopSearchParameters shopSearchParameters);
        void AddNewShopItem(ref ShopItem n);
    }

    public interface IDbMoney
    {
        #region Credit
        void Add(CreditCardMonthlyData i);
        List<CreditCardMonthlyData> Get(CreditSearchParameters creditSearchParameters);
        void Update(CreditCardMonthlyData i);


        #endregion


        #region Cash
        List<CashMonthlyData> Get(CashSearchParameters cashSearchParameters);

        void Add(CashMoneyItem i);

        void Add(CashMonthlyData i);
        void Update(CashMonthlyData i);
        List<CashMoneyItem> GetCashItems(CashSearchParameters cashSearchParameters);

        #endregion



        #region Balance

        void Add(MonthlyMoneyTransaction trnForMonth);
        MoneyTransactionTemplate GetTransactionTemplate(int templateId);
        List<MoneyTransactionTemplate> GetMoneyTransactionTemplates(MonthlyMoneyTransactionSearchParameters mp);
        List<MonthlyMoneyTransaction> GetMoneyTransactions(MonthlyMoneyTransactionSearchParameters mp);
        MonthlyMoneyTransaction GetTransaction(MonthlyMoneyTransactionSearchParameters mp);
        void Update(MoneyTransactionTemplate tmpl);
        void Add(MoneyTransactionTemplate i);
        void Update(MonthlyMoneyTransaction i);

        #endregion
    }

    public interface IDbShifts
    {
        void Save(ShiftDayData sp);
        List<ShiftDayData> GetShifts(ShiftsSearchParameters shiftsSearchParameters);
        void Delete(int id);
        List<WorkHoursData> GetWorkHoursData(WorkHoursSearchParameters workHoursSearchParameters);
        void AddWorkMonthlyData(WorkHoursData wh);
        void UpdateWorkMonthlyData(WorkHoursData wh);
    }


    public interface IDbCalendar
    {
        void Add(MemoItem sp);
        List<QuickTask> Get(QuickTasksSearchParameters quickTasksSearchParameters);
        List<MemoItem> Get(CalendarSearchParameters sp);
        void Update(QuickTask item);
        void Add(QuickTask t);
    }

    public interface IDbMigration
    {
        void ExecurteCreateTable(string v);
        void FinishMethod(string stepName);
        bool CheckTableExistence(string tableName);
        List<string> GetAlreadyFinishedSteps();
        void ClearDb(string clearDbScript);
    }

    public interface IDbGenericData
    {
        void Update(GenericData g);
        List<T> GetGenericData<T>(GenericDataSearchParameters sp);
        void Add(GenericData g);
        IEnumerable GetGenericData(GenericDataSearchParameters genericDataSearchParameters);
        object GetSingleGenericData(GenericDataSearchParameters genericDataSearchParameters);
    }

    public interface IDbGoogle
    {
         int AppUserId { get; set; }
    }

    public interface IDbUserDictionary
    {
        void Add(DictionarySearchParameters p);
        List<DictionaryItem> PerformSearch(DictionarySearchParameters p);
    }

    public interface IDbDocument
    {
        ArchiveDocument GetDocumentArchiveFull(int docId);
        List<ArchiveDocument> GetDocuments(DocumentSearchParameters documentSearchParameters);
        void Upsert(ArchiveDocument doc);
    }

    public class BaseSearchParameters
    {
        public LoggedUser CurrentUser { get; set; }
        public bool FromWs { get; set; }
        public bool RequirePrivateKeyOnly { get; set; }
        public BaseSearchParameters()
        {
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
        public ShiftTime? DaylyShiftTime { get; internal set; }
        public DateTime? IDate { get; internal set; }
    }
    
        public class DevTaskItemSearchParameters : BaseSearchParameters
    {
        public DevTaskItemSearchParameters() : base()
        {
        }

        public bool? Active { get; internal set; }
        public int? Id { get; internal set; }
        public string ItemName { get; internal set; }
    }
    public class ShopSearchParameters : BaseSearchParameters
    {
        public ShopSearchParameters() : base()
        {
        }

        public bool? Active { get; internal set; }
        public int? Id { get; internal set; }
        public string ItemName { get; internal set; }
        public DateTime? LastBought { get; internal set; }
    }

    public class LottoSearchParameters : BaseSearchParameters
    {
        public LottoSearchParameters() : base()
        {
        }

        public DateTime? PoleActionDate { get; internal set; }
        public string PoleKey { get; internal set; }
        public int? Id { get; internal set; }

    }

    public class LottoRowsSearchParameters : LottoSearchParameters
    {
    }

    public class LottoPolesSearchParameters : LottoSearchParameters
    {
    }

    public class GenericDataSearchParameters : BaseSearchParameters
    {
        public int? Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? Active { get; set; }
        public int? FieldIndex { get; set; }
        public string FieldIndexValue { get; set; }
        public List<GenericDataDbFilter> Filters { get; set; }
        public Type FromType { get; set; }

        public GenericDataSearchParameters()
        {
            Filters = new List<GenericDataDbFilter>();
        }

        public virtual void AppendExtraFieldsValues()
        {
            if (Id.HasValue)
                Filters.Add(new GenericDataDbFilter("Id", Id.ToString()));
            if (Active.HasValue)
                Filters.Add(new GenericDataDbFilter("Active", Active.Value));
        }
    }

    public class UserDepositSearchParameters : GenericDataSearchParameters
    {
        public UserDepositSearchParameters()
        {

        }
        public int? UserId { get; set; }

        public override void AppendExtraFieldsValues()
        {
            base.AppendExtraFieldsValues();
            if (UserId.HasValue)
                Filters.Add(new GenericDataDbFilter("UserId", UserId.ToString()));
        }
    }

    public class UserStatsSearchParameters : GenericDataSearchParameters
    {
        public UserStatsSearchParameters()
        {

        }
        public int? UserId { get; set; }

        public override void AppendExtraFieldsValues()
        {
            base.AppendExtraFieldsValues();
            if (UserId.HasValue)
                Filters.Add(new GenericDataDbFilter("UserId", UserId.ToString()));
        }
    }

    public class GoogleApDataSearchParameters : GenericDataSearchParameters
    {
        public GoogleApDataSearchParameters()
        {
            
        }
        public int? UserId { get; set; }

        public override void AppendExtraFieldsValues()
        {
            base.AppendExtraFieldsValues();
            if (UserId.HasValue)
                Filters.Add(new GenericDataDbFilter("UserId", UserId.ToString()));
        }
    }

    public class UserMemoSharingSettingsSearchParameters : GenericDataSearchParameters
    {
        public int? OwnerUserId { get; set; }

        public UserMemoSharingSettingsSearchParameters()
        {
            
        }

        public override void AppendExtraFieldsValues()
        {
            base.AppendExtraFieldsValues();
            if (OwnerUserId.HasValue)
                Filters.Add(new GenericDataDbFilter("OwnerUserId", OwnerUserId.ToString()));
        }
    }
    public class CalendarJobSearchParameters : GenericDataSearchParameters
    {
        public CalendarJobSearchParameters()
        {
            
        }
        public int? UserId { get; set; }
        public int? MemoId { get; set; }
        public CalendarJobStatusEnum? CalendarJobStatus { get; set; }

        public override void AppendExtraFieldsValues()
        {
            base.AppendExtraFieldsValues();
            if (UserId.HasValue)
                Filters.Add(new GenericDataDbFilter("UserId", UserId.ToString()));
            if (CalendarJobStatus.HasValue)
                Filters.Add(new GenericDataDbFilter("JobStatus", ((int)CalendarJobStatus.Value).ToString()));
            if (MemoId.HasValue)
                Filters.Add(new GenericDataDbFilter("MemoItemId", MemoId.ToString()));
        }
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

    public class MonthlyMoneyTransactionSearchParameters : BaseSearchParameters
    {
        public MonthlyMoneyTransactionSearchParameters() : base()
        {
        }

        public bool? Closed { get; set; }
        public int? Id { get; set; }
        public DateTime? Month { get; set; }
        public int? TemplateId { get;  set; }
        public MonthlyTransactionType? TranType { get;  set; }
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
        public List<int> Ids { get; internal set; }
    }

    public class ThemeSearchParameters : BaseSearchParameters
    {
        public ThemeSearchParameters() : base()
        {
        }

        public int? Id { get; set; }

    }

    public class DocumentSearchParameters : BaseSearchParameters
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ArchiveDocumentId { get; internal set; }
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
        public DateTime? FromCreationDate { get;  set; }
        public DateTime? ToCreationDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ID { get; internal set; }
        public int? UserId { get; internal set; }
        public List<int> IDs { get; internal set; }
    }

}