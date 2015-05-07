using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace SSODemo
{
    public static class Config
    {
        public static IEnumerable<InMemoryUser> GetUsers()
        {
            var users = new List<InMemoryUser>();
            users.Add(new InMemoryUser
            {
                Subject = "admin", // this is a unique identifier
                Username = "admin",
                Password = "pass"
            });
            return users;
        }

        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>();
            clients.Add(new Client
            {
                ClientName = "TestClient",
                ClientId = "test",
                Flow = Flows.Implicit,
                ClientSecrets = new List<ClientSecret>
                    { 
                        new ClientSecret("secret".Sha256())
                    },
                RedirectUris = new List<string>
                {
                    "Http://example.local/" //using a fake url for now
                }

            });
            return clients;
        }

        public static IEnumerable<Scope> GetScopes()
        {
            var scopes = new List<Scope>();
            scopes.Add(new Scope
            {
                DisplayName = "Api Access",
                Name = "api",
                Type = ScopeType.Resource
            });
            return scopes;
        }
    }
}