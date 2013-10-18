using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserPasswordStore<TUser> where TUser : IdentityUser {
        public Task SetPasswordHashAsync(TUser user, string passwordHash) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(!String.IsNullOrEmpty(user.PasswordHash));
        }
    }
}