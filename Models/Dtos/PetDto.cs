namespace pediagnoswebapi.Models.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Yas { get; set; }
        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; }
    }
}