using LINExOPENAI.ApplicationService.ADProduct;
using LINExOPENAI.ApplicationService.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINExOPENAI.Infrastructure.Network
{
    public class BotAPIService : IBotAPIService
    {
        private readonly IConfiguration _configuration;

        public BotAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GenerateContent(ADGenerateRequestModelDTO generateRequestModel)
        {
            string apiKey = _configuration["OPENAI:GChatAPIKEY"];
            string apiModel = _configuration["OPENAI:Model"];
            List<string> rq = new List<string>();
            string rs = "";
            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
            //var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            //{
            //    Prompt = generateRequestModel.prompt,
            //    Model = apiModel,
            //    Temperature = 0.5,
            //    MaxTokens = 100,
            //    TopP = 1.0,
            //    FrequencyPenalty = 0.0,
            //    PresencePenalty = 0.0,

            //};
            //var result = await api.Completions.CreateCompletionsAsync(completionRequest);
            //foreach (var choice in result.Completions)
            //{
            //    rs = choice.Text;
            //    rq.Add(choice.Text);
            //}
            ChatMessage chatMessage = new ChatMessage()
            {
                Content = generateRequestModel.prompt
            };
            List<ChatMessage> chatMessages = new List<ChatMessage>() { chatMessage};
            var completionRequest = new OpenAI_API.Chat.ChatRequest()
            {
                Messages = chatMessages,
                Model = apiModel,
                Temperature = 0.5,
                MaxTokens = 100,
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,

            };
            var result = await api.Chat.CreateChatCompletionAsync(completionRequest,1);
            foreach (var choice in result.Choices)
            {
                rs = choice.Message.Content;
                rq.Add(rs);
            }
            return rq;
        }

    }
}
