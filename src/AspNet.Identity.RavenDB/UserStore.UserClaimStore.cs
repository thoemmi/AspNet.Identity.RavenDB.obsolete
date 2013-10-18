using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserClaimStore<TUser> where TUser : IdentityUser {
        public Task<IList<Claim>> GetClaimsAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            IList<Claim> claims = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(claims);
        }

        public Task AddClaimAsync(TUser user, Claim claim) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (claim == null) {
                throw new ArgumentNullException("claim");
            }

            user.Claims.Add(new IdentityUserClaim { ClaimType = claim.Type, ClaimValue = claim.Value });
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (claim == null) {
                throw new ArgumentNullException("claim");
            }

            user.Claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
            return Task.FromResult(0);
        }
    }
}