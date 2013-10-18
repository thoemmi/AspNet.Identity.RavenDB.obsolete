using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Raven.Client;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserLoginStore<TUser> where TUser : IdentityUser {
        public async Task CreateAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            await _session.StoreAsync(user);
            await _session.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            await _session.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            _session.Delete(user);
            await _session.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<TUser> FindByIdAsync(string userId) {
            return await _session.LoadAsync<TUser>(userId);
        }

        public async Task<TUser> FindByNameAsync(string userName) {
            return await _session.Query<TUser>().FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (login == null) {
                throw new ArgumentNullException("login");
            }

            user.Logins.Add(new IdentityUserLogin {
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });
            return Task.FromResult(0);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (login == null) {
                throw new ArgumentNullException("login");
            }

            IdentityUserLogin entity =
                user.Logins.SingleOrDefault(
                    l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);
            if (entity != null) {
                user.Logins.Remove(entity);
            }
            return Task.FromResult(0);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            IList<UserLoginInfo> userLoginInfos =
                user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
            return Task.FromResult(userLoginInfos);
        }

        public async Task<TUser> FindAsync(UserLoginInfo login) {
            ThrowIfDisposed();
            if (login == null) {
                throw new ArgumentNullException("login");
            }

            return await _session.Query<TUser>()
                .Where(
                    user =>
                        user.Logins.Any(
                            l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey))
                .FirstOrDefaultAsync();
        }
    }
}