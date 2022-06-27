using Eatagram.Core.Entities.Chat;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace Eatagram.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }



        [IgnoreDataMember]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public ICollection<Connection> MyProperty { get; set; }

        public virtual ICollection<ConversationRoom> Rooms { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
