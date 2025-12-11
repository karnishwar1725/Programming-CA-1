using System;
using System.Collections.Generic;

namespace PhoneBookConsoleApp
{
    class PhoneBook
    {
        private List<Contact> contacts = new List<Contact>(); // this is a list that stores all contact objects

        public PhoneBook() //this constructor runs when phonebook is created
        {
            SeedContacts(); // Loads 20 contacts when we start running the code
        }

        private void SeedContacts()   //this command creates atleast 20 contacts
        {
            contacts.Add(new Contact("Emily Blackwell", "871111111"));

            for (int i = 2; i <= 20; i++)
            {
                string name = "Contact " + i;
                string number = (800000000 + i).ToString();
                contacts.Add(new Contact(name, number));
            }
        }

        public void AddContact(Contact c) // Adds contact using Contact Object
        {
            if (!IsValidNumber(c.Number))
            {
                Console.WriteLine("Invalid number. Must be 9-digit, non-zero.");
                return;    // Condition to validate number before adding
            } 
            contacts.Add(c);
            Console.WriteLine("Contact added.");
        }

        public void AddContact(string name, string number) // In thisw method overloading is used to save a contact with name and number
        {
            AddContact(new Contact(name, number));
        }

        public void DisplayContact(string number)
        {
            Contact found = FindByNumber(number);
            if (found == null)
            {
                Console.WriteLine("Contact not found.");
            }
            else
            {
                Console.WriteLine($"Name: {found.Name}, Number: {found.Number}");   // Condition for Displaying contact info with number 
            }
        }

        public void DisplayAllContact()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts.");
                return;
            }

            foreach (var c in contacts)
            {
                Console.WriteLine($"Name: {c.Name}, Number: {c.Number}");  // Shows all contacts in the list
            }
        }

        public void DisplayMatchingContacts(string searchPhrase)  // allows searching contact numbers with name
        {
            bool any = false;
            foreach (var c in contacts)
            {
                if (!string.IsNullOrEmpty(searchPhrase) &&
                    c.Name.IndexOf(searchPhrase, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"Name: {c.Name}, Number: {c.Number}");
                    any = true;
                }
            }

            if (!any)
                Console.WriteLine("No matching contacts.");
        }

        public bool UpdateContact(string oldNumber, string newName, string newNumber)
        {
            Contact c = FindByNumber(oldNumber);
            if (c == null) return false;

            if (!string.IsNullOrWhiteSpace(newName))
                c.Name = newName;

            if (!string.IsNullOrWhiteSpace(newNumber))
            {
                if (!IsValidNumber(newNumber))
                    Console.WriteLine("Invalid new number. Keeping old one.");
                else
                    c.Number = newNumber;     // Edit contact Logic
            }

            return true;
        }

        public bool DeleteContact(string number)
        {
            Contact c = FindByNumber(number);
            if (c == null) return false;
            contacts.Remove(c);
            return true;     // Delete contact Logic
        } 

        private Contact FindByNumber(string number)
        {
            foreach (var c in contacts)
            {
                if (c.Number == number)
                    return c;
            }
            return null;
        }
        private bool IsValidNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;
            if (number.Length != 9) return false;
            if (number == "000000000") return false;

            foreach (char ch in number)
            {
                if (!char.IsDigit(ch)) return false;
            }

            return true; // Logic for Number Validation
        }
    }
}