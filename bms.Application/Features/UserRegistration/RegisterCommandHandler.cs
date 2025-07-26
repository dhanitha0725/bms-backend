using bms.Application.Abstractions.Interfaces;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace bms.Application.Features.UserRegistration
{
    public class RegisterCommandHandler(
        IGenericRepository<User, Guid> useRepository,
        IJwtTokenService jwt,
        IPasswordHasher<User> passwordHasher) 
        : IRequestHandler<RegisterCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(
            RegisterCommand request, 
            CancellationToken cancellationToken)
        {
            if (await useRepository.ExistsAsync(u => u.Username == request.Username, cancellationToken))
            {
                return Result<string>.Failure(new Error("User with this email already exists."));
            }

            var user = new User
            {
                Username = request.Username,
            };

            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

            await useRepository.AddAsync(user, cancellationToken);

            var token = jwt.GenerateJwtToken(user);

            return Result<string>.Success(token);
        }
    }
}
