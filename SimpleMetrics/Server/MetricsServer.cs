using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using SimpleMetrics.Model;
using SimpleMetrics.Model.Database;
using SimpleMetrics.Util;

namespace SimpleMetrics.Server
{
    public class MetricsServer
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();
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
                        LOG.Error(e, "Caught unexpected exception while attempting to unpack network stream");
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
                    LOG.Error(e, "Caught unexpected exception while attempting to deserialize {0}", allData);
                    return;
                }

                using (var context = new MetricsContext())
                {
                    // TODO: Make this not suck
                    foreach (DatedCount count in serializedMetrics.Counts)
                    {
                        Application application = count.Count.Operation.Application;
                        if (!context.Applications.Contains(application))
                        {
                            context.Applications.Add(application);
                        }
                        Operation operation = count.Count.Operation;
                        if (!context.Operations.Contains(operation))
                        {
                            context.Operations.Add(operation);
                        }
                        Count dbCount = count.Count;
                        if (!context.Counts.Contains(dbCount))
                        {
                            context.Counts.Add(dbCount);
                        }

                        if (!context.DatedCounts.Contains(count))
                        {
                            context.DatedCounts.Add(count);
                        }
                    }
                    foreach (DatedDuration duration in serializedMetrics.Durations)
                    {
                        Application application = duration.Duration.Operation.Application;
                        if (!context.Applications.Contains(application))
                        {
                            context.Applications.Add(application);
                        }
                        Operation operation = duration.Duration.Operation;
                        if (!context.Operations.Contains(operation))
                        {
                            context.Operations.Add(operation);
                        }
                        Duration dbDuration = duration.Duration;
                        if (!context.Durations.Contains(dbDuration))
                        {
                            context.Durations.Add(dbDuration);
                        }

                        if (!context.DatedDurations.Contains(duration))
                        {
                            context.DatedDurations.Add(duration);
                        }
                    }
                }
            }
        }
    }
}