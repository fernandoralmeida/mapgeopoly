using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace mgp;

class Program
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("Open CSV/JSON File Map Geo");

        while (true)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Choose Options!");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 CSV");
            Console.WriteLine("2 JSON");
            Console.WriteLine("------------------------");
            Console.WriteLine("0 Close");

            Console.Write("Option: ");
            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Console.Write("Cidade: ");

                    await foreach (var item in FileCSV.Load_(File_("csv"), Console.ReadLine()!))
                        Console.WriteLine(item);
                    break;

                case 2:
                    await FileJson.Load_(File_("json"), Console.ReadLine()!);
                       // Console.WriteLine(item);
                    break;

                case 0:
                    Console.WriteLine("Closing App...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again!");
                    break;
            }
        }
    }

    /// <summary>
    /// Format File Path
    /// </summary>
    /// <param name="type">String Type: Json, CSV</param>
    /// <returns>Return Full File Path</returns>
    public static string File_(string type)
    {
        return $"/home/dbn/dev/mapgeopoly/csv/BC250_2017_Municipio_A.{type}";
    }

}

