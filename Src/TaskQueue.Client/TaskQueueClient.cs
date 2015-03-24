using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue.Client
{
    public class TaskQueueClient : IDisposable
    {
        readonly Uri _endpoint;
        readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="apiKey">The API key.</param>
        public TaskQueueClient(string endpoint, Guid apiKey) : this(new Uri(endpoint), apiKey) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="apiKey">The API key.</param>
        public TaskQueueClient(Uri endpoint, Guid apiKey)
        {
            _endpoint = endpoint;

            _httpClient = new HttpClient { BaseAddress = endpoint };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey.ToString("N").ToUpper());
        }

        /// <summary>
        /// Enqueue a given tasklet.
        /// </summary>
        /// <param name="tasklet">The tasklet to enqueue.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task EnqueueAsync(Tasklet tasklet, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(tasklet.Content, Encoding.UTF8, tasklet.ContentType),
                RequestUri = new Uri(_endpoint, "tasks")
            };

            AddHeaders(message.Headers, tasklet.Headers);

            var response = await _httpClient.SendAsync(message, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Enqueue a given list of tasks.
        /// </summary>
        /// <param name="tasklets">The tasks to enqueue.</param>
        public Task EnqueueAsync(params Tasklet[] tasklets)
        {
            return EnqueueAsync(tasklets, CancellationToken.None);
        }

        /// <summary>
        /// Enqueue a given list of tasks.
        /// </summary>
        /// <param name="tasklets">The tasks to enqueue.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task EnqueueAsync(IReadOnlyCollection<Tasklet> tasklets, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new MultipartContent();

            foreach (var tasklet in tasklets)
            {
                content.Add(CreateTaskletContent(tasklet));
            }

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri(_endpoint, "tasks")
            };

            var response = await _httpClient.SendAsync(message, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Creates the HTTP content for a tasklet.
        /// </summary>
        /// <param name="tasklet">The tasklet to create the HTTP content for.</param>
        /// <returns>The HTTP content for the given tasklet.</returns>
        static HttpContent CreateTaskletContent(Tasklet tasklet)
        {
            var content = new StringContent(tasklet.Content, Encoding.UTF8, tasklet.ContentType);

            AddHeaders(content.Headers, tasklet.Headers);

            return content;
        }

        /// <summary>
        /// Add the list of headers to the request headers collection.
        /// </summary>
        /// <param name="httpContentHeaders">The request headers collection to add it.</param>
        /// <param name="headers">The list of headers to add.</param>
        static void AddHeaders(HttpHeaders httpContentHeaders, IEnumerable<KeyValuePair<string, string>> headers)
        {
            foreach (var header in headers)
            {
                httpContentHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
