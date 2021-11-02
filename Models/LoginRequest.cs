using System.ComponentModel.DataAnnotations;

namespace pediagnoswebapi.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="Bu alan gerekli")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Bu alan gerekli")]
        public string Password { get; set; }
    }
}