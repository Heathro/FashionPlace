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

        CreateMap<MessageThreadDto, ModelChatRequest>();
        CreateMap<MessageDto, ModelChatMessage>()
            .ForMember(m => m.role, o => o.MapFrom(m => m.IsUser ? "user" : "assistant"));
    }
}
