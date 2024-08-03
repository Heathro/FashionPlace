using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AIService.Data;
using AIService.Entities;
using AIService.DTOs;
using AIService.Services;
using MongoDB.Entities;
using System.Text.Json;
using MongoDB.Driver;

namespace AIService.Hubs;

public class AIHub : Hub
{
    private readonly AIDbContext _context;
    private readonly IMapper _mapper;
    private readonly ModelService _modelService;
    private static string _products;

    public AIHub(AIDbContext context, IMapper mapper, ModelService modelService)
    {
        _context = context;
        _mapper = mapper;
        _modelService = modelService;
    }

    public override async Task OnConnectedAsync()
    {
        var result = await DB.Find<Product>().ExecuteAsync();
        _products = JsonSerializer.Serialize(result);

        var messageThread = new MessageThread()
        {
            ConnectionId = Context.ConnectionId,
            Messages = new List<Message>()
            {
                new Message
                {
                    IsUser = false,
                    Content = "Hi, may I help you?"
                }
            }
        };
        _context.MessageThreads.Add(messageThread);

        await _context.SaveChangesAsync();

        await Clients.Caller.SendAsync
        (
            "ReceiveMessageThread",
            _mapper.Map<MessageThreadDto>(messageThread)
        );
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string message)
    {
        var messageThread = await _context.MessageThreads
            .Include(mt => mt.Messages)
            .FirstOrDefaultAsync(mt => mt.ConnectionId == Context.ConnectionId);

        if (messageThread == null) return;

        var userMessage = new Message { IsUser = true, Content = message };
        messageThread.Messages.Add(userMessage);
        await _context.SaveChangesAsync();

        var userMessageDto = _mapper.Map<MessageDto>(userMessage);
        await Clients.Caller.SendAsync("ReceiveMessage", userMessageDto);

        var messageThreadDto = _mapper.Map<MessageThreadDto>(messageThread);
        var aiMessage = new Message
        {
            IsUser = false,
            Content = await _modelService.GetAIReplyAsync(messageThreadDto, _products) 
        };
        messageThread.Messages.Add(aiMessage);
        await _context.SaveChangesAsync();
        
        var aiMessageDto = _mapper.Map<MessageDto>(aiMessage);
        await Clients.Caller.SendAsync("ReceiveMessage", aiMessageDto);
    }
}
