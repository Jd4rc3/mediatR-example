using MediatR;
using MediatrExample.Features.Products.Queries;

namespace MediatrExample.Features.Auth.Command;

public class TokenCommand : IRequest<TokenCommandResponse>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class TokenCommandResponse
{
    public string AccessToken { get; set; } = default!;
}