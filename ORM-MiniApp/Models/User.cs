using ORM_MiniApp.Models.Common;
using System.Text.RegularExpressions;

namespace ORM_MiniApp.Models
{
    public class User : BaseEntity
    {

        private string _fullName;
        private string _password;
        private string _email;
        public string FullName
        {
            get => _fullName;

            set
            {
                value = value.Trim();

                while (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20)
                {
                    Console.WriteLine("FullName must be between 5 and 20 characters long and cannot consist only of spaces. Enter again:");
                    value = Console.ReadLine().Trim();
                }

                _fullName = value;
            }
        }
        public string Email
        {
            get => _email;

            set
            {
                value = value.Trim();
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                while (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, emailPattern))
                {
                    Console.WriteLine("Please enter a valid email address:");
                    value = Console.ReadLine()?.Trim();
                }

                _email = value;
            }
        }
        public string Password
        {
            get => _password;

            set
            {
                while (true)
                {
                    if (value.Length >= 8)
                    {
                        bool hasDigit = false, hasUpper = false, hasLower = false;
                        foreach (var item in value)
                        {
                            if (char.IsDigit(item)) hasDigit = true;
                            else if (char.IsUpper(item)) hasUpper = true;
                            else if (char.IsLower(item)) hasLower = true;
                            if (hasDigit && hasUpper && hasLower)
                            {
                                _password = value;
                                return;
                            }
                        }
                        Console.Write("Password should has at least 1 Upper, 1Lower char and 1 Digit!.Enter Again:");
                        value = Console.ReadLine();

                    }
                    else
                    {
                        Console.Write("Password should contains at least 8 elements.Enter again:");
                        value = Console.ReadLine();
                    }
                }

            }
        }
        public string Address { get; set; }
    }
}
