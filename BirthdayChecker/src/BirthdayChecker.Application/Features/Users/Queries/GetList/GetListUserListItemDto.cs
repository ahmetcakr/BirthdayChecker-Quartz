using Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}