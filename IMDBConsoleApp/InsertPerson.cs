using System;
using System.Data;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class InsertPerson
	{
		public static void RunInsertPerson(SqlConnection sqlConn)
		{
            var table = new Table();

            Console.WriteLine();
            Console.Write("Indtast Primary Name: ");
            string? primaryName = Console.ReadLine();

            Console.Write("Indtast Birth Year: ");
            string? birthYear = Console.ReadLine();


            Console.Write("Indtast Death Year: ");
            string? deathYear = Console.ReadLine();

            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("InsertPerson", sqlConn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PrimaryName", SqlDbType.VarChar, 255).Value = primaryName == "" ? DBNull.Value : primaryName;
            cmd.Parameters.Add("@BirthYear", SqlDbType.Int).Value = birthYear == "" ? DBNull.Value : Convert.ToInt32(birthYear);
            cmd.Parameters.Add("@DeathYear", SqlDbType.Int).Value = deathYear == "" ? DBNull.Value : Convert.ToInt32(deathYear);

            table.AddColumn("Nconst");
            table.AddColumn("PrimaryTitle");
            table.AddColumn("BirthYear");
            table.AddColumn("DeathYear");

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(cmd.CommandText, ex);
            }

            SqlCommand lastRowcmd = new SqlCommand("SELECT * FROM Persons WHERE Persons.Nconst = (SELECT MAX(Persons.Nconst) FROM Persons); ", sqlConn);


            using (SqlDataReader reader = lastRowcmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    table.AddRow(reader["Nconst"].ToString(), reader["PrimaryName"].ToString(), reader["BirthYear"].ToString(), reader["DeathYear"].ToString());
                }
            }

            AnsiConsole.Write(table);
        }
	}
}

