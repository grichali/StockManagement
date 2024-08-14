using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Google.Apis.Auth;

namespace api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, IList<string> roles);
        public Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId);
    }
}