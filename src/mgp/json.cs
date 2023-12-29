using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace mgp;

public static class FileJson
{
    public static async Task Load_(string filepath, string param)
    {
        Console.WriteLine($"Reading files csv");
        string jsonString = await File.ReadAllTextAsync(filepath);
        Console.WriteLine(jsonString);
        // Deserializa o JSON para um objeto JObject
        JObject? jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

        // Acessando a lista de features
        JArray features = (JArray)jsonObject!["features"]!;
        

        // Iterando sobre cada feature
        foreach (JObject feature in features.Where(s => features["properties"]!["nome"]!.ToString().ToLower() == param.ToLower()))
        {
            // Acessando o ID da feature
            string id = (string)feature!["id"]!;
            Console.WriteLine($"ID da feature: {id}");

            // Acessando o nome da feature
            string nome = (string)feature!["properties"]!["nome"]!;
            Console.WriteLine($"Nome da feature: {nome}");

            // Acessando o tipo de geometria
            string tipoGeometria = (string)feature!["geometry"]!["type"]!;
            Console.WriteLine($"Tipo de geometria: {tipoGeometria}");

            // Acessando as coordenadas da geometria (para MultiPolygon, precisa tratar o loop interno)
            if (tipoGeometria == "MultiPolygon")
            {
                JArray coordinates = (JArray)feature!["geometry"]!["coordinates"]!;
                foreach (JArray polygon in coordinates)
                {
                    foreach (JArray point in polygon)
                    {
                        double x = (double)point[0];
                        double y = (double)point[1];
                        Console.WriteLine($"Ponto da geometria: ({x}, {y})");
                    }
                }
            }

            Console.WriteLine("------------------------");
        }

        // Acessando informações gerais do JSON
        int totalFeatures = (int)jsonObject!["totalFeatures"]!;
        Console.WriteLine($"Total de features: {totalFeatures}");
    }
}