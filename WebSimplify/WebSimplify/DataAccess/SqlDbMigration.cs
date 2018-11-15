﻿using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbMigration : SqlDbController, IDbMigration
    {
        public SqlDbMigration(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void ExecurteCreateTable(string tsql)
        {
            ClearParameters();
            ExecuteSql(tsql);
        }

        public void FinishMethod(string stepName)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", stepName));
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            SetInsertIntoSql(SynnDataProvider.TableNames.MigrationItems, sqlItems);
            ExecuteSql();
        }

        public List<string> GetAlreadyFinishedSteps()
        {
            SetSqlFormat("select Name from {0}", SynnDataProvider.TableNames.MigrationItems);
            ClearParameters();
            
            var lst = new List<string>();
            using (IDataReader data = DoSelect())
            {
                while (data.Read())
                    lst.Add(data.GetString(0));
            }
            return lst;
        }
    }
}