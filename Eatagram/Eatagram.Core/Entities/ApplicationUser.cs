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

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
