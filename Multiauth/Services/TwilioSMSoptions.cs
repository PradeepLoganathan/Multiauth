using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiauth.Services
{
    public class TwilioSMSoptions
    {
        public string SMSAccountIdentification { get; set; }
        public string SMSAccountPassword { get; set; }
        public string SMSAccountFrom { get; set; }
    }
}
