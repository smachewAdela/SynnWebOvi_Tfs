using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class ArchiveDocument : IDbLoadable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastKnownPath { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreationDate { get; set; }

        public ArchiveDocument()
        {

        }
        public ArchiveDocument(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
            LastKnownPath = DataAccessUtility.LoadNullable<string>(reader, "LastKnownPath");
            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");

          
        }

        public void LoadData(IDataReader reader)
        {
            var data = DataAccessUtility.LoadNullable<string>(reader, "Data");
            Data = (byte[])Convert.FromBase64CharArray(data.ToArray(), 0, data.Length);
        }
    }

    public class CarArchiveDocument : ArchiveDocument
    {
        public int CarId { get; set; }

        public CarArchiveDocument()
        {

        }
        public CarArchiveDocument(IDataReader data)
        {
            CarId = DataAccessUtility.LoadInt32(data, "CarId");
            Load(data);
        }
    }
}