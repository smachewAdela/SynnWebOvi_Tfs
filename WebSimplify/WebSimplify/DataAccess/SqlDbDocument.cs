using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbDocument : SqlDbController, IDbDocument
    {
        public SqlDbDocument(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public List<ArchiveDocument> GetDocuments(DocumentSearchParameters dp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ArchiveDocument);
            ClearParameters();
            if(dp.ArchiveDocumentId.HasValue)
                AddSqlWhereField("Id", dp.ArchiveDocumentId.Value);
            if(dp.SearchDescription.NotEmpty())
                AddSqlWhereLikeField("Description", dp.SearchDescription);
            if (dp.SearchName.NotEmpty())
                AddSqlWhereLikeField("Name", dp.SearchName);

            var lst = new List<ArchiveDocument>();
            FillList(lst, typeof(ArchiveDocument));
            return lst;
        }

        public void Upsert(ArchiveDocument i)
        {
            var sqlItems = new SqlItemList();

            sqlItems.Add(new SqlItem("Name", i.Name));
            sqlItems.Add(new SqlItem("Description", i.Description));
            sqlItems.Add(new SqlItem("LastKnownPath", i.LastKnownPath));
            sqlItems.Add(new SqlItem("CreationDate", i.CreationDate));
            sqlItems.Add(new SqlItem("Data", Convert.ToBase64String(i.Data)));

            if (i.Id > 0)
            {
                SetUpdateSql(SynnDataProvider.TableNames.ArchiveDocument, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id } });
                ExecuteSql();
            }
            else
            {
                SetInsertIntoSql(SynnDataProvider.TableNames.ArchiveDocument, sqlItems);
                ExecuteSql();

                i.Id = GetLastIdentityValue(SynnDataProvider.TableNames.ArchiveDocument);
            }
        }

        public int GetLastIdentityValue(string table)
        {
            SetSqlFormat("select max(id) from {0}", table);
            ClearParameters();
            var obj = GetSingleRecordFirstValue();
            return int.Parse(obj.ToString());
        }

        public ArchiveDocument GetDocumentArchiveFull(int docId)
        {
            var arch = GetDocuments(new DocumentSearchParameters { ArchiveDocumentId = docId }).FirstOrDefault();

            SetSqlFormat("select Data from {0}", SynnDataProvider.TableNames.ArchiveDocument);
            ClearParameters();
            AddSqlWhereField("Id", docId);
    
            using (IDataReader Data = DoSelect())
            {
                while (Data.Read())
                if (Data["Data"] != DBNull.Value)
                {
                        arch.LoadData(Data);
                }
            }
            return arch;
        }

        public List<CarArchiveDocument> GetCarDocuments(DocumentSearchParameters dp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.CarArchiveDocument);
            ClearParameters();
            if (dp.ArchiveDocumentId.HasValue)
                AddSqlWhereField("Id", dp.ArchiveDocumentId.Value);
            if (dp.SearchDescription.NotEmpty())
                AddSqlWhereLikeField("Description", dp.SearchDescription);
            if (dp.SearchName.NotEmpty())
                AddSqlWhereLikeField("Name", dp.SearchName);
            if (dp.SearchCarId.HasValue)
                AddSqlWhereField("CarId", dp.SearchCarId.Value);
            var lst = new List<CarArchiveDocument>();
            FillList(lst, typeof(CarArchiveDocument));
            return lst;
        }

        public CarArchiveDocument GetCarDocumentArchiveFull(int docId)
        {
            var arch = GetCarDocuments(new DocumentSearchParameters { ArchiveDocumentId = docId }).FirstOrDefault();

            SetSqlFormat("select Data from {0}", SynnDataProvider.TableNames.CarArchiveDocument);
            ClearParameters();
            AddSqlWhereField("Id", docId);

            using (IDataReader Data = DoSelect())
            {
                while (Data.Read())
                    if (Data["Data"] != DBNull.Value)
                    {
                        arch.LoadData(Data);
                    }
            }
            return arch;
        }

        public void UpsertCarDoc(CarArchiveDocument i)
        {
            var sqlItems = new SqlItemList();

            sqlItems.Add(new SqlItem("Name", i.Name));
            sqlItems.Add(new SqlItem("Description", i.Description));
            sqlItems.Add(new SqlItem("LastKnownPath", i.LastKnownPath));
            sqlItems.Add(new SqlItem("CreationDate", i.CreationDate));
            sqlItems.Add(new SqlItem("CarId", i.CarId));
            sqlItems.Add(new SqlItem("Data", Convert.ToBase64String(i.Data)));

            if (i.Id > 0)
            {
                SetUpdateSql(SynnDataProvider.TableNames.CarArchiveDocument, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id } });
                ExecuteSql();
            }
            else
            {
                SetInsertIntoSql(SynnDataProvider.TableNames.CarArchiveDocument, sqlItems);
                ExecuteSql();

                i.Id = GetLastIdentityValue(SynnDataProvider.TableNames.CarArchiveDocument);
            }
        }
    }
}