using CommentService.Application.DTOs.UserDTOs;
using CommentService.Application.Interfaces.Security;
using CommentService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class AuthController: ControllerBase
    {
        private readonly string _imageStoragePath;
        private readonly IAuthService _authService;
        private readonly IUserContextProvider _userContextProvider;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, IUserContextProvider userContextProvider, ILogger<AuthController> logger, UserManager<User> userManager, IConfiguration configuration)
        {
            _authService = authService;
            _userContextProvider = userContextProvider;
            _logger = logger;
            _userManager = userManager;
            _imageStoragePath = configuration.GetSection("ConnectionStrings:ImageStorage").Value!;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(CreateAccountDTO createAccountDto)
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                throw new Exception();
            }

            var result = await _authService.Register(createAccountDto);
            if (result) return Ok();
            
            return BadRequest();

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(CredentialsDTO credentialsDto)
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
            {
                throw new Exception();
            }

            var response = await _authService.Login(credentialsDto);

            return Ok(response);
        }

        [HttpPost]
        [Route("Upload/Picture")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile image)
        {
            var userContext = await _userContextProvider.GetContext(this.User);
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded");
            }

            try
            {
                
                if (!Directory.Exists(_imageStoragePath))
                {
                    Directory.CreateDirectory(_imageStoragePath);
                }

                string fileExtension = Path.GetExtension(image.FileName);

                string uniqueFileName = $"{userContext.UserId}{fileExtension}";
                string filePath = Path.Combine(_imageStoragePath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var user = _userManager.Users.FirstOrDefault(u => u.Id == userContext.UserId);

                string imageUrl = $"{Request.Scheme}://{Request.Host}/{filePath}";

                user.Image = imageUrl;
                await _userManager.UpdateAsync(user);
       
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
