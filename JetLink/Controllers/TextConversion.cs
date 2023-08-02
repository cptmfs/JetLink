using Business.Abstract;
using Business.Concrete;
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
        ConversionManager conversionManager= new ConversionManager();
        [HttpPost]
        public ActionResult<string> ConvertTextToNumber([FromBody] string request)
        {
            var transformedInput = conversionManager.TransformInput(request);
            return Ok(transformedInput);
        }

        


        //private string TransformInput(string request)
        //{
        //    request = ReplaceTurkishWordsToNumber(request.ToLower()); //Metni küçük yazıya çevirme işlemi  " Dokuzyüzelli beş lira fiyatı var " bu tarz örnekler için gerekli.

        //    string inputText = request;
        //    string cleanedText = CleanExcessiveSpaces(inputText); // Fazla boşlukları silme.
        //    string[] words = cleanedText.Split(' ');

        //    Dictionary<string, int> turkishNumbers = GetTurkishNumberMappings();

        //    List<string> resultWords = new List<string>();
        //    bool previousWasNumber = false;
        //    bool resultAdded = false;
        //    int result = 1;
        //    int initialResult = 1;
        //    int indexCounter = 0;
        //    int prevNumber;

        //    for (int i = 0; i < words.Length; i++)
        //    {

        //        if (turkishNumbers.ContainsKey(words[i].ToLower()))
        //        {
        //            int number = turkishNumbers[words[i].ToLower()];


        //            if (previousWasNumber)
        //            {
        //                if (number == 1000)
        //                {
        //                    result *= number;
        //                }
        //                else if (number == 100)
        //                {
        //                    prevNumber = turkishNumbers[words[i - 1].ToLower()];
        //                    result = result - prevNumber;
        //                    result += prevNumber * number;
        //                }
        //                else
        //                {
        //                    result += number;
        //                }
        //                previousWasNumber = true;
        //            }
        //            else
        //            {
        //                result = number;
        //                previousWasNumber = true;
        //            }
        //        }
        //        else
        //        {
        //            if (result != 1)
        //            {
        //                if (result != initialResult)
        //                {
        //                    resultAdded = false;
        //                }
        //                if (!resultAdded)
        //                {
        //                    initialResult = result;
        //                    resultWords.Insert(indexCounter, result.ToString());
        //                    resultAdded = true;
        //                    indexCounter++;
        //                }
        //            }
        //            resultWords.Add(words[i]);
        //            indexCounter++;
        //            previousWasNumber = false;
        //        }
        //    }



        //    string resultString = string.Join(" ", resultWords);
        //    return resultString;
        //}

       

        //private Dictionary<string, int> GetTurkishNumberMappings()
        //{
        //    var turkishNumbers = new Dictionary<string, int>
        //    {
        //        { "1", 1 },
        //        { "2", 2 },
        //        { "3", 3 },
        //        { "4", 4 },
        //        { "5", 5 },
        //        { "6", 6 },
        //        { "7", 7 },
        //        { "8", 8 },
        //        { "9", 9 },
        //        { "10", 10 },
        //        { "20", 20 },
        //        { "30", 30 },
        //        { "40", 40 },
        //        { "50", 50 },
        //        { "60", 60 },
        //        { "70", 70 },
        //        { "80", 80 },
        //        { "90", 90 },
        //        { "100", 100 },
        //        { "1000", 1000 },
        //    };

        //    return turkishNumbers;
        //}
    }

}
