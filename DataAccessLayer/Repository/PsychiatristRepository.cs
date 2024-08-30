using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class PsychiatristRepository : IPsychiatristRepository
    {
        private readonly AppDbContext _context;

        public PsychiatristRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Psychiatrist> GetPsychiatristById(int id)
        {
            return await _context.Psychiatrists.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
