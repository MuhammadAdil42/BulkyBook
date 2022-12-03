using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order should be between 1 and 100!")]
        public int DisplayOrder { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    }
}
