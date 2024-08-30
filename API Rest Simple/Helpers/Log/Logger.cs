namespace API_Rest_Simple.Helpers.Log
{
    public class Logger
    {
        private string _logFilePath ="";
        private readonly IConfiguration _configuration;
        private readonly int _logFileSize = 0;
        private int logNumber = 0;

        public Logger(IConfiguration configuration)
        {
            _configuration = configuration;
            _logFileSize = int.Parse(_configuration["Logging:File:fileSize"].ToString());
        }

        public void Log(string message)
        {
            using (var writer = new StreamWriter(GetLogFilePath(), true))
            {
                writer.WriteLine($"{DateTime.UtcNow.AddHours(-3)}: {message}");
            }
        }

        public void LogError(string error)
        {
            
            using (var writer = new StreamWriter(GetLogFilePath(), true))
            {
                writer.WriteLine($"{DateTime.UtcNow.AddHours(-3)}: ERROR - {error}");
            }
        }

        private string GetLogFilePath()
        {          
            bool valid_File = false;
            while (!valid_File)
            {
                string logFilePath = _configuration["Logging:File:fileRoute"] + _configuration["Logging:File:fileName"] + DateTime.UtcNow.ToString("yyyyMMdd") + "_" + logNumber.ToString() + ".txt";
                // Ensure the log directory exists
                if (!File.Exists(logFilePath))
                {
                    using (var fileStream = File.Create(logFilePath)) { }
                    _logFilePath = logFilePath;
                    return _logFilePath;
                }

                //check the length of the file
                int lineCount = 0;
                bool File_too_long = false;
                foreach (var line in File.ReadLines(logFilePath))
                {
                    lineCount++;
                    if (lineCount > _logFileSize)
                    {
                        File_too_long = true;
                        continue;
                    } 
                }
                //Check the next file
                if (File_too_long) logNumber = logNumber + 1;
                else
                {
                    valid_File = true;
                    _logFilePath = logFilePath;
                }
                
            } 
            return _logFilePath;
        }
    }
}
