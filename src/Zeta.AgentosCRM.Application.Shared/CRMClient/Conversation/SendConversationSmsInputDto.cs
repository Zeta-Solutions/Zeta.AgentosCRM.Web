using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMClient.Conversation
{
    public class SendConversationSmsInputDto
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
