using Microsoft.EntityFrameworkCore;

namespace Phone.Book
{
    public class ContactRepository
    {
        public List<ContactEntity> GetAllContacts()
        {
            using (var context = new AppDbContext())
            {
                return context.Contacts.ToList();
            }
        }

        public async Task AddContact(ContactEntity contact)
        {
            using (var context = new AppDbContext())
            {
                await context.Contacts.AddAsync(contact);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ContactEntity?> GetContact(int id)
        {
            using (var context = new AppDbContext())
            {
                return await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ContactEntity?> GetContactByNane(string name)
        {
            using (var context = new AppDbContext())
            {
                return await context.Contacts.FirstOrDefaultAsync(x => x.Name.Equals(name));
            }
        }

        public async Task<ContactEntity?> GetContactByEmail(string email)
        {
            using (var context = new AppDbContext())
            {
                return await context.Contacts.FirstOrDefaultAsync(x => x.Email.Equals(email));
            }
        }

        public async Task<ContactEntity?> GetContactByPhoneNumber(string phoneNumber)
        {
            using (var context = new AppDbContext())
            {
                return await context.Contacts.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(phoneNumber));
            }
        }

        public async Task UpdateContact(int contactId, ContactEntity editedContact)
        {
            using (var context = new AppDbContext())
            {
                var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
                if (contact == null)
                {
                    throw new InvalidOperationException($"Contact with id {contactId} not found");
                }
                contact.Name = editedContact.Name;
                contact.Email = editedContact.Email;
                contact.PhoneNumber = editedContact.PhoneNumber;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteContact(int contactId)
        {
            using (var context = new AppDbContext())
            {
                var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
                if (contact == null)
                {
                    throw new InvalidOperationException($"Contact with id {contactId} not found");
                }
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
            }
        }
    }
}
