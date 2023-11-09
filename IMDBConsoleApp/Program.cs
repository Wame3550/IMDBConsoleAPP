using System.Data;
using System.Data.SqlClient;
using IMDBConsoleApp;
using Spectre.Console;

string ConnString = "server=localhost;database=IMDB;" +
            "user id=SA;password=Wame123456789;TrustServerCertificate=True";
int index = 1;

Console.WriteLine("Hvad vil du?");
Console.WriteLine("1: Søg efter Titles");
Console.WriteLine("2: Søg efter Persons");
Console.WriteLine("3: Tilføj Title");
Console.WriteLine("4: Tilføj Person");
Console.WriteLine("5: Update Person");
Console.WriteLine("6: Delete Title based on Tconst");

string input = Console.ReadLine();

SqlConnection sqlConn = new SqlConnection(ConnString);
sqlConn.Open();


var table = new Table();

switch (input)
{
    case "1":
        TitleSearch.RunTitleSearch(sqlConn);
        break;
    case "2":
        PersonSearch.RunPersonSearch(sqlConn);
        break;
    case "3":
        InsertTitle.RunInsertTitle(sqlConn);

        break;
    case "4":
        InsertPerson.RunInsertPerson(sqlConn);
        break;

    case "5":
        UpdateTitle.RunUpdateTitle(sqlConn);
        break;

    case "6":
        DeleteTitle.RunDeleteTitle(sqlConn);
        break;
}

sqlConn.Close();