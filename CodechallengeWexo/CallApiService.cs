namespace CodechallengeWexo
{
    public class CallApiService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CallApiService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dataFetchingService = scope.ServiceProvider.GetRequiredService<DataFetchingService>();

                // Fetch and save movies and series on startup
                await dataFetchingService.FetchAndSaveMovies();
                await dataFetchingService.FetchAndSaveSeries();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
