using System;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class TitleSearch
	{
		public static void RunTitleSearch(SqlConnection sqlConn)
		{
            var table = new Table();

            Console.WriteLine();
            Console.Write($"Indtast Title: ");
            string input = Console.ReadLine();
            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("SELECT TOP(100) * FROM Full_Titles WHERE PrimaryTitle LIKE @input", sqlConn);

            cmd.Parameters.AddWithValue("@input", input);

            table.AddColumn("Tconst");
            table.AddColumn("TitleType");
            table.AddColumn("PrimaryTitle");
            table.AddColumn("OriginalTitle");
            table.AddColumn("IsAdult");
            table.AddColumn("StartYear");
            table.AddColumn("EndYear");
            table.AddColumn("RuntimeMinues");
            table.AddColumn("Genre");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    table.AddRow(reader["Tconst"].ToString(), reader["TitleType"].ToString(), reader["PrimaryTitle"].ToString(), reader["OriginalTitle"].ToString(), reader["IsAdult"].ToString(), reader["StartYear"].ToString(), reader["EndYear"].ToString(), reader["RuntimeMinutes"].ToString(), reader["Genre"].ToString());
                }
            }
            AnsiConsole.Write(table);
        }
	}
}

