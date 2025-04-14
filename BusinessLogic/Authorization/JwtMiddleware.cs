using Microsoft.Extensions.Options;
using BusinessLogic.Helpers;
using Domain.Interfaces;
using Domain.Wrapper;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IRepositoryWrapper wrapper, IJwtUtils jwtUtils)
        {
            // Получаем JWT-токен из заголовка Authorization
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Проверяем токен и получаем ID пользователя
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // Если токен валиден, получаем пользователя по ID и добавляем его в контекст
                context.Items["User"] = await wrapper.User.GetByIdWithToken(accountId.Value);
            }

            // Продолжаем выполнение следующего middleware
            await _next(context);
        }
    }
}