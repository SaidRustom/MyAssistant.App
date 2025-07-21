using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ChatMessage.Get;

public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, ICollection<ChatMessageDto>>
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IMapper _mapper;
    private readonly IChatMessageRepository _repo;
    
    public GetConversationQueryHandler(ILoggedInUserService loggedInUserService, IMapper mapper, IChatMessageRepository repo)
    {
        _loggedInUserService = loggedInUserService;
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<ICollection<ChatMessageDto>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
    {
        //Validation not required since we are retrieving CurrentUserId messsages..

        var messages = await _repo.GetListAsync(request.OtherUserId, request.BeforeMessageId);
        
        var dtos = _mapper.Map<List<ChatMessageDto>>(messages);
        
        return dtos;
    }
}