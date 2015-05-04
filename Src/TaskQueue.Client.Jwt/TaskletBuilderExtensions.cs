using System;
using System.IdentityModel.Tokens;

namespace TaskQueue.Client.Jwt
{
    public static class TaskletBuilderExtensions
    {
        /// <summary>
        /// Sets the callback authorization to Bearer + Token.
        /// </summary>
        /// <param name="builder">The Tasklet builder to set the authorization on.</param>
        /// <param name="token">The token to use as the bearer token.</param>
        /// <returns>A tasklet builder to continue building upon.</returns>
        public static TaskletBuilder AuthorizeWithJwt(this TaskletBuilder builder, string token)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Authorization("Bearer", token);
        }

        /// <summary>
        /// Sets the callback authorization to Bearer + Token.
        /// </summary>
        /// <param name="builder">The Tasklet builder to set the authorization on.</param>
        /// <param name="token">The token to use as the bearer token.</param>
        /// <returns>A tasklet builder to continue building upon.</returns>
        public static TaskletBuilder AuthorizeWithJwt(this TaskletBuilder builder, JwtSecurityToken token)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Authorization("Bearer", new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
