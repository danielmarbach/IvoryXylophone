using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly EndpointName endpointName;
        private Func<PushContext, Task> pipe;
        private Task poller;

        private HashSet<string> alreadyPushedCommits = new HashSet<string>();

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public PushMessages(EndpointName endpointName)
        {
            this.endpointName = endpointName;
        }

        public void Init(Func<PushContext, Task> pipe, PushSettings settings)
        {
            this.pipe = pipe;
        }

        public void Start(PushRuntimeSettings limitations)
        {
            poller = Task.Run(async () =>
            {
                using (var repo = new Repository($"../{endpointName}", null))
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
                                    var headers = commit.Message
                                        .Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(
                                            kvp => kvp.Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries))
                                        .Select(kvp => new { Key = kvp[0], Value = kvp[1] })
                                        .ToDictionary(x => x.Key, x => x.Value);

                                    var treeEntry = commit.Tree.FirstOrDefault(x => x.Name == "message.payload");
                                    if (treeEntry != null)
                                    {
                                        await writer.WriteAsync(File.ReadAllText(treeEntry.Path)).ConfigureAwait(false);
                                    }

                                    await stream.FlushAsync().ConfigureAwait(false);
                                    stream.Position = 0;

                                    var pushContext = new PushContext(commit.Id.Sha, headers, stream,
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