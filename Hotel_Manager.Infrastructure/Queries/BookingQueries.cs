using Dapper;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Queries
{
    public class BookingQueries
    {
        private readonly IDbConnectionFactory _factory;

        public BookingQueries(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<BookingEntity>> GetLastBookingsAsync(int limit = 5)
        {
            using var conn = _factory.CreateConnection();
            var sql = @"SELECT * FROM Bookings 
                        ORDER BY CreatedAt DESC 
                        LIMIT @Limit;";
            return await conn.QueryAsync<BookingEntity>(sql, new { Limit = limit });
        }
    }
}
