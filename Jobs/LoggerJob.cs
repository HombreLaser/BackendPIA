// Singleton service.
namespace BackendPIA.Jobs {
    public class LoggerJob : IHostedService {
        private Timer? _timer;
        private readonly IWebHostEnvironment _env;
        private readonly string _log_file = "Log.txt";

	    public LoggerJob(IWebHostEnvironment env) { 
            _env = env;
        }

	    public Task StartAsync(CancellationToken cancel_token) {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        
            return Task.CompletedTask;
        }

	    public void DoWork(object state) {
            LogToFile("Executing task\n" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
	
	    public Task StopAsync(CancellationToken cancel_token) {
            LogToFile("Stopping task...\n");
            _timer.Dispose();
            return Task.CompletedTask;
        }

        private void LogToFile(string message) {
	        var path = $@"{_env.ContentRootPath}/wwwroot/{_log_file}";

	        using (StreamWriter w = new StreamWriter(path, append: true)) {
		        w.WriteLine(message);
	        }
	    }   
    }
}