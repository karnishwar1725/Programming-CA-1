using System;
using System.Collections.Generic;

namespace PhoneBookConsoleApp
{
    class PhoneBook
    {
        private List<Contact> contacts = new List<Contact>();

        public PhoneBook()
        {
            SeedContacts(); 
        }

        private void SeedContacts()
        {
            contacts.Add(new Contact("Emily Blackwell", "871111111"));

            for (int i = 2; i <= 20; i++)
            {
                string name = "Contact " + i;
                string number = (800000000 + i).ToString();
                contacts.Add(new Contact(name, number));
            }
        }

        public void AddContact(Contact c)
        {
            if (!IsValidNumber(c.Number))
            {
                Console.WriteLine("Invalid number. Must be 9-digit, non-zero.");
                return;
            }
            contacts.Add(c);
            Console.WriteLine("Contact added.");
        }

        public void AddContact(string name, string number)
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
                Console.WriteLine($"Name: {found.Name}, Number: {found.Number}");
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
                Console.WriteLine($"Name: {c.Name}, Number: {c.Number}");
            }
        }

        public void DisplayMatchingContacts(string searchPhrase)
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
                    c.Number = newNumber;
            }

            return true;
        }

        public bool DeleteContact(string number)
        {
            Contact c = FindByNumber(number);
            if (c == null) return false;
            contacts.Remove(c);
            return true;
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

            return true;
        }
    }
}