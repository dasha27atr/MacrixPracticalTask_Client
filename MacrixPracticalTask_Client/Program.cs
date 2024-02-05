using ConsoleTables;
using MacrixPracticalTask_Client.API;
using MacrixPracticalTask_Client.Generator;
using MacrixPracticalTask_Client.Models;
using MacrixPracticalTask_Client.Models.DTO;
using MacrixPracticalTask_Client.Validation;

namespace MacrixPracticalTask_Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            API_Client apiClient = new();

            while (true)
            {
                Console.WriteLine("Hello! " +
                "\nThis application was developed for demonstration uses of a practical task for Macrix company. " +
                "\nIt allows access to REST API methods developed within the technical assignment. " +
                "\nPlease select one of the options below:\n");

                Console.WriteLine("1) Show all people entries from the database." +
                    "\n2) Show a person entry by Id." +
                    "\n3) Create a new person entry." +
                    "\n4) Update a person entry by Id." +
                    "\n5) Delete a person entry by Id." +
                    "\n6) Close the application.");

                Console.WriteLine("Please enter number of your chosen option.");

                var option = Console.ReadLine();

                bool convertionResult = int.TryParse(option, out var optionNumber);

                if (!convertionResult)
                {
                    Console.WriteLine("\nThe value you entered (" + option + ") is not a number. Please try selecting the option again.");
                    continue;
                }

                if (optionNumber < 1 || optionNumber > 6)
                {
                    Console.WriteLine("\nThe value you entered (" + optionNumber + ") is not an option. Please try selecting the option again.");
                    continue;
                }

                switch (optionNumber)
                {
                    case 1:
                        CallGetAll(apiClient);
                        break;
                    case 2:
                        CallGetPersonById(apiClient);
                        break;
                    case 3: 
                        CallCreatePerson(apiClient);
                        break;
                    case 4:
                        CallUpdatePerson(apiClient);
                        break;
                    case 5:
                        CallDeletePerson(apiClient);
                        break;
                }

                bool WillContinue = false;

                if (optionNumber != 6)
                {
                    Console.WriteLine("\nWould you like to continue using the application (press Enter to proceed and Esc to quit)?");

                    ConsoleKeyInfo continueOption;

                    do
                    {
                        continueOption = Console.ReadKey();

                        if (continueOption.Key == ConsoleKey.Enter)
                        {
                            WillContinue = true;
                            Console.WriteLine();
                            break;
                        }
                        else if (continueOption.Key == ConsoleKey.Escape)
                        {
                            WillContinue = false;
                            Console.WriteLine("\nHHope you found this application useful :) \nHave a good day!");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    while (continueOption.Key != ConsoleKey.Escape || continueOption.Key != ConsoleKey.Enter);
                }
                else
                {
                    Console.WriteLine("\nHope you found this application useful :) \nHave a good day!");
                    break;
                }

                if (WillContinue is false)
                {
                    break;
                }
            }
        }

        private static void CallGetAll(API_Client apiClient)
        {
            int pageNumber = 1;
            int pageSize = 10;
            string orderBy = "lastName";

            Console.WriteLine("Please enter page number (press enter to use the default option):");
            int pageNumberEntered = Validator.ValidateNumber("page number", true);
            pageNumber = pageNumberEntered == 0 ? pageNumber : pageNumberEntered;

            Console.WriteLine("Please enter page size (press enter to use the default option):");
            int pageSizeEntered = Validator.ValidateNumber("page size", true);
            pageSize = pageSizeEntered == 0 ? pageSize : pageSizeEntered;

            Console.WriteLine("Please enter order by parameter (press enter to use the default option):");
            var orderByValue = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(orderByValue))
            {
                orderBy = orderByValue;
            }

            (bool success, object result) = apiClient.GetAll(pageNumber, pageSize, orderBy).Result;

            if (success)
            {
                Console.WriteLine();
                var people = (List<PersonDTO>)result;

                if (people.Any())
                {
                    TableGenerator.Generate(people).Write(Format.Alternative);
                }
                else
                {
                    Console.WriteLine("There are no people in the database (maybe the problem is with the wrong page number or size, please try again with different parameters).");
                }
            }
            else
            {
                var error = (Error)result;
                Console.WriteLine(error.Reason);
            }
        }

        private static void CallGetPersonById(API_Client apiClient)
        {
            Console.WriteLine("Please enter person id:");
            int idNumber = Validator.ValidateNumber("id");

            (bool success, object result) = apiClient.GetPersonById(idNumber).Result;

            if (success)
            {
                Console.WriteLine();
                var person = (PersonDTO)result;
                TableGenerator.Generate(person).Write(Format.Alternative);
            }
            else
            {
                var error = (Error)result;
                Console.WriteLine(error.Reason);
            }
        }

        private static void CallCreatePerson(API_Client apiClient)
        {
            PersonForCreationDTO person = new();

            FillFields(person);

            (bool success, object result) = apiClient.CreatePerson(person).Result;

            if (success)
            {
                Console.WriteLine("\nThe person has been created successfully.");
                Console.WriteLine("Created person:");
                Console.WriteLine();
                var personDTO = (PersonDTO)result;
                TableGenerator.Generate(personDTO).Write(Format.Alternative);
            }
            else
            {
                var error = (Error)result;
                Console.WriteLine(error.Reason);
            }
        }

        private static void CallUpdatePerson(API_Client apiClient)
        {
            PersonForCreationDTO person = new();

            Console.WriteLine("Please enter person id:");
            int idNumber = Validator.ValidateNumber("id");

            FillFields(person);

            (bool success, object result) = apiClient.UpdatePerson(idNumber, person).Result;

            if (success)
            {
                Console.WriteLine($"\nThe person with id = {idNumber} has been updated successfully.");
            }
            else
            {
                var error = (Error)result;
                Console.WriteLine(error.Reason);
            }
        }

        private static void CallDeletePerson(API_Client apiClient)
        {
            Console.WriteLine("Please enter person id:");
            int idNumber = Validator.ValidateNumber("id");

            (bool success, object result) = apiClient.DeletePerson(idNumber).Result;

            if (success)
            {
                Console.WriteLine($"Person with id = {idNumber} has been deleted successfully.");
            }
            else
            {
                var error = (Error)result;
                Console.WriteLine(error.Reason);
            }
        }

        private static void FillFields(PersonForCreationDTO person)
        {
            Console.WriteLine("Please enter last name:");
            var lastName = Validator.ValidateString("last name", true);
            person.lastName = lastName;

            Console.WriteLine("Please enter first name:");
            var firstName = Validator.ValidateString("first name", true);
            person.firstName = firstName;

            Console.WriteLine("Please enter street name:");
            var streetName = Validator.ValidateString("street name", true);
            person.streetName = streetName;

            Console.WriteLine("Please enter house number:");
            int houseNumber = Validator.ValidateNumber("house number");
            person.houseNumber = houseNumber;

            Console.WriteLine("Please enter apartment number (press enter to skip):");
            int apartmentNumber = Validator.ValidateNumber("apartment number", true);
            person.apartmentNumber = apartmentNumber;

            Console.WriteLine("Please enter postal code:");
            int postalCode = Validator.ValidateNumber("postal code");
            person.postalCode = postalCode;

            Console.WriteLine("Please enter town name:");
            var town = Validator.ValidateString("town", true);
            person.town = town;

            Console.WriteLine("Please enter phone number (must start with '+'):");
            var phoneNumber = Validator.ValidatePhoneNumber();
            person.phoneNumber = phoneNumber;

            Console.WriteLine("Please enter date of birth (in format dd/MM/yyyy):");
            DateTime dateOfBirth = Validator.ValidateDate();
            person.dateOfBirth = dateOfBirth;
        }
    }
}
