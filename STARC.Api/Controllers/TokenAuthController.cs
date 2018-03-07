using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using STARC.Api.Controllers;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace STARC.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/TokenAuth")]
    public class TokenAuthController : BaseController
    {
        private readonly IUserAppService __userApp;
        private readonly IPasswordAppService __passwordApp;

        public TokenAuthController(IUserAppService userApp, IPasswordAppService passwordApp, ILogger<TokenAuthController> logger)
            : base(userApp, logger)
        {
            __userApp = userApp;
            __passwordApp = passwordApp;
        }

        [HttpPost]
        public string GetAuthToken([FromBody]User user)
        {
            var searchedUser = __userApp.GetByUsername(user.Username);
                        
            var hashPassword = __passwordApp.GetHashPassword(searchedUser.UserId);
            var password = __passwordApp.GetCriptoPassword(user.Password, hashPassword);

            var _existUser = __userApp.GetByUsernameAndPassword(user.Username, password);
            
            if (_existUser != null)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(_existUser, expiresIn);

                return JsonConvert.SerializeObject(new ApiResponse(ApiResponseState.Success, 
                    new
                    {
                        requertAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokeyType = TokenAuthOption.TokenType,
                        accessToken = token,
                        loggedUser = _existUser
                    }));
            }
            else
            {
                return JsonConvert.SerializeObject(new ApiResponse(ApiResponseState.Failed, "Username or password is invalid"));
            }
        }

        private string GenerateToken(UserToQueryViewModel user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Username, "TokenAuth"),
                new[] {
                    //new Claim("UserId", user.UserId.ToString())
                    new Claim("UserId", user.UserId.ToString())
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }
    }
}
