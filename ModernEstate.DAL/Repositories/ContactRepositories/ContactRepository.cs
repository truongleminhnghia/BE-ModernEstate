
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ContactRepositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Guid> GetOrCreateAsync(Contact contact)
        {
            var existing = await _context.contacts.FirstOrDefaultAsync(c =>
                c.ContactName == contact.ContactName
                && c.ContactEmail == contact.ContactEmail
                && c.ContactPhone == contact.ContactPhone
            );
            if (existing != null)
                return existing.Id;

            await _context.contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }
    }
}
