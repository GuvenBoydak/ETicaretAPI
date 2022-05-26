using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Etities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var quary = Table.AsQueryable();
            if (!tracking)
                quary = quary.AsNoTracking();
            return quary;          
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var quary = Table.AsQueryable();
            if (!tracking)
                quary = quary.AsNoTracking();
            return await quary.FirstOrDefaultAsync(x => x.ID == Guid.Parse(id));
        }
            
        
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var quary = Table.AsQueryable();
            if (!tracking)
                quary = quary.AsNoTracking();
            return await quary.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var quary = Table.Where(method);
            if (!tracking)
                quary = quary.AsNoTracking();
            return quary;
        }
    }
}
