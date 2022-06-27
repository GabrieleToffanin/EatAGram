using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Repository
{
    public interface IChatConnectionRepository
    {
        ValueTask<IEnumerable<ApplicationUser>> GetUsers();
        ValueTask AddUserToExistingRoom(ApplicationUser user);
    }
}
