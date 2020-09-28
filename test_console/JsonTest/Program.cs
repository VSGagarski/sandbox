using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IdentityServer4.Models;
using System.Text.Json;
using System.IO;
using IdentityServer4;
using System.Collections.Generic;


namespace JsonTest
{



    class Program
    {
        public static void Main(string[] args)
        {
            Func<int, string> str = x => "der";
            Console.WriteLine(str(2));

            IAa aa = new Wwww { z = 1 };
            IBb bb = new Eeee { z = 1 };
            IABab aBab = new Qqq { z = 3 };

            aa = aBab;    
            bb = aBab;    


            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(configHost =>
            {

                configHost
                        .AddFilter("Microsoft", LogLevel.Information)
                        .AddFilter("System", LogLevel.Information)
                        .AddFilter("Target.Program", LogLevel.Debug)
                        .AddConsole();
            })
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.AddEnvironmentVariables();
            })
                .ConfigureServices((hostContext, services) =>
                {

                    var c = new Client
                    {
                        ClientId = "mvc",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,
                        RequireConsent = false,
                        AlwaysIncludeUserClaimsInIdToken = true,

                        // where to redirect to after login
                        // RedirectUris = { "https://armgateway/signin-oidc" },
                        RedirectUris = { "https://localhost:5556/signin-oidc" },

                        // where to redirect to after logout
                        //    PostLogoutRedirectUris = { "https://armgateway/signout-callback-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:5556/signout-callback-oidc" },

                        AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                        AllowOfflineAccess = true,
                        //Access token life time is  7200 seconds (2 hour)
                        AccessTokenLifetime = 15,
                        //Identity token life time is 7200 seconds (2 hour)
                        IdentityTokenLifetime = 15
                    };
                    var t = new RabbitMqConfig() { Hostname ="test", Password = "test",Port= "Test", Username = "test" };
                    string jsonString;  
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    var test =  new QueueConfig () {StartTime = new Time{Hours=-24, Minutes=-30},EndTime = new Time{Hours=22, Minutes=67}};
                    jsonString = JsonSerializer.Serialize(test, options);
                    File.WriteAllText("./test.json", jsonString);
                });
    }
    public interface IAa
    {
        int z { get; set; }
    }
    public interface IBb
    {
        int z { get; set; }
    }
    public interface IABab : IAa, IBb
    {
    }

    public class Qqq : IABab
    {
        public int z { get; set; }
    }

    public class Wwww : IAa
    {
        public int z { get; set; }
    }
    public class Eeee : IBb
    {
        public int z { get; set; }
    }

}
