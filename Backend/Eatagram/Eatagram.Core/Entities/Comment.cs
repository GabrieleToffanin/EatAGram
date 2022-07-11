using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255), MinLength(3)]
        public string Content { get; set; }
        public int UpVoted { get; set; }


        public string User_Id { get; set; }
        public ApplicationUser User { get; set; }

        public int RecipeId { get; set; }
        public Recipe OfRecipe { get; set; }

    }
}
