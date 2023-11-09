using System;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class DeleteTitle
	{
		public static void RunDeleteTitle(SqlConnection sqlConn)
		{
            Console.WriteLine();
            Console.Write($"Indtast Tconst of Title you wish to delete: ");
            string? tconst = Console.ReadLine();
            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("DELETE FROM Titles WHERE Titles.Tconst LIKE @Tconst", sqlConn);

            cmd.Parameters.AddWithValue("@Tconst", tconst?.Trim());

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine();
                Console.WriteLine($"Title with the Tconst of {tconst} was deleted!");
            }
            catch (Exception ex)
            {
                throw new Exception(cmd.CommandText, ex);
            }
        }
	}
}

