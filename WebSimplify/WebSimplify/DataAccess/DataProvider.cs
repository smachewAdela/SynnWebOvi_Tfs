using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SynnWebOvi
{
    public class SynnDataProvider
    {
        private static IDatabaseProvider dbc;
        public static string _connectionString;
        public static IDatabaseProvider DbProvider
        {
            get
            {
                if (dbc == null)
                {
                     _connectionString = ConfigurationSettings.AppSettings["connectionString"];
                    if (ConfigurationSettings.AppSettings["dbtype"] == "sql")
                        dbc = new SqlDatabaseProvider(_connectionString);
                }
                return dbc;
            }
        }

        public static class TableNames
        {
            public static string UserDictionary = "UserDictionary"; // user only
            public static string GoogleTokens = "GoogleTokens";
            public static string GoogleAPICredentials = "GoogleAPICredentials";
            public static string Users = "Users";
            public static string UserPreferences = "UserPreferences";
            public static string DevTasks = "DevTasks";
            //public static string GenericData = "GenericData";
            public static string Log = "Log";
            public static string ThemeLog = "ThemeLog";
            public static string ArchiveDocument = "ArchiveDocument";
            public static string WeddingItems = "WeddingItems"; // group
            public static string ShoppingItems = "ShoppingItems";  // group
            public static string ShiftsData = "ShiftsData"; // group
            public static string WorkHoursData = "WorkHoursData";
            public static string CreditData = "CreditData"; // group
            public static string CashData = "CashData"; // group
            public static string CashItems = "CashItems"; // group
            public static string MoneyTransactionItems = "MoneyTransactionItems";
            public static string MoneyTransactionTemplatess = "MoneyTransactionTemplatess";
            public static string AppSettings = "AppSettings";
            public static string DiaryData = "DiaryData";// group
            public static string QuickTasks = "QuickTasks";
            public static string MigrationItems = "MigrationItems";
            public static string PermissionGroups = "PermissionGroups";
            public static string User_PermissionGroups = "User_PermissionGroups";
            public static string ThemeItems = "ThemeItems";
            public static string LottoRows = "LottoRows";
            public static string LottoPoles = "LottoPoles";
        }
    }

    public interface IDatabaseProvider
    {
         IDbAuth DbAuth { get; }
         IDbLog DbLog { get; }
        IDbWedd DbWedd { get; }


        IDbShop DbShop { get; }
        IDbMoney DbMoney { get; }
        IDbShifts DbShifts { get; }
        IDbUserDictionary DbUserDictionary { get; }

        IDbDocument DbDocument { get; }

        LoggedUser CurrentUser { get; set; }

        IDbCalendar DbCalendar { get;}

        IDbMigration DbMigration { get; }

        IDbGenericData DbGenericData { get; }

        IDbGoogle DbGoogle { get; }
    }

    abstract class BaseDatabaseProvider : IDatabaseProvider
    {
        private string connString;

        public BaseDatabaseProvider(string connectionString)
        {
            this.connString = connectionString;
        }

        public virtual IDbGoogle DbGoogle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IDbAuth DbAuth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IDbUserDictionary DbUserDictionary
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IDbLog DbLog
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual LoggedUser CurrentUser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbWedd DbWedd
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        LoggedUser IDatabaseProvider.CurrentUser
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbShop DbShop
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbCalendar DbCalendar
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbMigration DbMigration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbShifts DbShifts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbMoney DbMoney
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbGenericData DbGenericData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbDocument DbDocument
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

 
}