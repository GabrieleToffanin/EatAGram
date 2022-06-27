using System.ComponentModel.DataAnnotations;

namespace Eatagram.Core.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public string User_Id { get; set; }
        public ApplicationUser Owner { get; set; }

    }
}