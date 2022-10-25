using Microsoft.AspNetCore;

namespace AspNetCoreSample
{
    public sealed class MyTheoreticalRepository
    {
        public Task<string> GetAuthentication() => Task.FromResult("Hello World");
    }

    public interface ICustomHttpClient
    {
        Task<string> GetAsync();
    }

    public sealed class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _client;
        private readonly MyTheoreticalRepository _repo;

        public CustomHttpClient(HttpClient client, MyTheoreticalRepository repo)
        {
            _client = client;
            _repo = repo;
        }

        private async Task<HttpClient> GetConfiguredClient()
        {
            var auth = await _repo.GetAuthentication();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", auth);
            return _client;
        }

        public async Task<string> GetAsync()
        {
            var client = await GetConfiguredClient();
            var result = await client.GetStringAsync("https://orf.at");
            return result;
        }
    }

    public sealed class Startup 
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpClient<ICustomHttpClient, CustomHttpClient>();
            services.AddTransient<MyTheoreticalRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
            webHost.Run();
        }
    }
}