using AutoMapper;
using AIService.DTOs;
using AIService.Entities;

namespace AIService.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<MessageThread, MessageThreadDto>();
        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, Message>();
    }
}
