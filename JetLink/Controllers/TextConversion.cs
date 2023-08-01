using Entity;
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
        public ActionResult<string> ConvertTextToNumber([FromBody] TextConveresionReq request)
        {
            string inputText = request.Text;
            string[] words = inputText.Split(' ');

            Dictionary<string, int> turkishNumbers = GetTurkishNumberMappings();

            List<string> resultWords = new List<string>();
            bool previousWasNumber = false;
            bool resultAdded = false;
            int result = 1;
            for (int i = 0; i < words.Length; i++)
            {

                if (turkishNumbers.ContainsKey(words[i].ToLower()))
                {
                    int number = turkishNumbers[words[i].ToLower()];

                    if (previousWasNumber)
                    {
                        result += number;
                       
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
                    if (!resultAdded) // Check if the result has been added before
                    {
                        resultWords.Add(result.ToString());
                        resultAdded = true; // Set the flag to true to indicate that the result has been added
                    }
                    resultWords.Add(words[i]);
                    previousWasNumber = false;
                }

              
            }

            // Sonuç metnini birleştiriyoruz //"\"" + result.ToString() + "\" " + 
            string resultString = string.Join(" ", resultWords);
            return resultString;
        }

        private Dictionary<string, int> GetTurkishNumberMappings()
        {
            var turkishNumbers = new Dictionary<string, int>
            {
                { "bin", 1000 },
                { "iki bin", 2000 },
                { "üç bin", 3000 },
                { "dört bin", 4000 },
                { "beş bin", 5000 },
                { "altı bin", 6000 },
                { "yedi bin", 7000 },
                { "sekiz bin", 8000 },
                { "dokuz bin", 9000 },
                { "on bin", 10000 },
                { "bir", 1 },
                { "iki", 2 },
                { "üç", 3 },
                { "dört", 4 },
                { "beş", 5 },
                { "altı", 6 },
                { "yedi", 7 },
                { "sekiz", 8 },
                { "dokuz", 9 },
                { "on", 10 },
                { "yirmi", 20 },
                { "otuz", 30 },
                { "kırk", 40 },
                { "elli", 50 },
                { "altmış", 60 },
                { "yetmiş", 70 },
                { "seksen", 80 },
                { "doksan", 90 },
                { "yüz", 100 }
            };

            return turkishNumbers;


        }
    }

}
