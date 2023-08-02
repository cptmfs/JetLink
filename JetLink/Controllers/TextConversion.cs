using Entity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace JetLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextConversion : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> ConvertTextToNumber([FromBody] string request)
        {
            var transformedInput = TransformInput(request);
            return Ok(transformedInput);

        }


        private string ReplaceTurkishNumberWords(string request)
        {
            // Dönüşüm tablosu tanımlayalım
            Dictionary<string, string> numberWordsMap = new Dictionary<string, string>
    {
        { "1", "1" },
        { "1 0", "10" },
        { "1 00", "100" },
        { "1 000", "1000" },
        { "2", "2" },
        { "2 0", "20" },
        { "2 00", "200" },
        { "2 000", "2000" },
        { "3", "3" },
        { "3 0", "30" },
        { "3 00", "300" },
        { "3 000", "3000" },
        { "4", "4" },
        { "4 0", "40" },
        { "4 00", "400" },
        { "4 000", "4000" },
        { "5", "5" },
        { "5 0", "50" },
        { "5 00", "500" },
        { "5 000", "5000" },
        { "6", "6" },
        { "6 0", "60" },
        { "6 00", "600" },
        { "6 000", "6000" },
        { "7", "7" },
        { "7 0", "70" },
        { "7 00", "700" },
        { "7 000", "7000" },
        { "8", "8" },
        { "8 0", "80" },
        { "8 00", "800" },
        { "8 000", "8000" },
        { "9", "9" },
        { "9 0", "90" },
        { "9 00", "900" },
        { "9 000", "9000" },
        { "10", "10" },
        { "10 0", "100" },
        { "10 00", "1000" },
        { "20", "20" },
        { "20 0", "200" },
        { "20 00", "2000" },
        { "30", "30" },
        { "30 0", "300" },
        { "30 00", "3000" },
        { "40", "40" },
        { "40 0", "400" },
        { "40 00", "4000" },
        { "50", "50" },
        { "50 0", "500" },
        { "50 00", "5000" },
        { "60", "60" },
        { "60 0", "600" },
        { "60 00", "6000" },
        { "70", "70" },
        { "70 0", "700" },
        { "70 00", "7000" },
        { "80", "80" },
        { "80 0", "800" },
        { "80 00", "8000" },
        { "90", "90" },
        { "90 0", "900" },
        { "90 00", "9000" },
        { "100", "100" },
        { "100 0", "1000" },
        { "1000", "1000" },
        {"bir","1"},
        {"iki","2"},
        {"üç","3" },
        {"dört","4" },
        {"beş", "5 "},
        {"altı","6"},
        {"yedi","7"},
        {"sekiz" , "8"},
        {"dokuz" , "9"},
        {"on" , "10"},
        {"yirmi" , "20"},
        {"otuz" , "30"},
        {"kırk" , "40"},
        {"elli","50"},
        {"altmış" , "60"},
        {"yetmiş" , "70"},
        {"seksen" , "80"},
        {"doksan" , "90"},
        {"yüz", "100 "},
        {"bin" , "1000"},
        
    };

            // Metni dönüşüm tablosuna göre değiştirelim
            foreach (var entry in numberWordsMap)
            {
                string cleanedText = CleanExcessiveSpaces(request); // Fazla boşlukları silme.
                request = cleanedText.Replace(entry.Key, entry.Value + "  ");

            }

            return request;
        }


        private string TransformInput(string request)
        {
            request = ReplaceTurkishNumberWords(request.ToLower()); //Metni küçük yazıya çevirme işlemi  " Dokuzyüzelli beş lira fiyatı var " bu tarz örnekler için gerekli.

            string inputText = request;
            string cleanedText = CleanExcessiveSpaces(inputText); // Fazla boşlukları silme.
            string[] words = cleanedText.Split(' ');

            Dictionary<string, int> turkishNumbers = GetTurkishNumberMappings();

            List<string> resultWords = new List<string>();
            bool previousWasNumber = false;
            bool resultAdded = false;
            int result = 1;
            int initialResult = 1;
            int indexCounter = 0;
            int prevNumber;

            for (int i = 0; i < words.Length; i++)
            {

                if (turkishNumbers.ContainsKey(words[i].ToLower()))
                {
                    int number = turkishNumbers[words[i].ToLower()];


                    if (previousWasNumber)
                    {
                        if (number == 1000)
                        {
                            result *= number;
                        }
                        else if (number == 100)
                        {
                            prevNumber = turkishNumbers[words[i - 1].ToLower()];
                            result = result - prevNumber;
                            result += prevNumber * number;
                        }
                        else
                        {
                            result += number;
                        }
                        previousWasNumber = true;
                    }
                    else
                    {
                        result = number;
                        previousWasNumber = true;
                    }
                }
                else
                {
                    if (result != 1)
                    {
                        if (result != initialResult)
                        {
                            resultAdded = false;
                        }
                        if (!resultAdded)
                        {
                            initialResult = result;
                            resultWords.Insert(indexCounter, result.ToString());
                            resultAdded = true;
                            indexCounter++;
                        }
                    }
                    resultWords.Add(words[i]);
                    indexCounter++;
                    previousWasNumber = false;
                }
            }



            string resultString = string.Join(" ", resultWords);
            return resultString;
        }

        private string CleanExcessiveSpaces(string input)
        {
            string cleanedText = Regex.Replace(input, @"\s+", " ");

            cleanedText = cleanedText.Trim();

            return cleanedText;
        }

        private Dictionary<string, int> GetTurkishNumberMappings()
        {
            var turkishNumbers = new Dictionary<string, int>
            {
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "10", 10 },
                { "20", 20 },
                { "30", 30 },
                { "40", 40 },
                { "50", 50 },
                { "60", 60 },
                { "70", 70 },
                { "80", 80 },
                { "90", 90 },
                { "100", 100 },
                { "1000", 1000 },
            };

            return turkishNumbers;
        }
    }

}
