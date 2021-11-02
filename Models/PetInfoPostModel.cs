using Microsoft.AspNetCore.Http;

namespace pediagnoswebapi.Models
{
    public class PetInfoPostModel
    {
        public string Ad { get; set; }
        public int? Yas { get; set; }
        public int? OwnerId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}