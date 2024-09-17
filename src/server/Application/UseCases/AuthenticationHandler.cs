using Application.Responses;
using Application.DataDTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.UseCases;

// TODO: AbstractValidation<AuthenticationRequest> RuleFor(x => x ...)

// "using" Alias Directive, change the generic type to suit the Handler return type.
using Response = Response<UserDTO>;

public record AuthenticationRequest
(
    string Username,
    string Password
) : IRequest<Response>;

public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, Response>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _password;
    
    public AuthenticationHandler(IUserRepository repository, IMapper mapper, IPasswordService password)
    {
        _repository = repository;
        _mapper = mapper;
        _password = password;
    }

    public async Task<Response> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
    {
        User? user;
        try
        {
            // Search user in database
            user = await _repository.GetUserByUsernameAsync(request.Username, cancellationToken);
            if (user is null)
                return new Response("User not found", 404);
        }
        catch
        {
            return new Response("Internal Server Error", 500);
        }

        // Validate user password
        bool isVerified = _password.VerifyHashedPassword(user.Password, request.Password);
        if (!isVerified)
        {
            return new Response("Password dont match", 400);
        }

        // Mapper user to DTO
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return new Response("User authenticated", 200, userDTO);
    }
}