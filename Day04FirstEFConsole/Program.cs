using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04FirstEFConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SocietyDbContext db = new SocietyDbContext();
                Random random = new Random();
                Person p1 = new Person { Name = "Jerry " + random.Next(1000), Age = random.Next(100) };
                db.People.Add(p1); // insert operation is scheduled but not executed yet
                db.SaveChanges(); // synchronize objects in memory with database
                Console.WriteLine("Record added");

                //UPDATE; equivalent of of update fetch then modify and save changes
                //firstOrDefault will either return Person or null
                Person p2 = (from p in db.People where p.Id == 2 select p).FirstOrDefault<Person>();
                if (p2 != null )
                {
                    p2.Name = "Alabma " + (random.Next(10000) + 10000); //entity framework is watching and notices the modification
                    db.SaveChanges(); // update the datebase to synchronize entities in memory with the database
                    Console.WriteLine("Record Updated.");
                }
                else
                {
                    Console.WriteLine("Record not found");
                }

                //delete - fetch schedule for deletion then save changes
                Person p3 = (from p in db.People where p.Id == 1 select p).FirstOrDefault<Person>();
                if (p3 != null )
                { // found the record
                    db.People.Remove(p3);
                    db.SaveChanges();
                    Console.WriteLine("Record Deleted");
                } else
                {
                    Console.WriteLine("Record not found");
                }

                //fetch all records
                List<Person> peopleList = (from p in db.People select p).ToList<Person>();
                foreach (Person p in peopleList)
                {
                    Console.WriteLine($"{p.Id}: {p.Name} is {p.Age} years old.");
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Database operation failed: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

        }
    }
}
