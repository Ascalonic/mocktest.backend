using Microsoft.AspNetCore.Mvc;
using MocktestBot.Models;
using MocktestBot.Services;

namespace MocktestBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        // GET: /questions/start-generating
        [HttpGet("start-generating")]
        public async Task<ActionResult<Question>> StartGenerating([FromQuery] string topic)
        {
            QuestionGeneratorService questionGeneratorService = new QuestionGeneratorService();
            var question = await questionGeneratorService.generateNextQuestion(topic, []);

            return Ok(question);
        }

        // POST: /questions/continue-generating
        [HttpPost("continue-generating")]
        public async Task<ActionResult<Question>> ContinueGenerating([FromBody] ContinueGeneratingRequest request)
        {
            // Create a new mock question based on the topic
            QuestionGeneratorService questionGeneratorService = new QuestionGeneratorService();
            var question = await questionGeneratorService.generateNextQuestion(request.Topic, request.PreviousQuestions);

            return Ok(question);
        }
    }
}
