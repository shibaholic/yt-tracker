using Application.DataDTOs;
using Application.Responses;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.UseCases;

// "using" Alias Directive, change the generic type to suit the Handler return type.
using Response = Response<UserDTO>;

public record RefreshTokenRequest
(
    // using RefreshToken as the Username may be public to other users.
    Guid RefreshToken
) : IRequest<Response>;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Response>
{
    private readonly IUserRepository _userRepository;

    public RefreshTokenHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        User? user;
        try
        {
            user = await _userRepository.GetUserByRefreshCode(request.RefreshToken, cancellationToken);
            if (user is null)
            {
                return new Response("User not found", 404);
            }
        }
        catch
        {
            return new Response("Internal server error", 500);
        }
        
        // Generate a new refresh token
        user.GenerateRefreshToken();

        try
        {
            // Commit changes to database
            // await _unitOfWork.Commit(cancellationToken);
        }
        catch
        {
            return new Response("Internal server error", 500);
        }
        
        // Mapper user to dto
        UserDTO userDTO = new UserDTO() { Id = Guid.NewGuid(), Roles = new List<RoleDTO>(), Username = "testUser" }; // _mapper.Map<UserDTO>(user);

        return new Response("Token refreshed", 200, userDTO);
    }
}