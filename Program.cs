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
                    using (var connection = new SqlConnection("Server=tcp:192.168.1.100,1433;Initial Catalog=test;User=DockerTest;Password=12345;"))
                    {
                        var command = new SqlCommand("SELECT TOP 10 * FROM dbo.test", connection);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("test!!!");
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