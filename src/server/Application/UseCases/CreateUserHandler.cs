using Application.DataDTOs;
using Application.Responses;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.UseCases;

// "using" Alias Directive, change the generic type to suit the Handler return type.
using Response = Response<UserDTO>;

public record CreateUserRequest(
    string Username,
    string Password,
    List<Guid> RoleIds
) : IRequest<Response>;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordService _password;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordService password, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _password = password;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Response> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check if Username is available
            bool isNotAvailable = await _userRepository.AnyAsync(request.Username, cancellationToken);
            if (isNotAvailable)
            {
                return new Response("Username already in use", 400);
            }
        }
        catch
        {
            return new Response("Internal server error", 500);
        }
        
        // Get all the different types of roles
        List<Role> roles = [];
        try
        {
            roles = await _roleRepository.GetRoles(request.RoleIds);
            if (roles.Count == 0)
            {
                return new Response("No roles found", 404);
            }
        }
        catch
        {
            return new Response("Internal Server Error", 500);
        }
        
        // Generate User object 
        User user = new User(request.Username, _password.HashPassword(request.Password));
        user.Roles = roles; // TODO: a user who is creating other users requires different levels of permission to assign roles
        
        try
        {
            // Save user in database
            _userRepository.Create(user);
            // Commit the chages in database
            await _unitOfWork.Commit(cancellationToken);
        }
        catch
        {
            return new Response("Internal Server Error", 500);
        }

        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        
        return new Response("User created", 201, userDTO);
    }
}