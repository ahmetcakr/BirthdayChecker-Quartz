using AutoMapper;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetListUsers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetUsers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            IPaginate<User> users = await _userRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return response;
        }
    }
}