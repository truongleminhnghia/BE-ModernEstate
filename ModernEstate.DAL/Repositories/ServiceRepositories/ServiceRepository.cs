using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ServiceRepositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<IEnumerable<Service>> FindServicesAsync(EnumTypeService? serviceType)
        {
            IQueryable<Service> query = _context.Services;

            if (serviceType.HasValue)
                query = query.Where(s => s.TypeService == serviceType.Value);

            return await query.OrderByDescending(s => s.CreatedAt).ToListAsync();
        }
    }
}
