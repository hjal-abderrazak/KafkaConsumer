//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using KafkaConsumer.Helper;

//namespace KafkaConsumer.Middlewares
//{
//    public class JwtMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly TokenValidationParameters _tokenValidationParameters;

//        public JwtMiddleware(RequestDelegate next, TokenValidationParameters tokenValidationParameters)
//        {
//            _next = next;
//            _tokenValidationParameters = tokenValidationParameters;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//            if (token != null)
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                try
//                {
//                    tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);
//                    context.User = (token);
//                }
//                catch
//                {
//                    // If the token is not valid, do nothing and continue with the next middleware in the pipeline.
//                }
//            }

//            await _next(context);
//        }
//    }

//    public static class JwtMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder, TokenValidationParameters tokenValidationParameters)
//        {
//            return builder.UseMiddleware<JwtMiddleware>(tokenValidationParameters);
//        }
//    }
//}
