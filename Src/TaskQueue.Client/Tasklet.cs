using Newtonsoft.Json.Linq;

namespace TaskQueue.Client
{
    public sealed class Tasklet
    {
        readonly string _endpoint;
        readonly JObject _content;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint">The endpoint that the task queue should call with the payload.</param>
        /// <param name="content">The task payload.</param>
        public Tasklet(string endpoint, JObject content)
        {
            _endpoint = endpoint;
            _content = content;
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(object content)
        {
            return new TaskletBuilder(content);
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(JObject content)
        {
            return new TaskletBuilder(content);
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(string content)
        {
            return new TaskletBuilder(content);
        }

        /// <summary>
        /// Gets the endpoint.
        /// </summary>
        public string Endpoint
        {
            get { return _endpoint; }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public JObject Content
        {
            get { return _content; }
        }
    }
}
