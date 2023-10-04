using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Required]
        [Range(1,100, ErrorMessage = "The field Display Order field must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
