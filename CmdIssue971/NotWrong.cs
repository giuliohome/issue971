using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CmdIssue971
{
    public class NotWrong
    {
        public IEnumerable<Person> GetPersons0(string myDB)
        {
            using (var db = new DataConnection(myDB))
            {
                return db.GetTable<Person>();
            }
        }
        public IEnumerable<Person> GetPersons1(string myDB)
        {

            using (var db = new DataConnection(myDB))
            {
                var people = db.GetTable<Person>();
                db.Close();
                return people.ToList();
            }
        }
        public IQueryable<Person> GetPersons2(string myDB)
        {
            // query is not sent to database here
            // it will be executed later when user will enumerate results of method
            // but DataContext will handle it properly
            return new DataContext(myDB).GetTable<Person>();
        }
        public IEnumerable<Person> GetPersons3(string myDB)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[myDB].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Person", conn))
                {
                    var DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        yield return new Person()
                        {
                            ID = (int)DR["ID"],
                            Name = (string)DR["Name"]
                        };
                    }
                    DR.Close();
                }
                conn.Close();
            }
        }
        public async Task<IEnumerable<Person>> GetPersons4(string myDB)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[myDB].ConnectionString))
            {
                var ret = Enumerable.Empty<Person>();
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("select * from Person", conn))
                {
                    var DR = await cmd.ExecuteReaderAsync();
                    while (await DR.ReadAsync())
                    {
                        ret = ret.Append( new Person()
                        {
                            ID = (int)DR["ID"],
                            Name = (string)DR["Name"]
                        });
                    }
                    DR.Close();
                }
                conn.Close();
                return ret;
            }
        }
    }
}
