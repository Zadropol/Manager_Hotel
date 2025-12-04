using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Repositories
{
    public class SecurityRepository
    {
        private readonly HotelManagerContext _context; // Ajusta el nombre de tu DbContext

        public SecurityRepository(HotelManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            // Busca por Login y Password (recordatorio: usar Hash en un proyecto real)
            return await _context.Set<Security>().FirstOrDefaultAsync(
                x => x.Login == login.User && x.Password == login.Password);
        }
    }
}
