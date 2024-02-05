using System.Text.RegularExpressions;
using System.Globalization;

namespace MacrixPracticalTask_Client.Validation
{
    public class Validator
    {
        public static int ValidateNumber(string fieldName, bool checkForWhiteSpace = false)
        {
            int number = 0;

            while (true)
            {
                var numberEntered = Console.ReadLine();

                if (checkForWhiteSpace)
                {
                    if (!string.IsNullOrWhiteSpace(numberEntered))
                    {
                        bool convertionResult = int.TryParse(numberEntered, out number);

                        if (!convertionResult)
                        {
                            Console.WriteLine("\nThe value you entered (" + numberEntered + ") is not a number. Please try entering the " + fieldName + " again.");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    bool convertionResult = int.TryParse(numberEntered, out number);

                    if (!convertionResult)
                    {
                        Console.WriteLine("\nThe value you entered (" + numberEntered + ") is not a number. Please try entering the " + fieldName + " again.");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return number;
        }

        public static string ValidateString(string fieldName, bool checkForWhiteSpace = false)
        {
            string line;

            while (true)
            {
                line = Console.ReadLine();

                if (checkForWhiteSpace)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine("\nThe value you entered (" + line + ") is empty. Please try entering the " + fieldName + " again.");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return line;
        }

        public static string ValidatePhoneNumber()
        {
            string phoneNumber;

            while (true)
            {
                var phone = Console.ReadLine();

                bool convertionResult = Regex.IsMatch(phone, @"^\+\d{1,}");

                if (!convertionResult)
                {
                    Console.WriteLine("\nThe value you entered (" + phone + ") is not a phone number. Please try entering phone number starting from '+' again.");
                    continue;
                }
                else
                {
                    phoneNumber = phone.Trim();
                    break;
                }
            }

            return phoneNumber;
        }

        public static DateTime ValidateDate()
        {
            DateTime dateOfBirth;

            while (true)
            {
                var date = Console.ReadLine();

                bool convertionResult = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);

                if (!convertionResult)
                {
                    Console.WriteLine("\nThe value you entered (" + date + ") is not a date. Please try entering date again.");
                    continue;
                }
                else
                {
                    break;
                }
            }

            return dateOfBirth;
        }
    }
}
