using System;
using System.IO;
using System.Threading;

namespace UserRegistrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the User Registration App!");
                Console.WriteLine("A. Register a new user");
                Console.WriteLine("B. Login");
                Console.WriteLine("C. View registered users");
                Console.WriteLine("D. Quit");

                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "a":
                        RegisterUser();
                        break;
                    case "b":
                        Login();
                        break;
                    case "c":
                        ViewUsers();
                        break;
                    case "d":
                        Console.WriteLine("Exiting...");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RegisterUser()
        {
            Console.Write("Enter your user name: ");
            string name = Console.ReadLine();

            Console.Write("Do you want to generate a random password? (Y/N): ");
            string choice_pass = Console.ReadLine().ToLower();
            if (choice_pass == "y" || choice_pass == "")
            {
                string password = GenerateRandomPassword();
                Console.WriteLine($"Your password is: {password}");
                SaveUserCredentials(name, password);
            }
            else
            {
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                SaveUserCredentials(name, password);
            }

            Console.WriteLine("User registered successfully.");
        }

        static void Login()
        {
            Console.Write("Enter your user name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            bool userFound = false;
            string welcomeMessage = string.Empty;
            using (StreamReader reader = new StreamReader("account.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] userData = line.Split(", ");
                    if (userData[0] == name && userData[1] == password)
                    {
                        userFound = true;
                        break;
                    }
                }
            }

            if (userFound)
            {
                Console.WriteLine("Login successful.");
                using (StreamReader reader = new StreamReader("Welcome.txt"))
                {
                    welcomeMessage = reader.ReadToEnd();
                }
                Console.WriteLine(welcomeMessage);
            }
            else
            {
                Console.WriteLine("Invalid user name or password.");
            }
        }

        static void ViewUsers()
        {
            if (File.Exists("account.txt"))
            {
                using (StreamReader reader = new StreamReader("account.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("No users registered yet.");
            }
        }

        static void SaveUserCredentials(string name, string password)
        {
            using (StreamWriter writer = File.AppendText("account.txt"))
            {
                writer.WriteLine($"{name}, {password}");
            }
        }

        static string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
