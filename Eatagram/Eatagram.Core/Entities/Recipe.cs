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

        public virtual ICollection<Ingredient> Ingredients { get; set; }


        public virtual string User_Id { get; set; }
        public virtual ApplicationUser Owner { get; set; }

    }
}