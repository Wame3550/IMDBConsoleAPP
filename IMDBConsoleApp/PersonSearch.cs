using System;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class PersonSearch
	{
		public static void RunPersonSearch(SqlConnection sqlConn)
		{
            var table = new Table();

            Console.WriteLine();
            Console.Write($"Indtast et wildcard på PrimaryName: ");
            string? input = Console.ReadLine();
            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("SELECT TOP(100) * FROM Persons_Professions_KnownForTitles WHERE PrimaryName LIKE @input AND BirthYear IS NOT NULL", sqlConn);

            cmd.Parameters.AddWithValue("@input", input);

            table.AddColumn("Nconst");
            table.AddColumn("PrimaryName");
            table.AddColumn("BirthYear");
            table.AddColumn("DeathYear");
            table.AddColumn("Profession");
            table.AddColumn("KnownForTitle");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    table.AddRow(reader["Nconst"].ToString(), reader["PrimaryName"].ToString(), reader["BirthYear"].ToString(), reader["DeathYear"].ToString(), reader["Profession"].ToString(), reader["KnownForTitle"].ToString());
                }
            }
            AnsiConsole.Write(table);
        }
	}
}

