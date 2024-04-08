using System.Security.Claims;

namespace BannerService.Services
{
    public class ClaimService
    {
        private readonly ClaimsIdentity? _identity;

        public ClaimService()
        {
            _identity = new HttpContextAccessor().HttpContext?.User.Identities.FirstOrDefault();
        }

        public void AddClaims(params Claim[] claims)
        {
            if (_identity == null)
            {
                return;
            }

            _identity.AddClaims(claims);
        }

        public void RemoveClaims(params string[] keys)
        {
            if (_identity == null)
            {
                return;
            }

            var claims = _identity.FindAll(x => keys.Contains(x.Type)).ToList();
            foreach (var claim in claims)
            {
                _identity.RemoveClaim(claim);
            }
        }
    }
}
