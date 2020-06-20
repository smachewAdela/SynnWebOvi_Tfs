using SynnCore.DataAccess;
using SynnCore.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public static  class CultureHelper
    {
        public static CultureInfo HebrewCulture =  CultureInfo.CreateSpecificCulture("he-IL");
        public static string ToJewishDateString(this DateTime value, string format)
        {
            HebrewCulture.DateTimeFormat.Calendar = new HebrewCalendar();
            return value.ToString(format, HebrewCulture);
        }


    }

    public static class ExtensionsHelper
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        public static DateTime EndOfNextWeek(this DateTime dt)
        {
            return dt.EndOfWeek().AddDays(7);
        }

        public static bool InSameWeek(this DateTime dt, DateTime o)
        {
            return dt.StartOfWeek().Date == o.StartOfWeek().Date;
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime (dt.Year,dt.Month,1);
        }

        public static DateTime EndOfWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Saturday)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year,dt.Month,1).AddMonths(1).AddDays(-1);
        }

       

        public static bool InTwoWeekPeriod(this DateTime periodStart, DateTime dateToCheck)
        {
            if (dateToCheck.Date >= periodStart.Date)
                return (dateToCheck.Date - periodStart.Date).TotalDays < 14;

            return false;
        }

        public static bool InCurrentMonth(this DateTime actualMonth, DateTime dateToCheck)
        {
            return actualMonth.Year == dateToCheck.Year && dateToCheck.Month == dateToCheck.Month;
        }

        public static string HebrewMonthName(this DateTime cMonth)
        {
            return cMonth.ToString("MMMM", CultureHelper.HebrewCulture);
        }

        public static string HebrewMonthNameWithYear(this DateTime cMonth)
        {
            return cMonth.ToString("MMMM", CultureHelper.HebrewCulture) + " " + cMonth.Year.ToString();
        }

        public static string HebrewDayName(this DateTime cMonth)
        {
            return CultureHelper.HebrewCulture.DateTimeFormat.DayNames[(int)cMonth.DayOfWeek];
        }

        public static string HebrewDayAndMonth(this DateTime cMonth)
        {
            return CultureHelper.HebrewCulture.DateTimeFormat.DayNames[(int)cMonth.DayOfWeek] +  " " + cMonth.Day.ToString() + " " + CultureHelper.HebrewCulture.DateTimeFormat.MonthNames[cMonth.Month];
        }

        public static string ToInputFormat(this DateTime cMonth)
        {
            return cMonth.ToString("yyyy-MM-dd");
        }

        public static bool Empty(this DateTime cMonth)
        {
            return cMonth == null || cMonth == DateTime.MinValue;
        }

        public static bool NotEmpty(this DateTime cMonth)
        {
            return cMonth != null && cMonth != DateTime.MinValue;
        }

        public static int NumberOfDays(this DateTime cMonth)
        {
            var tmpm = new DateTime(cMonth.Year, cMonth.Month, 1);
            var tmpmEnd = tmpm.AddMonths(1).AddDays(-1);
            
            return tmpmEnd.Day;
        }

        public static string FormattedString(this int integer)
        {
            return string.Format("{0:n0}", integer);
        }

        public static int ToInteger(this double d)
        {
            return Convert.ToInt32(d);
        }

        public static bool InRangeNoBorders(this List<int> lst, int bottom, int top)
        {
            var res = true;
            foreach (var n in lst)
            {
                if (!n.InRangeNoBorders(bottom, top))
                    res = false;
            }
            return res;
        }

        public static bool InRangeNoBorders(this int n, int bottom, int top)
        {
            return n > bottom && n < top;
        }

        public static int ToInteger(this string d)
        {
            var dd = string.Empty;
            foreach (var c in d.Where(x => x.IsInteger()))
                dd += c;
            return Convert.ToInt32(dd);
        }

        public static T ToEnum<T> (this string d) 
        {
            return (T)Enum.Parse(typeof(T), d, true);
        }

        public static bool ToBoolean(this string d)
        {
            return Boolean.Parse(d);
        }

        public static bool IsInteger(this string d)
        {
            var dd = 0;
            return int.TryParse(d,out dd);
        }

        public static bool IsDateTime(this string d)
        {
            DateTime dd = DateTime.MinValue;
            return DateTime.TryParse(d, out dd);
        }

        public static bool IsInteger(this char a)
        {
            return a >= '0' && a <= '9';
        }

        public static int ToInteger(this decimal d)
        {
            return Convert.ToInt32(d);
        }

        public static bool IsEmpty(this IEnumerable d)
        {
            if (d == null)
                return true;
            bool any = true;
            foreach (var e in d)
            {
                any = false;
                break;
            }
            return any;
        }

        public static bool IsEmpty(this string d)
        {
            return string.IsNullOrEmpty(d);
        }

        public static bool NotEmpty(this string d)
        {
            return !string.IsNullOrEmpty(d);
        }

        public static DateTime ToDateTime(this string d) 
        {
            DateTime dat = DateTime.MinValue;
            try
            {
                dat = DateTime.Parse(d);
                return dat;
            }
            catch (Exception)
            {

            }
            return DateTime.Parse(d, CultureInfo.InvariantCulture);
        }

        public static bool IsDefault(this DateTime d)
        {
            return d == DateTime.MinValue;
        }

        public static bool NotEmpty(this IEnumerable d)
        {
            if (d == null)
                return false;
            bool any = false;
            foreach (var e in d)
            {
                any = true;
                break;
            }
            return any;
        }

        public static bool NotNull(this object d)
        {
            return d != null;
        }

        public static bool NotZero(this int d)
        {
            return d != 0;
        }

        public static string GetDescription(this Enum e)
        {
            return GenericFormatter.GetEnumDescription(e);
        }

        public static string ToTimeDisplay(this WorkTime d)
        {
            TimeSpan sp = new TimeSpan(d.Hour, d.Minute, 0);
            return sp.ToString(@"hh\:mm");
        }

        public static string ToTimeDisplay(this TimeSpan sp)
        {
            return sp.ToString(@"hh\:mm");
        }

        public static string ToXml(this object d)
        {
            if (d == null)
                return null;
            return XmlHelper.ToXml(d);
        }
        

        public static void SetFromDbXmlField<T>(this IDataReader reader,object obj, string dbFieldName, PropertyInfo pinfo)
        {
            if (pinfo != null)
            {
                var xmlData = DataAccessUtility.LoadNullable<string>(reader, dbFieldName);
                if (xmlData.NotEmpty())
                    pinfo.SetValue(obj, XmlHelper.CreateFromXml<T>(xmlData));
                else
                    pinfo.SetValue(obj, Activator.CreateInstance<T>());
            }
        }

        public static T ParseXml<T>(this string str)
        {
            if (str == null)
                throw new Exception("No Xml string to parse");
            return XmlHelper.CreateFromXml<T>(str);
        }

        public static PropertyInfo GetPropertyInfo(this object obj, string propertyName)
        {
            PropertyInfo result = obj.GetType().GetProperty(propertyName);
            return result;
        }

        //public static T GetAttribute<T>(this PropertyInfo prop)
        //{
        //    var genericDataField = prop.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        //    return genericDataField;
        //}

        public static void FindControlRecursive(this System.Web.UI.Control c, string cotrolToFind, ref System.Web.UI.Control resp)
        {
            if (c.ID == cotrolToFind)
            {
                resp = c;
                return;
            }
            else if (c.Controls.NotEmpty())
                foreach (System.Web.UI.Control ctr in c.Controls)
                    ctr.FindControlRecursive(cotrolToFind,ref resp);
        }

        public static string HebrewDayName(this DayOfWeek d)
        {
            return CultureHelper.HebrewCulture.DateTimeFormat.GetDayName(d);
        }

        public static void FillControlValues(this System.Web.UI.Control c, ref Dictionary<string,string> data)
        {
            if (c is HtmlInputText)
            {
                data.Add(c.UniqueID, (c as HtmlInputText).Value);
            }
            else if (c.Controls.NotEmpty())
                foreach (System.Web.UI.Control ctr in c.Controls)
                    ctr.FillControlValues(ref data);
        }


    }
}