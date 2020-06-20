using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SynnWebOvi;
using SynnCore.DataAccess;
using System.Data;
using SynnCore.Generics;
using WebSimplify.Controls;

namespace WebSimplify.Data
{
    public enum ShiftTime
    {
        [Description("בוקר")]
        Morning,
        [Description("ערב")]
        Noon,
        [Description("לילה")]
        Night,
        [Description("בוקר ערב")]
        LongMorning,
        [Description("לילה מוקדם")]
        LongNight,
        [Description("בוקר לילה")]
        MorningAndNight,
    }

    [Serializable]
    public class ShiftDayData : IDbLoadable, IMarkAble, ICalendarItem
    {
        public DateTime Date { get; set; }
        public ShiftTime DaylyShift { get; set; }
        public int Id { get;  set; }
        public int UserGroupId { get; set; }
        public int OwnerId { get;  set; }

        public string MarkableDescription
        {
            get
            {
                return Date.HebrewDayName();
            }
        }

        public string MarkableName
        {
            get
            {
                return GenericFormatter.GetEnumDescription(DaylyShift);
            }
        }

        public string MarkableType
        {
            get
            {
                return "המשמרת הבאה";
            }
        }

        public string Display
        {
            get
            {
                return GenericFormatter.GetEnumDescription(DaylyShift);
            }
        }

        public DateTime WeekStart
        {
            get
            {
                return Date.StartOfWeek().Date;
            }
        }

        public int Index
        {
            get
            {
                return (int)DaylyShift;
            }
        }

        public ShiftDayData()
        {
        }
        public ShiftDayData(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
            DaylyShift = (ShiftTime)DataAccessUtility.LoadInt32(reader, "DaylyShift");
        }
    }
}