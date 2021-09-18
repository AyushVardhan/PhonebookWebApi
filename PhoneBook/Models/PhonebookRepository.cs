using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PhoneBook.Models
{
    public class PhonebookRepository : IPhonebookRepository
    {

        private readonly ILogger _logger;
        private Dictionary<string, string> phoneDictionary = new Dictionary<string, string>();

        public PhonebookRepository(ILogger<PhonebookRepository> logger)
        {
            _logger = logger;
            phoneDictionary.Add("Ayush Vardhan", "8320069702");
        }

        public Boolean Add(Contact item)
        {
            if (phoneDictionary.ContainsKey(item.Name)) {
                return false;
            }
            phoneDictionary.Add(item.Name, item.Number);
            return true;
        }

        public IEnumerable<Contact> GetAll()
        {
            List<Contact> contactList = new List<Contact>();
            foreach (KeyValuePair<string, string> entry in phoneDictionary)
            {
                Contact contact = new Contact();
                contact.Name = entry.Key;
                contact.Number = entry.Value;

                contactList.Add(contact);
            }

            return contactList;
        }

        public string Find(string itemName) {
            try
            {
                return phoneDictionary[itemName];
            }
            catch (KeyNotFoundException exception) {
                _logger.LogError("Error is :: " + exception.Message);
                return null;
            }
        }

        public string Delete(string itemName)
        {

            string name = null;

            if (phoneDictionary.ContainsKey(itemName)) {
                name = itemName;
                phoneDictionary.Remove(itemName);
            }

            return name;
        }

        public void Update(Contact item)
        {
            if (phoneDictionary.ContainsKey(item.Name)) {
                phoneDictionary[item.Name] = item.Number;
                return;
            }

            phoneDictionary.Add(item.Name, item.Number);
        }
    }
}
