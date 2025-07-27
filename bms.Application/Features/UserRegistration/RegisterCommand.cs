using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.UserRegistration
{
    public class RegisterCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
