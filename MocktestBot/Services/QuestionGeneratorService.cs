using MocktestBot.Models;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Text.Json;

namespace MocktestBot.Services
{
    public class QuestionGeneratorService
    {
        public async Task<Question> generateNextQuestion(String topic, ICollection<Question> previousQuestions)
        {
            string openaiApiKey = Environment.GetEnvironmentVariable("API_KEY");
            var api = new OpenAI_API.OpenAIAPI(openaiApiKey);
            // Prompt to ask OpenAI for a question based on the topic (adjust the prompt as necessary)
            string prompt = File.ReadAllText("Resources/Prompt.txt");
            prompt = prompt.Replace("{topic}", topic);
            prompt = prompt.Replace("{previous_questions}", JsonSerializer.Serialize(previousQuestions));

            // Make the API call
            var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                    Model = Model.GPT4_Turbo,
                    Temperature = 0.1,
                    MaxTokens = 200,
                    Messages = new ChatMessage[] {
                        new ChatMessage(ChatMessageRole.User, prompt)
            }
            });

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string resultString = result.Choices[0].ToString();
            if(resultString.StartsWith("{```json")) {
                resultString = resultString.Remove(0, 8);
                resultString = resultString.Remove(resultString.IndexOf("```}"));
            }
            if (resultString.StartsWith("\"{```json"))
            {
                resultString = resultString.Remove(0, 8);
                resultString = resultString.Remove(resultString.IndexOf("```}\""));
            }

            Question question = JsonSerializer.Deserialize<Question>(resultString, options);
            return question;

        }
    }
}
