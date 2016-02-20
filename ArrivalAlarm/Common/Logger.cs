using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace ArrivalAlarm.Common
{
    public static class Logger
    {
        private static IStorageFile _logFile;

        /// <summary>
        /// Creates log file with unique name for each day 
        /// </summary>
        /// <returns></returns>
        static public async Task CreateLoggerAsync()
        {
            if(_logFile == null)
            {
                string fileName = $"log_file_from_{DateTime.Now.ToString().Split(' ').FirstOrDefault()}";

                try
                {
                    _logFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                }
                catch (FileNotFoundException ex)
                {
                    throw;
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Writes message to log file
        /// </summary>
        /// <param name="message">Message to write</param>
        /// <returns></returns>
        static public async Task WriteLogAsync(string message)
        {
            await FileIO.WriteTextAsync(_logFile, $"{DateTime.Now.ToString().Split(' ')[1]}>{message}\n");
        }
    }
}
