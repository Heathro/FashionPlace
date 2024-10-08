﻿using AutoMapper;
using AIService.DTOs;
using AIService.Entities;

namespace AIService.Services;

public class ModelService
{
    private readonly ModelHttpClient _modelHttpClient;
    private readonly IMapper _mapper;

    public ModelService(ModelHttpClient modelHttpClient, IMapper mapper)
    {
        _modelHttpClient = modelHttpClient;
        _mapper = mapper;
    }

    public async Task<string> GetAIReplyAsync(MessageThreadDto messageThreadDto, string products = "")
    {
        var modelChatRequest = _mapper.Map<ModelChatRequest>(messageThreadDto);

        modelChatRequest.messages.Insert(0, new ModelChatMessage
        {
            role = "user",
            content = "Products in stock: " + products
        });

        var response = await _modelHttpClient.GetAIResponseAsync(modelChatRequest);
        return response.message.content;
    }
}
