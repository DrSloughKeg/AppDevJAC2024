using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace Day01PeopleListInFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mainMenu = "What do you want to do?\n1.\tAdd person info\n2.\tList persons info\n3.\tFind and list a person by name\n4.\tFind and list persons younger than age\n0.\tExit\n\n";
            ReadAllPeopleFromFile();
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine(mainMenu);
                string input = Console.ReadLine();
                try { choice = int.Parse(input); } catch (Exception ex) when (ex is FormatException || ex is OverflowException) { Console.Write("Error: Please enter integer"); }
                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        SaveAllPeopleToFile();
                        choice = -1;
                        break;
                    case 2:
                        ListAllPersonsInfo();
                        choice = -1;
                        break;
                    case 3:
                        FindPersonByName();
                        choice = -1;
                        break;
                    case 4:
                        FindPersonYoungerThan();
                        choice = -1;
                        break;
                }

            }

            Console.WriteLine("Press any Key to exit...");
            Console.ReadLine();
        }

        static List<Person> people = new List<Person>();

        static void AddPersonInfo()
        {
            Console.Write("What is your name? ");
            string name = Console.ReadLine();
            Console.Write("How old are you? ");
            string ageStr = Console.ReadLine();
            Console.Write("What city do you live in? ");
            string city = Console.ReadLine();
            try
            {
                int age = int.Parse(ageStr);
                Person toAdd = new Person() { Name = name, Age = age, City = city };
                people.Add(toAdd);
                Console.WriteLine("Person added!\n");
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: You must enter an integer. \n" + ex.ToString());
            }



        }

        static void ListAllPersonsInfo()
        {
            Console.WriteLine("Listing all persons");
            int len = people.Count;
            for (int i = 0; i < len; i++)
            {
                Console.WriteLine(people[i].ToString());
            }
            Console.WriteLine();
        }

        static void FindPersonByName()
        {
            Console.Write("Who are you looking for? ");
            string search = Console.ReadLine();
            Console.WriteLine("Matches found: ");
            bool found = false;
            foreach (Person check in people)
            {
                if (check.Name.Contains(search))
                {
                    Console.WriteLine(check.ToString());
                    found = true;
                }
            }
            if (!found) { Console.WriteLine("Sorry, I did't find anyone.\n"); } else { Console.WriteLine(); }
        }

        static void FindPersonYoungerThan()
        {
            Console.Write("Search for anyone under the age of: ");
            string searchStr = Console.ReadLine();
            try
            {
                int search = int.Parse(searchStr);
                Console.WriteLine("Matches found: ");
                bool found = false;
                foreach (Person check in people)
                {
                    if (check.Age < search)
                    {
                        Console.WriteLine(check.ToString());
                        found = true;
                    }
                }
                if (!found) { Console.WriteLine("Sorry, I did't find anyone.\n"); } else { Console.WriteLine(); }
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: You must enter an integer. \n" + ex.ToString());
            }

        }

        static void ReadAllPeopleFromFile()
        {
            const string filePath = @"..\..\people.txt";
            try
            {
                {
                    string[] linesArray = File.ReadAllLines(filePath);
                    foreach (string line in linesArray)
                    {
                        if (line.Contains(";"))
                        {
                            //this is really ugly
                            int i = line.IndexOf(";");
                            string name = line.Substring(0, i);
                            string rest = line.Substring(i + 1, line.Length - (i + 1));
                            int j = rest.IndexOf(";");
                            string ageStr = rest.Substring(0, j);
                            string city = rest.Substring(j + 1, rest.Length - (j + 2));

                            //it would be better to turn that into a function

                            try
                            {
                                int age = int.Parse(ageStr);
                                Person toAdd = new Person() { Name = name, Age = age, City = city};
                                people.Add(toAdd);
                            }
                            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                            {
                                Console.WriteLine("Error: ageStr is not an integer. \n" + ex.ToString());
                            }
                        }
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error Reading file: " + ex.Message);
            }
        }

        static void SaveAllPeopleToFile()
        {
            const string filePath = @"..\..\people.txt";
            try
            {
                //add logic to write to file
                int size = people.Count();
                string[] array = new string[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = people[i].ToStringF();
                }

                File.WriteAllLines(filePath, array);
            }
            catch (SystemException ex) { Console.WriteLine("Error writing to file: " + ex.Message); }
        }
    }

    public class Person
    {
        private string _name;
        private int _age;
        private string _city;
        public string Name
        {
            get
            { return _name; }
            set
            {
                if (value.Length > 1 && value.Length < 101 && !value.Contains(";"))
                { _name = value; }
                else { throw new ArgumentException("Invalid Name"); }
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value >= 0 && value <= 150)
                { _age = value; }
                else { throw new ArgumentException("Invalid Age"); }
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                if (value.Length > 1 && value.Length < 101 && !value.Contains(";")) { _city = value; }
                else { throw new ArgumentException("Invalid City"); }
            }
        }

        override
        public string ToString()
        {
            string s = $"{this.Name} is {this.Age} years old and lives in {this.City}.";
            return s;
        }

        public string ToStringF()
        {
            string s = $"{this.Name};{this.Age};{this.City};";
            return s;
        }

    }
}
