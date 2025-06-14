
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PropertyRepositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Property?> FindByCode(string code)
        {
            return await _context.Properties.Include(p => p.Address)
                                        .Include(p => p.Owner)
                                        .Include(p => p.PropertyImages)
                                        .Include(p => p.Histories)
                                        .Include(p => p.Posts)
                                        .Include(p => p.Favorites)
                                        .FirstOrDefaultAsync(p => p.Code.Equals(code));
        }

        public async Task<Property?> FindById(Guid id)
        {
            return await _context.Properties.Include(p => p.Address)
                                        .Include(p => p.Owner)
                                        .Include(p => p.PropertyImages)
                                        .Include(p => p.Histories)
                                        .Include(p => p.Posts)
                                        .Include(p => p.Favorites)
                                        .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<Property?> FindByTitle(string title)
        {
            return await _context.Properties.Include(p => p.Address)
                                        .Include(p => p.Owner)
                                        .Include(p => p.PropertyImages)
                                        .Include(p => p.Histories)
                                        .Include(p => p.Posts)
                                        .Include(p => p.Favorites)
                                        .FirstOrDefaultAsync(p => p.Title.Equals(title));
        }

        public async Task<IEnumerable<Property>> FindProperties(string? title, double? minPrice, double? maxPrice, EnumTypeProperty? typeProperty,
                                                                float? minArea, float? maxArea, int? numberOfBedroom, int? numberOfBathroom,
                                                                int? numberOfFloor, int? numberOfRoom, EnumStateProperty? state, EnumStatusProperty? status
        )
        {
            IQueryable<Property> query = _context.Properties
                .Include(p => p.Address)
                .Include(p => p.Owner)
                .Include(p => p.PropertyImages)
                .Include(p => p.Histories)
                .Include(p => p.Posts)
                .Include(p => p.Favorites);
            // if (typeProperty.HasValue)
            //     query = query.Where(p => p.TypeProperty == typeProperty.Value);
            // if (!string.IsNullOrWhiteSpace(title))
            //     query = query.Where(p => EF.Functions.Like(p.Title, $"%{title}%"));
            // if (minPrice.HasValue)
            //     query = query.Where(p => p.SalePrice >= minPrice.Value);
            // if (maxPrice.HasValue)
            //     query = query.Where(p => p.SalePrice <= maxPrice.Value);
            // if (minArea.HasValue)
            //     query = query.Where(p => p.PropertyArea >= minArea.Value);
            // if (maxArea.HasValue)
            //     query = query.Where(p => p.PropertyArea <= maxArea.Value);
            // if (numberOfBedroom.HasValue)
            //     query = query.Where(p => p.NumberOfBedroom == numberOfBedroom.Value);
            // if (numberOfBathroom.HasValue)
            //     query = query.Where(p => p.NumberOfBathroom == numberOfBathroom.Value);
            // if (numberOfFloor.HasValue)
            //     query = query.Where(p => p.NumberOfFloor == numberOfFloor.Value);
            // if (numberOfRoom.HasValue)
            //     query = query.Where(p => p.NumberOfRoom == numberOfRoom.Value);
            // if (state.HasValue)
            //     query = query.Where(p => p.State == state.Value);
            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);
            // if (attributes != null && attributes.Length > 0)
            // {
            //     query = query.Where(p =>
            //         p.Attributes.Any(a => attributes.Contains(a.Name))
            //     );
            // }
            query = query.OrderByDescending(p => p.CreatedAt);
            return await query.ToListAsync();
        }
    }

}
