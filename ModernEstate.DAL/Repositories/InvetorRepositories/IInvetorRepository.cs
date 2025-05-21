
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.InvetorRepositories
{
    public interface IInvetorRepository : IGenericRepository<Invetor>
    {
        Task<Invetor?> FindById(Guid id);
        Task<Invetor?> FindByName(string name);
        Task<Invetor?> FindByEmail(string Email);
        Task<IEnumerable<Invetor>> FindInvetors(string? name, string? companyName,
                                                EnumInvetorType? invetorType, string email);
    }
}
