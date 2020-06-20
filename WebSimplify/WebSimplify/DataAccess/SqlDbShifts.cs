using SynnCore.DataAccess;
using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbShifts : SqlDbController, IDbShifts
    {
        public SqlDbShifts(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {

        }

        public void Save(ShiftDayData i)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date",i.Date));
            sqlItems.Add(new SqlItem("DaylyShift", (int)i.DaylyShift));
            SetInsertIntoSql(SynnDataProvider.TableNames.ShiftsData, sqlItems);
            ExecuteSql();
        }
        

        public List<ShiftDayData> GetShifts(ShiftsSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
        
                if (lsp.FromDate.HasValue)
                    AddSqlWhereField("Date", lsp.FromDate, ">=");
                if (lsp.ToDate.HasValue)
                    AddSqlWhereField("Date", lsp.ToDate, "<=");
            if (lsp.IDate.HasValue)
                AddSqlWhereField("Date", lsp.IDate);

            if (lsp.DaylyShiftTime.HasValue)
                AddSqlWhereField("DaylyShift", (int)lsp.DaylyShiftTime.Value);

            var lst = new List<ShiftDayData>();
            FillList(lst, typeof(ShiftDayData));
            return lst;
        }

        public void Delete(int id)
        {
            SetSqlFormat("delete {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            AddSqlWhereField("Id", id);
            ExecuteSql();
        }

        public List<WorkHoursData> GetWorkHoursData(WorkHoursSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.WorkHoursData);
            ClearParameters();
            if (lsp.Month.HasValue)
            {
                var d = lsp.Month.Value;
                AddSqlWhereField("Month", new DateTime(d.Year, d.Month, 1), ">=");
                AddSqlWhereField("Month", new DateTime(d.Year, d.Month, d.NumberOfDays()), "<");
            }
            if (lsp.Id.HasValue)
                AddSqlWhereField("Id", lsp.Id.Value);
            if (lsp.Active.HasValue)
                AddSqlWhereField("Active", lsp.Active.Value);
            var lst = new List<WorkHoursData>();
            FillList(lst, typeof(WorkHoursData));
            return lst;
        }

        public void AddWorkMonthlyData(WorkHoursData w)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Month",w.Month));
            sqlItems.Add(new SqlItem("Active", w.Active));
            sqlItems.Add(new SqlItem("CurrentMonthTotal", w.CurrentMonthTotal.ToXml()));
            sqlItems.Add(new SqlItem("CurrentShiftStart", w.CurrentShiftStart.ToXml()));
            sqlItems.Add(new SqlItem("CurrentShiftEnd", w.CurrentShiftEnd.ToXml()));
            SetInsertIntoSql(SynnDataProvider.TableNames.WorkHoursData, sqlItems);
            ExecuteSql();
        }

        public void UpdateWorkMonthlyData(WorkHoursData w)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Month", w.Month));
            sqlItems.Add(new SqlItem("Active", w.Active));
            sqlItems.Add(new SqlItem("CurrentMonthTotal", w.CurrentMonthTotal.ToXml()));
            sqlItems.Add(new SqlItem("CurrentShiftStart", w.CurrentShiftStart.ToXml()));
            sqlItems.Add(new SqlItem("CurrentShiftEnd", w.CurrentShiftEnd.ToXml()));
            SetUpdateSql(SynnDataProvider.TableNames.WorkHoursData, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = w.Id } });
            ExecuteSql();
        }
    }
}