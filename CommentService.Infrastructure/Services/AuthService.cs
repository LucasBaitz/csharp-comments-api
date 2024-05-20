using AutoMapper;
using CommentService.Application.DTOs.UserDTOs;
using CommentService.Application.Interfaces.Security;
using CommentService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _tokenService;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IJwtTokenGenerator tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<bool> Register(CreateAccountDTO createUser)
        {
            var newUser = _mapper.Map<User>(createUser);

            var createResult = await _userManager.CreateAsync(newUser, createUser.Password);

            return createResult == IdentityResult.Success;
        }

        public async Task<SuccessfulLoginDTO> Login(CredentialsDTO userLoginDto)
        {
            var loginAttempt = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);

            if (!loginAttempt.Succeeded)
                throw new Exception();

            var user = await _signInManager.UserManager.Users
                .FirstOrDefaultAsync(user => user.NormalizedUserName == userLoginDto.UserName.ToUpper())
                ?? throw new Exception();

            string token = _tokenService.GenerateToken(user);

            SuccessfulLoginDTO responseDto = _mapper.Map<SuccessfulLoginDTO>(user);
            responseDto.Token = token;

            return responseDto;
        }
        public async Task DeleteAccount(Guid userId, CredentialsDTO credentials)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new Exception();
            }

            await _userManager.DeleteAsync(user);
            await _userManager.UpdateAsync(user);
        }
    }
}
