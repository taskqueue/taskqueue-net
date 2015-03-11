using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue.Clients
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
        public TaskQueueClient(Uri endpoint, Guid apiKey)
        {
            _endpoint = endpoint;

            _httpClient = new HttpClient { BaseAddress = endpoint };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey.ToString("N").ToUpper());
        }

        /// <summary>
        /// Enqueue a given task.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        /// <returns>The ID that was generated for the given task.</returns>
        public Task EnqueueAsync(Tasklet task)
        {
            return EnqueueAsync(task, CancellationToken.None);
        }

        /// <summary>
        /// Enqueue a given task.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID that was generated for the given task.</returns>
        public async Task EnqueueAsync(Tasklet task, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(task.Content.ToString(), Encoding.UTF8, "application/json"),
                RequestUri = new System.Uri(_endpoint, "tasks")
            };

            if (String.IsNullOrWhiteSpace(task.Endpoint) == false)
            {
                message.Headers.Add(HttpHeaders.Endpoint, task.Endpoint);
            }

            var response = await _httpClient.SendAsync(message, cancellationToken);
            response.EnsureSuccessStatusCode();
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
