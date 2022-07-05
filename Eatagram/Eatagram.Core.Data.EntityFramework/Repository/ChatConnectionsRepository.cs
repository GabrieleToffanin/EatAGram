using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Comments;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Data.EntityFramework.Repository
{
    public class ChatConnectionsRepository : IChatConnectionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatConnectionsRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ValueTask AddUserToExistingRoom(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<ApplicationUser>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
