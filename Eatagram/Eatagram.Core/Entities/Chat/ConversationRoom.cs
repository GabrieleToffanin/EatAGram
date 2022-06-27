using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Entities.Chat
{
    public class ConversationRoom
    {
        [Key]
        public string RoomName { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
