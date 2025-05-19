using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SigmaBank.Auth;
using SigmaBank.Core.Services.Accounts;
using SigmaBank.Core.Services.CardsPrivateData;
using SigmaBank.Protos;
using System.Security.Claims;
using CardType = SigmaBank.Core.Entities.CardType;

namespace SigmaBank.GrpcServices;

internal class AccountsGrpcService(
    IAccountsService accountsService,
    ICardsPrivateDataService cardsPrivateDataService,
    ITokenService tokenService) : AccountsService.AccountsServiceBase
{
    public override async Task<GetAccountsResponse> GetAccounts(GetAccountsRequest request, ServerCallContext context)
    {
        long userId = ResolveUserId(context);

        IReadOnlyCollection<Core.Entities.Account> result = await accountsService.GetAccountsByUserIdAsync(
            userId,
            context.CancellationToken);

        return new GetAccountsResponse
        {
            Accounts =
            {
                result.Select(account => new Account
                {
                    Id = account.AccountId,
                    UserId = account.UserId,
                    Amount = decimal.ToDouble(account.Amount),
                    Cards =
                    {
                        account.Cards.Select(card => new Card
                        {
                            Id = card.CardId,
                            AccountId = card.AccountId,
                            Number = card.Number,
                            Type = card.Type switch
                            {
                                CardType.Unknown => Protos.CardType.Unknown,
                                CardType.Mastercard => Protos.CardType.Mastercard,
                                CardType.Visa => Protos.CardType.Visa,
                                CardType.MIR => Protos.CardType.Mir,
                                _ => throw new ArgumentOutOfRangeException(nameof(card)),
                            },
                        }),
                    },
                }),
            },
        };
    }

    public override async Task<GetCardPrivateDataResponse> GetCardPrivateData(
        GetCardPrivateDataRequest request,
        ServerCallContext context)
    {
        Core.Entities.CardPrivateData result = await cardsPrivateDataService.GetCardPrivateDataByCardIdAsync(
            request.CardId,
            context.CancellationToken);

        return new GetCardPrivateDataResponse
        {
            ExpirationDate = result.ExpirationDate.ToTimestamp(),
            Code = result.Code,
        };
    }

    protected long ResolveUserId(ServerCallContext context)
    {
        ClaimsPrincipal claimsPrincipal = context.GetHttpContext().User;

        return tokenService.ResolveUserId(claimsPrincipal);
    }
}