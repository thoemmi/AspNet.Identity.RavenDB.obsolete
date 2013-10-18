using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.RavenDB {
    public class IdentityUser : IUser {
        public IdentityUser() {
            Claims = new List<IdentityUserClaim>();
            Roles = new List<string>();
            Logins = new List<IdentityUserLogin>();
        }

        public IdentityUser(string userName)
            : this() {
            UserName = userName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public List<IdentityUserLogin> Logins { get; set; }
        public List<IdentityUserClaim> Claims { get; set; }
        public List<string> Roles { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
    }

    public class IdentityUserLogin {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual string UserId { get; set; }
    }

    public class IdentityUserClaim {
        public virtual int Id { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
    }
}