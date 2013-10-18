using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserRoleStore<TUser> where TUser : IdentityUser {
        public Task AddToRoleAsync(TUser user, string role) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrEmpty(role)) {
                throw new ArgumentNullException("role");
            }

            user.Roles.Add(role);
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(TUser user, string role) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrEmpty(role)) {
                throw new ArgumentNullException("role");
            }

            user.Roles.Remove(role);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult((IList<string>) user.Roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string role) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrEmpty(role)) {
                throw new ArgumentNullException("role");
            }

            return Task.FromResult(user.Roles.Contains(role));
        }
    }
}