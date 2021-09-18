using System;
using System.Collections.Generic;

namespace PhoneBook.Models
{
    public interface IPhonebookRepository
    {
        Boolean Add(Contact item);
        IEnumerable<Contact> GetAll();
        void Update(Contact item);
        string Delete(string itemName);
        string Find(string itemName);
    }
}
