using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdIssue971
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            var notWrong = new NotWrong();
            var persons = notWrong.GetPersons1("MyDB");
            var check = persons.ToList();
            check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
        }
    }
}
