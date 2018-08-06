using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2.Class
{
    public class Descarga
    {
        String destino;
        public void HandleDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            lock (e.UserState)
            {
                Console.SetCursorPosition(0, Console.CursorTop);                
                Console.Write(new string(' ',Console.WindowWidth));                
                Console.WriteLine(String.Format("Archivo {0} descargado",destino));                
                Monitor.Pulse(e.UserState);
            }
        }

      
        public void HandleDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {            
            Console.SetCursorPosition(0, Console.CursorTop);                       
            Console.Write("Descargando  {0} Kb  de {1} Kb. {2} % completado...",                                      
                          ((e.BytesReceived/1024)),
                          ((e.TotalBytesToReceive)/1024),
                            e.ProgressPercentage);
          






        }
        public void DownloadFile(string  url, string des)
        {
            this.destino = des;
            using (var wc = new WebClient())
            {
                
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(HandleDownloadComplete);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(HandleDownloadProgress);

                var syncObject = new Object();
                lock (syncObject)
                {
                    
                    wc.DownloadFileAsync(new Uri(url), des, syncObject);
                    //This would block the thread until download completes
                    Monitor.Wait(syncObject);
                    
                }
            }

            //Do more stuff after download was complete
        }

       

      
    }
}
