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
        private static readonly API_Client _apiClient = new();

        static void Main(string[] args)
        {
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
                        CallGetAll();
                        break;
                    case 2:
                        CallGetPersonById();
                        break;
                    case 3: 
                        CallCreatePerson();
                        break;
                    case 4:
                        CallUpdatePerson();
                        break;
                    case 5:
                        CallDeletePerson();
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

        private static void CallGetAll()
        {
            int pageNumber = 1;
            int pageSize = 10;
            string orderBy = "lastName";

            int pageNumberEntered = FillerValidator.FillValidatedNumber("page number", true);
            pageNumber = pageNumberEntered == 0 ? pageNumber : pageNumberEntered;
            int pageSizeEntered = FillerValidator.FillValidatedNumber("page size", true);
            pageSize = pageSizeEntered == 0 ? pageSize : pageSizeEntered;

            Console.WriteLine("Please enter order by parameter (press enter to use the default option):");
            var orderByValue = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(orderByValue))
            {
                orderBy = orderByValue;
            }

            (bool success, object result) = _apiClient.GetAll(pageNumber, pageSize, orderBy).Result;

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

        private static void CallGetPersonById()
        {
            int idNumber = FillerValidator.FillValidatedNumber("person id");

            (bool success, object result) = _apiClient.GetPersonById(idNumber).Result;

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

        private static void CallCreatePerson()
        {
            PersonForCreationDTO person = CreatePersonObject();

            (bool success, object result) = _apiClient.CreatePerson(person).Result;

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

        private static void CallUpdatePerson()
        {
            int idNumber = FillerValidator.FillValidatedNumber("person id");

            PersonForCreationDTO person = CreatePersonObject();

            (bool success, object result) = _apiClient.UpdatePerson(idNumber, person).Result;

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

        private static void CallDeletePerson()
        {
            int idNumber = FillerValidator.FillValidatedNumber("person id");

            (bool success, object result) = _apiClient.DeletePerson(idNumber).Result;

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

        private static PersonForCreationDTO CreatePersonObject()
        {
            PersonForCreationDTO person = new()
            {
                lastName = FillerValidator.FillValidatedString("last name"),
                firstName = FillerValidator.FillValidatedString("first name"),
                streetName = FillerValidator.FillValidatedString("street name"),
                houseNumber = FillerValidator.FillValidatedNumber("house number"),
                apartmentNumber = FillerValidator.FillValidatedNumber("apartment number", true),
                postalCode = FillerValidator.FillValidatedNumber("postal code"),
                town = FillerValidator.FillValidatedString("town"),
                phoneNumber = FillerValidator.FillValidatedPhoneNumber(),
                dateOfBirth = FillerValidator.FillValidatedDate()
            };

            return person;
        }
    }
}
