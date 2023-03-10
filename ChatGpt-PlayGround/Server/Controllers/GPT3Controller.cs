using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

namespace ChatGpt_PlayGround.Server.Controllers;

[ApiController]
[Route("api/v1/chat")]
public class GPT3Controller : Controller
{
    private IOpenAIService openAIService;

    public GPT3Controller(IOpenAIService openAIService)
    {
        this.openAIService = openAIService;
    }

    /// <summary>
    /// Return Message (ChatCompletionCreateResponse) with GPT3
    /// Use SendMessage method
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("")]
    public Task<ChatCompletionCreateResponse> Completion(List<ChatMessage> chatMessages)
    {
        return SendMessage(chatMessages);
    }

    private async Task<ChatCompletionCreateResponse> SendMessage(List<ChatMessage> chatMessages)
    {
        var completionResult = await openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
        {
            Messages = chatMessages,
            Model = Models.ChatGpt3_5Turbo
        });

        if(completionResult.Successful) return completionResult;
        
        if(completionResult.Error is not null) Console.WriteLine(completionResult.Error);
        
        throw new InvalidOperationException();
    }
}
