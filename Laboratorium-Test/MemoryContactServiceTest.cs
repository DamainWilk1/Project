using Laboratorium_3___App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorium_Test
{
    public class MemoryContactServiceTest
    {

        [Fact]
        public void Add_NewContact_ReturnsNewId()
        {
            var service = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Name = "John" };

            service.add(contact);

            var retrievedContact = service.FindAll().FirstOrDefault(c => c.Name == "John");
            Assert.NotNull(retrievedContact);
            Assert.True(retrievedContact.Id > 0);
        }

        [Fact]
        public void FindById_ExistingContact_ReturnsContact()
        {
            var service = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            service.add(contact);
            var retrievedContact = service.FindByID(contact.Id);

            Assert.NotNull(retrievedContact);
            Assert.Equal(contact.Id, retrievedContact.Id);
        }

        [Fact]
        public void FindAll_ReturnsAllContacts()
        {
            var service = new MemoryContactService(new DateTimeProviderImplementation());
            var contact1 = new Contact { Id = 1, Name = "John" };
            var contact2 = new Contact { Id = 2, Name = "Alice" };
            service.add(contact1);
            service.add(contact2);

            var contacts = service.FindAll();
            Assert.Equal(2, contacts.Count);
        }

        [Fact]
        public void Delete_ExistingContact_RemovesContact()
        {
            var service = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            service.add(contact);

            service.RemoveByID(contact.Id);
            Assert.Null(service.FindByID(contact.Id));
        }

        [Fact]
        public void Delete_NonExistingContact_DoesNothing()
        {
            var service = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            service.add(contact);
            service.RemoveByID(2); // Attempt to delete a non-existing contact

            Assert.NotNull(service.FindByID(contact.Id));
        }
    }
}
