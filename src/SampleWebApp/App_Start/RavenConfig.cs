using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace SampleWebApp {
    public static class RavenConfig {
        public static IDocumentStore DocumentStore { get; private set; }

        public static void Init() {
            InitializeDocumentStore();
        }

        private static void InitializeDocumentStore() {
            if (DocumentStore != null) {
                return; // prevent misuse
            }

            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);

            DocumentStore = new EmbeddableDocumentStore {
                ConnectionStringName = "RavenDB",
                UseEmbeddedHttpServer = true
            }.Initialize();
        }
    }
}