﻿using Rystem.OpenAi;
using Rystem.OpenAi.Chat;

namespace InfoTechSecretary.Core.Services;

public class TranslatorService
{
    private readonly string _apiKey;
    private readonly IOpenAi _openAiApi;

    public TranslatorService(string apiKey)
    {
        OpenAiService.Instance.AddOpenAi(settings => { settings.ApiKey = apiKey; }, "NoDI");

        _openAiApi = OpenAiService.Factory.Create("NoDI");
    }

    public async Task<string> TranslateAndImproveAsync(string textToTranslate, double temperature = 0.1)
    {
        if (string.IsNullOrEmpty(textToTranslate))
        {
            throw new ArgumentException("Text to translate cannot be null or empty.", nameof(textToTranslate));
        }

        try
        {
            var results = await _openAiApi.Chat
                .Request(new Rystem.OpenAi.Chat.ChatMessage { Role = ChatRole.User, Content = string.Empty })
                .AddAssistantMessage("I'm here to help you with translation and improvement. Please provide the text you'd like to translate.")
                .AddSystemMessage("""
                                  請你照著原文翻成繁體中文，不要改變原意
                                  """)
                .WithModel(ChatModelType.Gpt4)
                .AddMessage(textToTranslate)
                .WithTemperature(temperature)
                .ExecuteAsync();

            return results.Choices[0].Message.Content;
        }
        catch (Exception ex)
        {
            // Handle the exception based on your application's logging and error handling strategy
            throw new ApplicationException("Error occurred while translating and improving the text.", ex);
        }
    }
}
