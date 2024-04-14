using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class CreatePostViewModel
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required(ErrorMessage = "Please select an item")]
        public int ItemId { get; set; } // Assuming ItemId is of type int

    }
}
