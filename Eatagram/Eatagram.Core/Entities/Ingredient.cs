using System.ComponentModel.DataAnnotations;

namespace Eatagram.Core.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}