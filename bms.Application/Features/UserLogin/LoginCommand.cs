using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.UserLogin
{
    public class LoginCommand: IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
  
    }
}
