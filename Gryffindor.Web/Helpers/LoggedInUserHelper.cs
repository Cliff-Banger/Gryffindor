using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Gryffindor.Web.Helpers
{
    public static class LoggedInUserHelper
    {
        internal static Guid GetUserId(IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            return new Guid(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        internal static UserProfile GetUserDetails(IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            var user = new UserProfile()
            {
                FirstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value,
                Surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value,
                PreferredEmail = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                MainInterest = Convert.ToInt32(claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value)
            };
            return user;
        }

        internal static void UpdateUserDetails(IIdentity identity, UserProfile profile)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            var user = new UserProfile()
            {
                FirstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value,
                Surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value,
                PreferredEmail = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                MainInterest = Convert.ToInt32(claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value)
            };
        }
    }
}