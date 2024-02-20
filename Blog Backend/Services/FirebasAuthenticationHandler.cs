using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Blog_Backend.Services
{
    public class FirebasAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private FirebaseApp _firebaseApp;

        public FirebasAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, FirebaseApp firebaseApp) : base(options, logger, encoder, clock)
        {
            _firebaseApp = firebaseApp;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string? bearToken = Request.Headers.Authorization;

            if (bearToken == null || !bearToken.StartsWith("Bearer"))
            {
                return AuthenticateResult.Fail("Invalid schema");
            }


            string token = bearToken.Split(' ')[1];

            try
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

                var claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity>() {

                new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(FirebasAuthenticationHandler))


            });


                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, JwtBearerDefaults.AuthenticationScheme));



            }
            catch (Exception ex)
            {

                return AuthenticateResult.Fail(ex);
            }

           

        }
  

            private IEnumerable<Claim>? ToClaims(IReadOnlyDictionary<string, object> claims)
            {
                return new List<Claim>
                {
                    new Claim("id", claims["user_id"].ToString()),
                    new Claim("email", claims["email"].ToString()),
                };
            }
        }
    }

 