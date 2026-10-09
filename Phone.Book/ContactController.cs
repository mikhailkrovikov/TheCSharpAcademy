using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Book
{
    public class ContactController
    {
        private readonly ContactRepository _repository;
        public ContactController()
        {
            _repository = new ContactRepository();
        }

        public List<GetContactDTO> GetAllContacts()
        {
            var contacts = _repository.GetAllContacts();
            var contactDTOs = new List<GetContactDTO>();
            foreach (var contact in contacts)
            {
                contactDTOs.Add(new GetContactDTO
                {
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email
                });
            }
            return contactDTOs;
        }

        public async Task AddContact(ContactEntity contact)
        {
            await _repository.AddContact(contact);
        }
    }
}
