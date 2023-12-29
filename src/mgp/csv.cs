using System.Text;

namespace mgp;

public static class FileCSV
{
    public static async IAsyncEnumerable<string> Load_(string filepath, string param)
    {
        var _list = new List<string>();

        using (var reader = new StreamReader(filepath))
        {
            var _rows = 0;

            while (!reader.EndOfStream)
            {
                _rows++;
                var line = await reader.ReadLineAsync();
                var _sb = new StringBuilder();


                var fields = line!.Split(',');
                if (fields.Length > 9)
                {
                    for (int i = 9; i <= fields.Length - 1; i++)
                        _sb.Append(fields[i] + ",");
                }

                if (fields[2].ToLower() == param.ToLower())
                    _list.Add($"{fields[1]}:{fields[2]}:{_sb.ToString()[..^1]}");

            }
        }
        foreach (var item in _list)
            yield return item;
            
    }
}