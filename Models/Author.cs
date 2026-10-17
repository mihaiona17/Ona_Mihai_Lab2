using System.ComponentModel.DataAnnotations;

namespace Ona_Mihai_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName)
            ? string.Empty
            : (FirstName + " " + LastName).Trim();

        public ICollection<Book>? Books { get; set; }
    }
}
