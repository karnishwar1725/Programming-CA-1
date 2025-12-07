using System;

namespace PhoneBookConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var phoneBook = new PhoneBook();

            Console.WriteLine("Welcome to 1Console PhoneBook app");

            while (true)
            {
                Console.WriteLine("\nSelect operation");
                Console.WriteLine("1 Add contact");
                Console.WriteLine("2 Display contact by number");
                Console.WriteLine("3 View all contacts");
                Console.WriteLine("4 Search for contacts for a given name");
                Console.WriteLine("5 Update contact");
                Console.WriteLine("6 Delete contact");
                Console.WriteLine("Press 'x' to exit");

                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Contact name:");
                        var name = Console.ReadLine();

                        Console.WriteLine("Contact number (9-digit, non-zero):");
                        var number = Console.ReadLine();

                        var newContact = new Contact(name, number);
                        phoneBook.AddContact(newContact);
                        break;

                    case "2":
                        Console.WriteLine("Contact number to search:");
                        var searchNumber = Console.ReadLine();
                        phoneBook.DisplayContact(searchNumber);
                        break;

                    case "3":
                        phoneBook.DisplayAllContact();
                        break;

                    case "4":
                        Console.WriteLine("Name search phrase:");
                        var searchPhrase = Console.ReadLine();
                        phoneBook.DisplayMatchingContacts(searchPhrase);
                        break;

                    case "5":
                        Console.WriteLine("Enter current contact number to update:");
                        var oldNumber = Console.ReadLine();

                        Console.WriteLine("New name (leave blank to keep same):");
                        var newName = Console.ReadLine();

                        Console.WriteLine("New number (leave blank to keep same):");
                        var newNumber = Console.ReadLine();

                        if (phoneBook.UpdateContact(oldNumber, newName, newNumber))
                            Console.WriteLine("Contact updated.");
                        else
                            Console.WriteLine("Contact not found.");
                        break;

                    case "6":
                        Console.WriteLine("Enter contact number to delete:");
                        var delNumber = Console.ReadLine();

                        if (phoneBook.DeleteContact(delNumber))
                            Console.WriteLine("Contact deleted.");
                        else
                            Console.WriteLine("Contact not found.");
                        break;

                    case "x":
                        return;

                    default:
                        Console.WriteLine("Select valid operation");
                        break;
                }
            }
        }
    }
}