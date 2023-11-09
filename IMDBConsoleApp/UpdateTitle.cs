using System;
using System.Data;
using System.Data.SqlClient;
using Spectre.Console;

namespace IMDBConsoleApp
{
	public class UpdateTitle
	{
		public static void RunUpdateTitle(SqlConnection sqlConn)
		{
            var table = new Table();

            Console.WriteLine();
            Console.Write("Indtast Tconst på den title som du ønsker at opdatere: ");
            string tconst = Console.ReadLine();

            Console.Write("Indtast Title Type: ");
            string titleType = Console.ReadLine();

            Console.Write("Indtast Primary Title: ");
            string primaryTitle = Console.ReadLine();


            Console.Write("Indtast Original Type: ");
            string originalTitle = Console.ReadLine();

            Console.Write("Indtast 0 for Ikke Adult eller 1 for Adult: ");
            string isAdult = Console.ReadLine();

            Console.Write("Indtast Start Year: ");
            string startYear = Console.ReadLine();

            Console.Write("Indtast End Year: ");
            string endYear = Console.ReadLine();

            Console.Write("Indtast Runtime Minutes: ");
            string runtimeMinutes = Console.ReadLine();

            Console.WriteLine();

            table.AddColumn("Tconst");
            table.AddColumn("TitleType");
            table.AddColumn("PrimaryTitle");
            table.AddColumn("OriginalTitle");
            table.AddColumn("IsAdult");
            table.AddColumn("StartYear");
            table.AddColumn("EndYear");
            table.AddColumn("RuntimeMinues");

            Console.WriteLine();
            Console.WriteLine("Before updating the title:");
            Console.WriteLine();

            SqlCommand beforeUpdateCmd = new SqlCommand("SELECT * FROM Titles WHERE Titles.Tconst = @tconst", sqlConn);

            beforeUpdateCmd.Parameters.AddWithValue("@tconst", tconst);

            using (SqlDataReader reader = beforeUpdateCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    table.AddRow(reader["Tconst"].ToString(), reader["TitleType"].ToString(), reader["PrimaryTitle"].ToString(), reader["OriginalTitle"].ToString(), reader["IsAdult"].ToString(), reader["StartYear"].ToString(), reader["EndYear"].ToString(), reader["RuntimeMinutes"].ToString());
                }
            }

            AnsiConsole.Write(table);

            SqlCommand updateTitleCmd = new SqlCommand("UpdateTitle", sqlConn);

            updateTitleCmd.CommandType = CommandType.StoredProcedure;

            if(tconst == "")
            {
                Console.Write("Tconst cannot be null");
            } else
            {
                updateTitleCmd.Parameters.Add("@tconst", SqlDbType.VarChar, 10).Value = tconst;
            }
 
            updateTitleCmd.Parameters.Add("@TitleType", SqlDbType.VarChar, 255).Value = titleType == "" ? DBNull.Value : titleType;
            updateTitleCmd.Parameters.Add("@PrimaryTitle", SqlDbType.VarChar, 500).Value = primaryTitle == "" ? DBNull.Value : primaryTitle;
            updateTitleCmd.Parameters.Add("@OriginalTitle", SqlDbType.VarChar, 500).Value = originalTitle == "" ? DBNull.Value : originalTitle;
            updateTitleCmd.Parameters.Add("@IsAdult", SqlDbType.Int).Value = isAdult == "" ? DBNull.Value : Convert.ToInt32(isAdult);
            updateTitleCmd.Parameters.Add("@StartYear", SqlDbType.Int).Value = startYear == "" ? DBNull.Value : Convert.ToInt32(startYear);
            updateTitleCmd.Parameters.Add("@EndYear", SqlDbType.Int).Value = endYear == "" ? DBNull.Value : Convert.ToInt32(endYear);
            updateTitleCmd.Parameters.Add("@RuntimeMinutes", SqlDbType.Int).Value = runtimeMinutes == "" ? DBNull.Value : Convert.ToInt32(runtimeMinutes);

            try
            {
                updateTitleCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(updateTitleCmd.CommandText, ex);
            }

            Console.WriteLine();
            Console.WriteLine("After updating the title:");
            Console.WriteLine();

            table.RemoveRow(0);

            using (SqlDataReader reader = beforeUpdateCmd.ExecuteReader())
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

