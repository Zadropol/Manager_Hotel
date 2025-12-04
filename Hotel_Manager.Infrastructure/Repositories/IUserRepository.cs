using FluentValidation;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Repositories
{
    public interface ISecurityRepository : IBaseRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
