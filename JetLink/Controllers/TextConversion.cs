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
            int insertIndex = 0;
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
                        else if (number==100)
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
                        insertIndex = i;                    
                    }
                   
                }
                else
                {
                    if (result!=1)
                    {
                        if (result != initialResult)
                        {
                            resultAdded = false;
                        }
                        if (!resultAdded)
                        {
                            initialResult = result;
                            //resultWords.Insert(insertIndex, result.ToString());
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

        private Dictionary<string, int> GetTurkishNumberMappings()
        {
            var turkishNumbers = new Dictionary<string, int>
            {

                // Sadece  bin yeterli olacak !!
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
                { "yüz", 100 },
                { "1000", 1000 },
                { "2000", 2000 },
                { "3000", 3000 },
                { "4000", 4000 },
                { "5000", 5000 },
                { "6000", 6000 },
                { "7000", 7000 },
                { "8000", 8000 },
                { "9000", 9000 },
                { "10000", 10000 },
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
                { "100", 100 }
            };

            return turkishNumbers;


        }
    }

}
