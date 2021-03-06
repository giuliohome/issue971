﻿using System;
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
            Test().Wait();
            Console.ReadKey();
        }

        async static Task Test()
        {
            var notWrong = new NotWrong();
            var persons = await notWrong.GetPersons4("MyDB");
            var check = persons.ToList();
            check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
            persons = notWrong.GetPersons3("MyDB");
            check = persons.ToList();
            check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
            persons = notWrong.GetPersons2("MyDB");
            check = persons.ToList();
            check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
            persons = notWrong.GetPersons1("MyDB");
            check = persons.ToList();
            check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
            //persons = notWrong.GetPersons0("MyDB");
            //check = persons.ToList(); //crash! throw new ObjectDisposedException
            //check.ForEach(p => Console.WriteLine("{0} {1}", p.ID, p.Name));
        }
    }
}
