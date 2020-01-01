﻿using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CalendarUtilities
{
    public static class CalendarEventManager
    {
        public static void SendCalendarByMail(CalendarMailRequest mailRequest, List<MyCalendarEvent> calendarEvents)
        {
            MailMessage message = new MailMessage();
            foreach (string item in mailRequest.To)
                message.To.Add(item);

            message.From = new MailAddress(mailRequest.FromEmail, mailRequest.FromName);
            message.Subject = mailRequest.Subject;
            message.Body = mailRequest.HtmlBody;
            message.IsBodyHtml = true;

            var serializedCalendar = generateCalendarFile(calendarEvents);
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(serializedCalendar));
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(ms, "event.ics", "text/calendar");
            message.Attachments.Add(attachment);

            SmtpClient MailClient = new SmtpClient("smtp.gmail.com");
            MailClient.EnableSsl = true;
            MailClient.Credentials = mailRequest.NetworkCredential;
            MailClient.Send(message);
        }

        public static void DownloadCalendarFile(HttpContext httpContext, List<MyCalendarEvent> calendarEvents)
        {
            var Response = httpContext.Response;
            var CalendarItemAsString = generateCalendarFile(calendarEvents);
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/calendar";
            Response.AddHeader("content-length", CalendarItemAsString.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=event.ics");
            Response.Write(CalendarItemAsString);
            Response.Flush();
            httpContext.ApplicationInstance.CompleteRequest();
        }

        private static string generateCalendarFile(List<MyCalendarEvent> calendarEvents)
        {
            var calendar = new Ical.Net.Calendar();
            //calendar.Name = "Smach - Calendar";
            foreach (MyCalendarEvent res in calendarEvents)
            {
                var ca = new CalendarEvent
                {
                    Class = "PUBLIC",
                    //Name = "Smach - Calendar",
                    Summary = res.SummaryText,
                    Created = new CalDateTime(DateTime.Now),
                    Description = res.Details,
                    Start = new CalDateTime(Convert.ToDateTime(res.BeginDate)),
                    End = new CalDateTime(Convert.ToDateTime(res.EndDate)),
                    Sequence = 0,
                    Uid = Guid.NewGuid().ToString(),
                    Location = res.LocationText,

                };
                var alarm = new Alarm()
                {
                    Summary = res.SummaryText,
                    Trigger = new Trigger(TimeSpan.FromMinutes(-15)),
                    Action = AlarmAction.Display
                };

                calendar.Events.Add(ca);
            }
            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);
            //var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
            return serializedCalendar;
        }
    }
}