using System;
using System.Collections.Generic;

#nullable disable

namespace pediagnoswebapi.Models.DB
{
    public partial class User
    {
        public User()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
