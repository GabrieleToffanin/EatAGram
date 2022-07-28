using Eatagram.SDK.Models.Authentication;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.WPF.Events
{
    public class SuccessAuthenticationEvent : PubSubEvent<AuthenticationToken>
    {

    }
}
