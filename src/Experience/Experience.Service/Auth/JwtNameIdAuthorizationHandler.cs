using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Auth
{
    public class JwtNameIdAuthorizationHandler : AuthorizationHandler<JwtNameIdListing>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtNameIdListing requirement)
        {

            var nameIdClaim = context.User.Claims.FirstOrDefault(c => c.Type.Equals(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                StringComparison.OrdinalIgnoreCase));

            if (nameIdClaim != null)
            {
                if (requirement.AllowedNames.Contains(nameIdClaim.Value))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

    public class JwtNameIdListing : IAuthorizationRequirement
    {
        public JwtNameIdListing(params string[] allowedNames)
        {
            AllowedNames = allowedNames;
        }

        public string[] AllowedNames { get; private set; }
    }
}
