using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiauth.Services
{
    public class AuthMessageOptions
    {
        private TwilioSMSoptions _SMSoptions;
        private SendGridOptions _EmailOptions;

        public AuthMessageOptions()
        {
            _SMSoptions = new TwilioSMSoptions();
            _EmailOptions = new SendGridOptions();
        }

        public TwilioSMSoptions SMSOptions
        { get { return _SMSoptions; } }

        public SendGridOptions  EmailOptions{ get { return _EmailOptions; } }

    }
}
