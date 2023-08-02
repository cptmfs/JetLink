using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    private Dictionary<string, string> numberMap = new Dictionary<string, string>
    {
        {"bir", "1"},
        {"iki", "2"},
        {"üç", "3"},
        {"dört", "4"},
        {"beş", "5"},
        {"altı", "6"},
        {"yedi", "7"},
        {"sekiz", "8"},
        {"dokuz", "9"},
        {"on", "10"},
        {"yirmi", "20"},
        {"otuz", "30"},
        {"kırk", "40"},
        {"elli", "50"},
        {"altmış", "60"},
        {"yetmiş", "70"},
        {"seksen", "80"},
        {"doksan", "90"},
        {"yüz", "100"},
        {"bin", "1000"}
    };

    [HttpPost]
    public IActionResult Post([FromBody] string input)
    {
        var transformedInput = TransformInput(input);
        return Ok(transformedInput);
    }

    private string TransformInput(string input)
    {
        // Türkçe sayı sözcüklerini ve birleşik ifadeleri ilgili rakamlarla değiştirin
        input = ReplaceTurkishNumberWords(input);
        input = ReplaceCombinedExpressions(input);

        return input;
    }

    private string ReplaceTurkishNumberWords(string input)
    {
        // Sözcükleri bul ve çevir
        foreach (var kvp in numberMap)
        {
            var word = kvp.Key;
            var number = kvp.Value;

            input = Regex.Replace(input, @"\b" + word + @"\b", number + " ");
        }

        return input;
    }

    private string ReplaceCombinedExpressions(string input)
    {
        // Birleşik ifadeleri bul ve çevir
        var matches = Regex.Matches(input, @"\b(\d+)([a-z]+)\b");
        foreach (Match match in matches)
        {
            var replacedValue = match.Groups[1].Value + " " + GetNumericValuesFromTurkishNumber(match.Groups[2].Value) + " ";
            input = input.Replace(match.Value, replacedValue);
        }

        return input;
    }

    private string GetNumericValuesFromTurkishNumber(string numberWord)
    {
        // Türkçe sayı sözcüklerini ve özel durumları ilgili rakamlarla eşleştirin
        var numericValues = new List<int>();
        var words = numberWord.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (numberMap.TryGetValue(word, out var number))
            {
                numericValues.Add(int.Parse(number));
            }
        }

        return string.Join("", numericValues);
    }
}
//private string ReplaceTurkishNumberWords(string request)
//{
//    return request
//        .Replace("2", "2 ")
//    .Replace("3", "3 ")
//    .Replace("4", "4 ")
//    .Replace("5", "5 ")
//    .Replace("6", "6 ")
//    .Replace("7", "7 ")
//    .Replace("8", "8 ")
//    .Replace("9", "9 ")
//    .Replace("10", "10 ")
//    .Replace("20", "20 ")
//    .Replace("30", "30 ")
//    .Replace("40", "40 ")
//    .Replace("50", "50 ")
//    .Replace("60", "60 ")
//    .Replace("70", "70 ")
//    .Replace("80", "80 ")
//    .Replace("90", "90 ")
//    .Replace("100", "100 ")
//    .Replace("1000", "1000 ")
//    .Replace("bir", "1 ")
//    .Replace("iki", "2 ")
//    .Replace("üç", "3 ")
//    .Replace("dört", "4 ")
//    .Replace("beş", "5 ")
//    .Replace("altı", "6 ")
//    .Replace("yedi", "7 ")
//    .Replace("sekiz", "8 ")
//    .Replace("dokuz", "9 ")
//    .Replace("on", "10 ")
//    .Replace("yirmi", "20 ")
//    .Replace("otuz", "30 ")
//    .Replace("kırk", "40 ")
//    .Replace("elli", "50 ")
//    .Replace("altmış", "60 ")
//    .Replace("yetmiş", "70 ")
//    .Replace("seksen", "80 ")
//    .Replace("doksan", "90 ")
//    .Replace("yüz", "100 ")
//    .Replace("bin", "1000 ")
//    ;

//}