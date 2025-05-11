using Grpc.Core;
using SigmaBank.Auth;
using SigmaBank.Core.Entities;
using SigmaBank.Core.Services.Users;
using SigmaBank.Core.Services.Users.Models;
using SigmaBank.Protos;
using System.Security.Claims;

namespace SigmaBank.GrpcServices;

internal class AuthGrpcService(
    IUsersService usersService,
    ITokenService tokenService) : AuthService.AuthServiceBase
{
    public override Task<SendConfirmationCodeResponse> SendConfirmationCode(SendConfirmationCodeRequest request, ServerCallContext context)
    {
        string result = tokenService.GenerateVerificationToken(request.PhoneNumber);

        return Task.FromResult(
            new SendConfirmationCodeResponse
            {
                Token = new Token
                {
                    Value = result,
                },
            });
    }

    [AuthorizeToken(TokenTypes.Verification)]
    public override async Task<ConfirmResponse> Confirm(ConfirmRequest request, ServerCallContext context)
    {
        string phoneNumber = ResolvePhoneNumber(context);
        string result = tokenService.GenerateConfirmedToken(phoneNumber);

        User? user = await ResolveUserByPhoneNumber(phoneNumber, context.CancellationToken);

        return new ConfirmResponse
        {
            Token = new Token
            {
                Value = result,
            },
            NeedRegistration = user is null,
        };
    }

    [AuthorizeToken(TokenTypes.Confirmed)]
    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        string phoneNumber = ResolvePhoneNumber(context);

        if (await usersService.UserExistsByPhoneNumberAsync(phoneNumber, context.CancellationToken))
        {
            throw new InvalidOperationException("User with phone number already exists");
        }

        var model = new CreateUserModel(phoneNumber, request.FirstName, request.LastName, request.Age);

        User user = await usersService.CreateUserAsync(model, context.CancellationToken);

        string token = tokenService.GenerateAuthorizedToken(user);

        return new RegisterResponse
        {
            Token = new Token
            {
                Value = token,
            },
        };
    }

    [AuthorizeToken(TokenTypes.Confirmed)]
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        string phoneNumber = ResolvePhoneNumber(context);

        if (!await usersService.UserExistsByPhoneNumberAsync(phoneNumber, context.CancellationToken))
        {
            throw new InvalidOperationException("User with this phone number not found");
        }

        User user = await usersService.GetUserByPhoneNumberAsync(phoneNumber, context.CancellationToken);

        string token = tokenService.GenerateAuthorizedToken(user);

        return new LoginResponse
        {
            Token = new Token
            {
                Value = token,
            },
        };
    }

    private async Task<User?> ResolveUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        return await usersService.UserExistsByPhoneNumberAsync(phoneNumber, cancellationToken)
            ? await usersService.GetUserByPhoneNumberAsync(phoneNumber, cancellationToken)
            : null;
    }

    private string ResolvePhoneNumber(ServerCallContext context)
    {
        ClaimsPrincipal claimsPrincipal = context.GetHttpContext().User;

        return tokenService.ResolvePhoneNumber(claimsPrincipal);
    }
}