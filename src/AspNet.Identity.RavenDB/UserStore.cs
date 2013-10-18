using System;
using Microsoft.AspNet.Identity;
using Raven.Client;

namespace AspNet.Identity.RavenDB {
    public partial class UserStore<TUser> : IUserStore<TUser> where TUser : IdentityUser {
        private IAsyncDocumentSession _session;
        private bool _disposed;

        public UserStore(IAsyncDocumentSession session) {
            _session = session;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            _session = null;
            _disposed = true;
        }

        private void ThrowIfDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}