using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AIService.Data;
using AIService.Entities;
using AutoMapper;
using AIService.DTOs;

namespace AIService.Hubs;

public class AIHub : Hub
{
    private readonly AIDbContext _context;
    private readonly IMapper _mapper;

    public AIHub(AIDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public override async Task OnConnectedAsync()
    {
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
        
        await Clients.Caller.SendAsync(
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

        var userMessage = new Message
        {
            IsUser = true,
            Content = message
        };
        messageThread.Messages.Add(userMessage);
        await _context.SaveChangesAsync();
        await Clients.Caller.SendAsync(
            "ReceiveMessage",
            _mapper.Map<MessageDto>(userMessage)
        );

        var aiMessage = new Message
        {
            IsUser = false,
            Content = "I'm thinking..."
        };
        messageThread.Messages.Add(aiMessage);
        await _context.SaveChangesAsync();
        await Clients.Caller.SendAsync(
            "ReceiveMessage",
            _mapper.Map<MessageDto>(aiMessage)
        );
    }
}
