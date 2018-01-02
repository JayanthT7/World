using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WebApplication1.Services
{
    public class DebugMailService : Controllers.Web.IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail : To: {to} From: { from} subject: {subject}");
        }
    }
}