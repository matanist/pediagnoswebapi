using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pediagnoswebapi.Models;
using pediagnoswebapi.Models.DB;
using pediagnoswebapi.Models.Dtos;

namespace pediagnoswebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeterinerController : Controller
    {
        private readonly PediagnosDBContext _context;
        private readonly IMapper _mapper;
        public VeterinerController(PediagnosDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ApiResponse Get()
        {
            // var tumPetler = _context.Pets.Include(p => p.Owner).Select(p => new
            // {
            //     p.Ad,
            //     p.Yas,
            //     Owner = new
            //     {
            //         Ad = p.Owner.Ad,
            //         Soyad = p.Owner.Soyad
            //     }
            // }).ToList();
            var tumPetler = _context.Pets.Include(p => p.Owner).Select(p => _mapper.Map<PetDto>(p)).ToList();

            return new ApiResponse { Code = 200, Message = "Success", Set = tumPetler };
        }
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            var seciliPet = _context.Pets.Include(p => p.Owner).FirstOrDefault(p => p.Id == id);
            var petDto = _mapper.Map<PetDto>(seciliPet);
            return new ApiResponse { Code = 200, Message = "Başarılı", Set = petDto };
        }
        [HttpPost]
        public ApiResponse Post(Pet pet)
        {
            if (pet == null) return new ApiResponse { Code = 400, Message = "Pet null olamaz" };

            _context.Pets.Add(pet);
            _context.SaveChanges();
            return new ApiResponse { Code = 200, Message = "Başarılı", Set = pet };
        }
        [HttpPut]
        public ApiResponse Put(Pet pet)
        {
            if (pet == null) return new ApiResponse { Code = 400, Message = "Pet null olamaz" };

            _context.Pets.Update(pet);
            _context.SaveChanges();
            return new ApiResponse{Code=200, Message="Başarılı", Set=pet};
        }
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id){
            var silinecekPet = _context.Pets.FirstOrDefault(p=>p.Id==id);
            _context.Pets.Remove(silinecekPet);
            _context.SaveChanges();
            return new ApiResponse{Code=200, Message="Başarılı", Set=silinecekPet};
        }

    }
}