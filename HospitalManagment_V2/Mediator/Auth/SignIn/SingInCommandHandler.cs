using HospitalManagment_V2.Services;
using MediatR;

namespace HospitalManagment_V2.Mediator.Auth.SignIn;

public class SignInCommandHandler(IAuthService authService) : IRequestHandler<SignInCommand, SignInResponseDto>
{
    public async Task<SignInResponseDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var token = authService.GetToken(request.Request.Username);

        return new SignInResponseDto()
        {
            AccessToken = token,
        };
    }
}
