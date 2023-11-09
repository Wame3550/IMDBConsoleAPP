using System;
using System.Data;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class InsertTitle
	{
		public static void RunInsertTitle(SqlConnection sqlConn)
		{
            var table = new Table();

            Console.WriteLine();
            Console.Write("Indtast Title Type: ");
            string? titleType = Console.ReadLine();

            Console.Write("Indtast Primary Title: ");
            string? primaryTitle = Console.ReadLine();


            Console.Write("Indtast Original Type: ");
            string? originalTitle = Console.ReadLine();

            Console.Write("Indtast 0 for Ikke Adult eller 1 for Adult: ");
            string? isAdult = Console.ReadLine();

            Console.Write("Indtast Start Year: ");
            string? startYear = Console.ReadLine();

            Console.Write("Indtast End Year: ");
            string? endYear = Console.ReadLine();

            Console.Write("Indtast Runtime Minutes: ");
            string? runtimeMinutes = Console.ReadLine();

            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("InsertTitle", sqlConn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TitleType", SqlDbType.VarChar, 255).Value = titleType == "" ? DBNull.Value : titleType;
            cmd.Parameters.Add("@PrimaryTitle", SqlDbType.VarChar, 500).Value = primaryTitle == "" ? DBNull.Value : primaryTitle;
            cmd.Parameters.Add("@OriginalTitle", SqlDbType.VarChar, 500).Value = originalTitle == "" ? DBNull.Value : originalTitle;
            cmd.Parameters.Add("@IsAdult", SqlDbType.Int).Value = isAdult == "" ? DBNull.Value : Convert.ToInt32(isAdult);
            cmd.Parameters.Add("@StartYear", SqlDbType.Int).Value = startYear == "" ? DBNull.Value : Convert.ToInt32(startYear);
            cmd.Parameters.Add("@EndYear", SqlDbType.Int).Value = endYear == "" ? DBNull.Value : Convert.ToInt32(endYear);
            cmd.Parameters.Add("@RuntimeMinutes", SqlDbType.Int).Value = runtimeMinutes == "" ? DBNull.Value : Convert.ToInt32(runtimeMinutes);


            table.AddColumn("Tconst");
            table.AddColumn("TitleType");
            table.AddColumn("PrimaryTitle");
            table.AddColumn("OriginalTitle");
            table.AddColumn("IsAdult");
            table.AddColumn("StartYear");
            table.AddColumn("EndYear");
            table.AddColumn("RuntimeMinues");

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(cmd.CommandText, ex);
            }

            SqlCommand titlesLastRowcCmd = new SqlCommand("SELECT * FROM Titles WHERE Titles.Tconst = (SELECT MAX(Titles.Tconst) FROM Titles); ", sqlConn);


            using (SqlDataReader reader = titlesLastRowcCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    table.AddRow(reader["Tconst"].ToString(), reader["TitleType"].ToString(), reader["PrimaryTitle"].ToString(), reader["OriginalTitle"].ToString(), reader["IsAdult"].ToString(), reader["StartYear"].ToString(), reader["EndYear"].ToString(), reader["RuntimeMinutes"].ToString());
                }
            }

            AnsiConsole.Write(table);
        }
	}
}

