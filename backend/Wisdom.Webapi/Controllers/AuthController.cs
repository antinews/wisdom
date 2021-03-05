using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using Wisdom.Webapi.Auth;
using Wisdom.Webapi.Extensions;
using Sugar.Enties;
using static Wisdom.Webapi.Entities.Enum.CommonEnum;

namespace Wisdom.Webapi.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController:BaseController
    {
        private readonly AppAuthenticationSettings _appSettings;
        public AuthController(IOptions<AppAuthenticationSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetToken(string username, string password)
        {
            System.Console.WriteLine("123".Md5Encrypt());
            var response = ResponseModelFactory.CreateInstance;
            var user = base.db.Queryable<sys_users>().First(x => x.UserName == username.Trim());
            if (user == null)
            {
                response.SetFailed("用户不存在");
                return Ok(response);
            }
            if (user.Password != password.Trim().Md5Encrypt())
            {
                response.SetFailed("密码不正确");
                return Ok(response);
            }
            if (user.IsDisabled == (int)IsDisabled.YES)
            {
                response.SetFailed("账号已被禁用");
                return Ok(response);
            }
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("id",user.Id.ToString()),
                    new Claim("trueName",user.TrueName?.ToString()??""),
                    new Claim("email",user.Email?.ToString()??""),
                    new Claim("roleId",user.RoleId.ToString())
                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

            response.SetData(token);
            return Ok(response);
        }
    }
}
