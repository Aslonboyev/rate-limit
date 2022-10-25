using AspNetCoreRateLimit;

namespace DemoRateLimitApp.Configurations
{
    public static class RateLimitingConfiguration
    {
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.Configure<IpRateLimitOptions>(option =>
            {
                option.EnableEndpointRateLimiting = true;
                option.StackBlockedRequests = false;
                option.HttpStatusCode = 429;
                option.RealIpHeader = "X-Real-IP";
                option.ClientIdHeader = "X-ClientId";
                option.GeneralRules = new List<RateLimitRule> {

                    //new RateLimitRule
                    //{
                    //    Endpoint = "*",
                    //    Period = "1s",
                    //    Limit = 2,
                    //},

                    //new RateLimitRule()
                    //{
                    //    Endpoint="*",
                    //    Period="1m",
                    //    Limit=10,
                    //},

                    new RateLimitRule()
                    {
                        Endpoint="*",
                        Period="10m",
                        Limit=10,
                    },
                };
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            services.AddInMemoryRateLimiting();
        }
    }
}
