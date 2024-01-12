using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMSetup.Email.Dtos
{
    public class ForSentEmailDto
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string MailAddressTo { get; set; }
        public string MailAddressFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CCEmail { get; set; }
    }
}
