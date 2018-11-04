﻿using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSimplify;

namespace SynnWebOvi
{
    internal class SqlDatabaseProvider : IDatabaseProvider
    {
        private string _connectionString;
        internal IDbAuth IDbAuth = null;
        internal IDbUserDictionary IDbUserDictionary = null;
        internal IDbLog IDbLog = null;
        internal IDbWedd IDbWedd = null;
        internal IDbShop IDbShop = null;
        internal IDbShifts IDbShifts = null;
        internal IDbCredit IDbCredit = null;
        

        internal IDbCalendar IDbCalendar = null;
        

        internal LoggedUser LoggedUser = null;

        public SqlDatabaseProvider(string _connectionString)
        {
            this._connectionString = _connectionString;
        }

        public IDbAuth DbAuth
        {
            get
            {
                if (IDbAuth == null)
                    IDbAuth = new SqlDbAuth(_connectionString);
                return IDbAuth;
            }
        }

        public IDbUserDictionary DbUserDictionary
        {
            get
            {
                if (IDbUserDictionary == null)
                    IDbUserDictionary = new SqlDbUserDictionary(_connectionString);
                return IDbUserDictionary;
            }
        }

        public IDbLog DbLog
        {
            get
            {
                if (IDbLog == null)
                    IDbLog = new SqlDbLog(_connectionString);
                return IDbLog;
            }
        }

        public LoggedUser CurrentUser
        {
            get
            {
                if (LoggedUser == null)
                    return null;
                return LoggedUser;
            }
            set
            {
                CurrentUser = value;
            }
        }

        public IDbWedd DbWedd
        {
            get
            {
                if (IDbWedd == null)
                    IDbWedd = new SqlDbWedd(_connectionString);
                return IDbWedd;
            }
        }

        public IDbShop DbShop
        {
            get
            {
                if (IDbShop == null)
                    IDbShop = new SqlDbShop(_connectionString);
                return IDbShop;
            }
        }

        public IDbCalendar DbCalendar
        {
            get
            {
                if (IDbCalendar == null)
                    IDbCalendar = new SqlDbCalendar(_connectionString);
                return IDbCalendar;
            }
        }

        public IDbShifts DbShifts
        {
            get
            {
                if (IDbShifts == null)
                    IDbShifts = new SqlDbShifts(_connectionString);
                return IDbShifts;
            }
        }

        public IDbCredit DbCredit
        {
            get
            {
                if (IDbCredit == null)
                    IDbCredit = new SqlDbCredit(_connectionString);
                return IDbCredit;
            }
        }

        public void SetUser(LoggedUser u)
        {
            LoggedUser = u;
        }
    }
}