using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Entities.Authentication
{
    public class RegistrationStatus
    {
        public string Message { get; set; }
        public bool Succedeed { get; set; }
        public string Email { get; set; }
    }
}
