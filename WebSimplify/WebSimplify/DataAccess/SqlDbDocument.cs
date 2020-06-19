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

        public List<ArchiveDocument> GetDocuments(DocumentSearchParameters documentSearchParameters)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ArchiveDocument);
            ClearParameters();
            if(documentSearchParameters.ArchiveDocumentId.HasValue)
                AddSqlWhereField("Id", documentSearchParameters.ArchiveDocumentId.Value);
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

                i.Id = GetLastIdentityValue();
            }
        }

        public int GetLastIdentityValue()
        {
            SetSqlFormat("select max(id) from {0}", SynnDataProvider.TableNames.ArchiveDocument);
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
    }
}