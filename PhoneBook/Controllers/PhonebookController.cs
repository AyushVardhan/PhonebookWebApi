using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{

    [Route("api/[controller]")]
    public class PhonebookController : Controller
    {

        private readonly ILogger _logger;
        public IPhonebookRepository PhonebookRepository { get; set; }

        public PhonebookController(IPhonebookRepository phonebook, ILogger<PhonebookRepository> logger)
        {
            PhonebookRepository = phonebook;
            _logger = logger;
        }


        public IEnumerable<Contact> GetAll() {
            return PhonebookRepository.GetAll();
        }

        [Route("contact/add")]
        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact newContact)
        {
            _logger.LogInformation("Received data :: {} , {}", newContact.Name, newContact.Number);
            Contact newItem = new Contact();
            newItem.Number = newContact.Number;
            newItem.Name = newContact.Name;

            bool addAction = PhonebookRepository.Add(newItem);
            if (addAction) {
                return CreatedAtAction("CreateContact", "Saved successfully!");
            }

            return BadRequest("Duplpicate Entry! Try to call update API.");
        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name) {

            var item = PhonebookRepository.Find(name);
            if (null == item) {
                return StatusCode(StatusCodes.Status202Accepted, "Not found!");
            }

            return Json(item);
        }

        [HttpDelete("delete/{name}", Name = "DeleteByName")]
        public IActionResult DeleteContact(string name) {
            var deletedEntry = PhonebookRepository.Delete(name);
            if (null == deletedEntry) {
                return BadRequest("Contact not found!");
            }

            return StatusCode(StatusCodes.Status202Accepted, "Deleted!");
        }

        [Route("contact/update")]
        [HttpPut]
        public IActionResult UpdateContact([FromBody] Contact newContact) {
            Contact newItem = new Contact();
            newItem.Number = newContact.Number;
            newItem.Name = newContact.Name;

            PhonebookRepository.Update(newItem);
            return StatusCode(StatusCodes.Status205ResetContent, "Updated!");
        }
    }
}
