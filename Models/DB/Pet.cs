using System;
using System.Collections.Generic;

#nullable disable

namespace pediagnoswebapi.Models.DB
{
    public partial class Pet
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int? Yas { get; set; }
        public int? OwnerId { get; set; }
        public byte[] PetImage { get; set; }

        public virtual User Owner { get; set; }
    }
}
