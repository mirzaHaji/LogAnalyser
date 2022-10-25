// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.RegularExpressions;

Console.WriteLine("Please enter the test string");
var input = Console.ReadLine();
Console.WriteLine("Please enter the regex pattern");
var regex = Console.ReadLine();
if (string.IsNullOrWhiteSpace(regex) || string.IsNullOrWhiteSpace(input))
    throw new InvalidDataException();
if (regex[0] == regex[^1] && (regex [0] is '/' or '#' or '%' or '~'))
    regex = regex.Substring(1, regex.Length - 2);
var matches = Regex.Matches(input,regex, RegexOptions.None);
foreach (Match match in matches)
{
    var output = new Dictionary<string, string>();
    var time = "";
    foreach (Group group in match.Groups)
    {
        if(group.Name == "time")
        {
            time = group.Value;
            continue;
        }
        if (!int.TryParse(group.Name, out _))
            output[group.Name] = group.Value;
    }
    var result = new { time = time, parsed = output };
    var serialized =  JsonSerializer.SerializeToDocument(result, new JsonSerializerOptions{WriteIndented = true});
    Console.WriteLine(serialized.RootElement.ToString());
}