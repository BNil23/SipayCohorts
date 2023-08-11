using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SipayHafta2.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FakeUserAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool _isFakeUser;

        public FakeUserAttribute(bool isFakeUser = true)
        {
            _isFakeUser = isFakeUser;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_isFakeUser)
            {
                // Fake kullanıcı girişi burada sağlanabilir
                // Örneğin, bir fake kullanıcı nesnesi oluşturup context.HttpContext.User'a atama yapılabilir.
                // Bu, kullanıcının kimlik doğrulaması olmadan isteği işlemesine izin verebilir.

                var fakeClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "fakeuser"),
                    new Claim(ClaimTypes.Role, "User")
                };

                var fakeIdentity = new ClaimsIdentity(fakeClaims, "fake");
                var fakePrincipal = new ClaimsPrincipal(fakeIdentity);

                context.HttpContext.User = fakePrincipal;
            }
            else
            {
                // Normal işlem devam eder, kimlik doğrulaması gereklidir.
            }
        }
    }
}
