using System.ComponentModel.DataAnnotations;

namespace GAMA.CO5.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "الموضوع مطلوب")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرسالة مطلوبة")]
        public string Message { get; set; } = string.Empty;
    }
}