using ConsoleTables;
using MacrixPracticalTask_Client.Models.DTO;

namespace MacrixPracticalTask_Client.Generator
{
    public class TableGenerator
    {
        public static ConsoleTable Generate(PersonDTO person)
        {
            var table = new ConsoleTable(typeof(PersonDTO).GetProperties().Select(x => x.Name).ToArray());

            table.AddRow(person.id, person.firstName, person.lastName, person.streetName, person.houseNumber, person.apartmentNumber,
                person.postalCode, person.town, person.phoneNumber, person.dateOfBirth.ToShortDateString(), person.age);

            return table;
        }

        public static ConsoleTable Generate(List<PersonDTO> people)
        {
            var table = new ConsoleTable(typeof(PersonDTO).GetProperties().Select(x => x.Name).ToArray());

            foreach (var person in people)
            {
                table.AddRow(person.id, person.firstName, person.lastName, person.streetName, person.houseNumber, person.apartmentNumber,
                person.postalCode, person.town, person.phoneNumber, person.dateOfBirth.ToShortDateString(), person.age);
            }

            return table;
        }
    }
}
