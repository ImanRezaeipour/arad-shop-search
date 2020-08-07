using Framework.Core.Helper;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;


namespace Framework.AccountsManagementIdentity.Providers
{
    internal class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            //var refreshTokenId = context.Ticket.Identity.Claims.FirstOrDefault(a => a.Type == "SessionID").Value;

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");


            //    var token = new RefreshToken()
            //    {
            //        Id = CustomHash.GetHash(refreshTokenId),
            //        ClientId = clientid,
            //        Subject = context.Ticket.Identity.Name,
            //        UserID = Convert.ToInt64(context.Ticket.Identity.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value),
            //        IssuedUtc = DateTime.UtcNow,
            //        ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            //    };

            //    context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            //    context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            //    token.ProtectedTicket = context.SerializeTicket();

            //    var result = await _repo.AddRefreshToken(token);

            //    if (result)
            //    {
            //        context.SetToken(refreshTokenId);
            //    }
            //}
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            #region Set Allow-Origin in Header

            //var allowedOrigin = "*";// context.OwinContext.Get<string>("as:clientAllowedOrigin");
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST, GET, OPTIONS, DELETE, PUT" });
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding" });

            #endregion Set Allow-Origin in Header

            string hashedTokenId = CustomHash.GetHash(context.Token);

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

            //    if (refreshToken != null)
            //    {
            //        //Get protectedTicket from refreshToken class
            //        context.DeserializeTicket(refreshToken.ProtectedTicket);
            //        var result = await _repo.RemoveRefreshToken(hashedTokenId);
            //    }
            //}
        }
    }
}