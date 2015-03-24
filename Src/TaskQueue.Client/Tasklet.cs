using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskQueue.Client
{
    public sealed class Tasklet
    {
        readonly IDictionary<string, string> _headers;
        readonly string _content;
        readonly string _contentType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The task payload.</param>
        /// <param name="contentType">The content type of the payload.</param>
        /// <param name="headers">The headers to add to the request.</param>
        internal Tasklet(string content, string contentType, IDictionary<string, string> headers)
        {
            _headers = headers;
            _content = content;
            _contentType = contentType;
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(object content)
        {
            return From(JObject.Parse(JsonConvert.SerializeObject(content, Formatting.None)));
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(JObject content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            return new TaskletBuilder(content.ToString(), "application/json");
        }

        /// <summary>
        /// Creates a Tasklet builder to configure the task.
        /// </summary>
        /// <param name="content">The content to create the tasklet with.</param>
        /// <param name="contentType">The content type of the payload.</param>
        /// <returns>A tasklet building to continue building.</returns>
        public static TaskletBuilder From(string content, string contentType)
        {
            return new TaskletBuilder(content, contentType);
        }

        /// <summary>
        /// Gets the headers for the request.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get { return _headers; }
        }

        /// <summary>
        /// Gets the content type.
        /// </summary>
        public string ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content
        {
            get { return _content; }
        }
    }
}
