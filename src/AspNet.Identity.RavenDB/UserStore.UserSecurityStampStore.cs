using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserSecurityStampStore<TUser> where TUser : IdentityUser {
        public Task SetSecurityStampAsync(TUser user, string stamp) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }
    }
}