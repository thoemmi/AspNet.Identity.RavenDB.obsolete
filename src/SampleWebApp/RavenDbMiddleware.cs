using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;

namespace SampleWebApp {
    public class RavenDbMiddleware : OwinMiddleware {
        public RavenDbMiddleware(OwinMiddleware next) : base(next) {
        }

        public override async Task Invoke(IOwinContext context) {
            var asyncDocumentSession = RavenConfig.DocumentStore.OpenAsyncSession();
            HttpContext.Current.Items["CurrentRequestRavenSession"] = asyncDocumentSession;
            await Next.Invoke(context);
            if (context.Response.StatusCode < 500) {
                await asyncDocumentSession.SaveChangesAsync();
            }
        }
    }
}