using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pediagnoswebapi.Models;
using pediagnoswebapi.Models.DB;
using pediagnoswebapi.Models.Dtos;

namespace pediagnoswebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : Controller
    {
        private readonly PediagnosDBContext _context;
        private readonly IMapper _mapper;

        public OwnerController(PediagnosDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ApiResponse Get()
        {
            var allOwners = _context.Users.Select(u => _mapper.Map<OwnerDto>(u)).ToList();
            return new ApiResponse { Code = 200, Message = "Başarılı", Set = allOwners };
        }
    }
}