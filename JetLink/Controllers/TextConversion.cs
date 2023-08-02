using Business.Abstract;
using Business.Concrete;
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
    }

}
