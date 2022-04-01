using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\juliakarol\source\repos\SQL\SQL\bin\Debug\KursCA_sklep.db; Version=3;"))
                {
                    string query = "SELECT*FROM Uzytkownik";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    connection.Open();
                    SQLiteDataReader reader = command.ExecuteReader();
                    Console.WriteLine($"{"Imie",10}{"Miasto",10}{"Mail",10}");
                    Console.WriteLine();
                    while (reader.Read()) Console.WriteLine((reader["Imie"], reader["Miasto"], reader["Email"]));
                    connection.Close();
                }

                Console.WriteLine("Wpisz komendę");
                string komenda = Console.ReadLine().ToLower();
                if (komenda.Contains("newuser"))
                {
                    Console.WriteLine("Wpisz nowe imię");
                    string newname = Console.ReadLine();
                    Console.WriteLine("Wpisz nowe miasto");
                    string newcity = Console.ReadLine();
                    Console.WriteLine("Wpisz nowy email");
                    string newemail = Console.ReadLine();
                    using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\juliakarol\source\repos\SQL\SQL\bin\Debug\KursCA_sklep.db; Version=3;"))
                    {
                        string query = $"INSERT INTO Uzytkownik (Imie, Email, Miasto) VALUES ('{newname}', '{newcity}', '{newemail}')";
                        SQLiteCommand command = new SQLiteCommand(query, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else break;
            }
        }
    }
}

