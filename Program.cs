using System;
using System.Data.SqlClient;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    using (var connection = new SqlConnection("Server=RBHDWHRED003\\SQL;Initial Catalog=master;Integrated Security=True;Trusted_Connection=True"))
                    {
                        var command = new SqlCommand("SELECT TOP (10) *  FROM [COMORBIDITY].[dbo].[DNA_OUTPUT_DOCKERTEST]", connection);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("DNA_OUTPUT_DOCKERTEST");
								Console.WriteLine($"{reader[0]}:{reader[1]} ${reader[2]}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }

                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}