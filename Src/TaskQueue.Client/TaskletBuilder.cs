using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskQueue.Clients
{
    public sealed class TaskletBuilder
    {
        readonly JObject _content;
        string _callbackEndpoint;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content to build the Tasklet from.</param>
        internal TaskletBuilder(object content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.None);
            
            _content = JObject.Parse(json);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content to build the Tasklet from.</param>
        internal TaskletBuilder(string content)
        {
            _content = JObject.Parse(content);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content to build the Tasklet from.</param>
        internal TaskletBuilder(JObject content)
        {
            _content = content;
        }

        /// <summary>
        /// Builds the tasklet.
        /// </summary>
        /// <returns>The tasklet that was built.</returns>
        public Tasklet Build()
        {
            return new Tasklet(_callbackEndpoint, _content);
        }

        /// <summary>
        /// Sets the endpoint for the tasklet to be called back with.
        /// </summary>
        /// <param name="callbackEndpoint">The endpoint callback </param>
        /// <returns>The tasklet builder to continue building.</returns>
        public TaskletBuilder CallbackEndpoint(string callbackEndpoint)
        {
            _callbackEndpoint = callbackEndpoint;

            return this;
        }
    }
}
