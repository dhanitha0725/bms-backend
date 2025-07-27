using bms.Application.Abstractions.Interfaces;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace bms.Application.Features.UserLogin
{
    public class LoginCommandHandler(
        IGenericRepository<User, Guid> userRepository,
        IJwtTokenService jwt,
        ILogger logger,
        IPasswordHasher<User> passwordHasher) 
        : IRequestHandler<LoginCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(
            LoginCommand request, 
            CancellationToken cancellationToken)
        {
            // get user by username
            var users = await userRepository
                .GetAllAsync(u => u.Username == request.Username, cancellationToken);
            var user = users.FirstOrDefault();

            // validate user and password
            if (user == null)
            {
                logger.Information("User not found: {Username}", request.Username);
                return Result<string>.Failure(new Error("Invalid password or username"));
            }
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                logger.Information("Invalid password for user: {Username}", request.Username);
                return Result<string>.Failure(new Error("Invalid password or username"));
            }
            var token = jwt.GenerateJwtToken(user);
            return Result<string>.Success(token);
        }
    }
}
