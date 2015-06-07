using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleMetrics.Model;
using SimpleMetrics.Util;

namespace SimpleMetrics.Server
{
    public class MetricsServer
    {
        private readonly TcpListener socketListener_;

        public MetricsServer(short port)
        {
            socketListener_ = new TcpListener(IPAddress.Any, port);
            var listenerThread = new Thread(ListenForClients);
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            while (true) // Forever and ever
            {
                TcpClient client = socketListener_.AcceptTcpClient();
                Parallel.Invoke(() => HandleClientConnection(client));
            }
        }

        private void HandleClientConnection(TcpClient client)
        {
            using (NetworkStream connectionStream = client.GetStream())
            {
                StringBuilder metricBuilder = new StringBuilder();
                int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];
                List<byte> allData = new List<byte>(bufferSize);

                while (true)
                {
                    int bytesRead = 0;

                    try
                    {
                        bytesRead = connectionStream.Read(buffer, 0, bufferSize);
                    }
                    catch (Exception e)
                    {
                        // TODO: Log, gracefully handle
                        break;
                    }

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    allData.AddRange(buffer.Take(bytesRead));
                }

                Metrics serializedMetrics;
                try
                {
                    serializedMetrics = Serializer<Metrics>.JsonDeserialize(allData.ToArray());
                }
                catch (Exception e)
                {
                    // TODO: Log
                }
            }
        }
    }
}