using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using NServiceBus.Extensibility;
using NServiceBus.Transports;

namespace NServiceBus.GitTransport
{
    public class PushMessages : IPushMessages
    {
        private Func<PushContext, Task> pipe;
        private Task poller;

        private HashSet<string> alreadyPushedCommits = new HashSet<string>();

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public void Init(Func<PushContext, Task> pipe, PushSettings settings)
        {
            this.pipe = pipe;
        }

        public void Start(PushRuntimeSettings limitations)
        {
            poller = Task.Run(async () =>
            {
                using (var repo = new Repository("../Broker", null))
                {
                    while (!tokenSource.IsCancellationRequested)
                    {
                        PullOptions options = new PullOptions
                        {
                            FetchOptions = new FetchOptions
                            {
                                CredentialsProvider = (url, usernameFromUrl, types) =>
                                    new UsernamePasswordCredentials
                                    {
                                        Username = "daniel",
                                        Password = "daniel"
                                    }
                            }
                        };
                        repo.Network.Pull(new Signature("daniel", "daniel", new DateTimeOffset(DateTime.Now)),
                            options);

                        foreach (var commit in repo.Commits)
                        {
                            if (!alreadyPushedCommits.Contains(commit.Id.Sha))
                            {
                                using (var stream = new MemoryStream())
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(commit.Message);
                                    await stream.FlushAsync().ConfigureAwait(false);
                                    stream.Position = 0;

                                    var pushContext = new PushContext(commit.Id.Sha, new Dictionary<string, string>(), stream,
                                        new NoOpTransaction(), new ContextBag(null));
                                    await pipe(pushContext);
                                    alreadyPushedCommits.Add(commit.Id.Sha);
                                }

                            }
                        }

                    }
                }
            });
        }

        private class NoOpTransaction : TransportTransaction { }

        public Task Stop()
        {
            tokenSource.Cancel();
            return poller;
        }
    }
}