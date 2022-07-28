using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        // Fields
        private readonly ILogger<SampleController> logger;
        private static readonly List<int> samples = new() { 12 };

        // Constructor
        public SampleController(ILogger<SampleController> logger)
        {
            this.logger = logger;
        }

        // Methods
        [HttpGet("/sample")]
        [HttpGet("/samples")] // c# supports multiple attributes
        public ContentResult GetSamples()
        {
            // asp.net provides a bunch of different data types under the IActionResult interface.
            // the job of an action result is to deserialize itself into an http response.

            // ContentResult is good for when you have something to put in the response body.
            // (otherwise StatusCodeResult)
            samples[0]++;

            string json = JsonSerializer.Serialize(samples);

            var result = new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };

            //    logger.LogTrace();// use trace to record everything, evry little action, literally anything.
                logger.LogInformation($"Samples value:  {samples[0]}");
            //    logger.LogDebug(); // use debug to log relevent events
            //    logger.LogWarning();
            //    logger.LogError($"Samples value:  {samples[0]}"); // uh oh, something BAD happened
            //    logger.LogCritical($"Samples value:  {samples[0]}");

            return result;
        }

        [HttpPost("/sample")]
        public ContentResult AddSample([FromBody] int sample)
        {
            samples.Add(sample);

            string json = JsonSerializer.Serialize(samples);

            var result = new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
            
            return result;

        }
        [HttpGet("/helloworld")]
        public ContentResult PrintHelloWorld(){
            string json = JsonSerializer.Serialize("hello:world");
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            return result;
        }

    }
}
