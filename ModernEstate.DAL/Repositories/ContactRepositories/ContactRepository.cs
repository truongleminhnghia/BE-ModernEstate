
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ContactRepositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Contact?> FindByEmail(string contactEmail)
        {
            return await _context.contacts.FirstOrDefaultAsync(p => p.ContactEmail.Equals(contactEmail));
        }

        public async Task<Contact?> FindByPhone(string phone)
        {
            return await _context.contacts.FirstOrDefaultAsync(p => p.ContactPhone.Equals(phone));
        }
    }
}
