using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskQueue.Client
{
    public sealed class TaskletBuilder
    {
// ReSharper disable InconsistentNaming
        internal const string X_TQ_ContentType = "X-TQ-Content-Type";
        internal const string X_TQ_Timeout = "X-TQ-Timeout";
        internal const string X_TQ_Retries = "X-TQ-Retries";
        internal const string X_TQ_Endpoint = "X-TQ-Endpoint";
// ReSharper restore InconsistentNaming

        readonly string _content;
        internal readonly string _contentType;
        readonly Dictionary<string, string> _headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        ///// <summary>
        ///// Constructor.
        ///// </summary>
        ///// <param name="content">The content to build the Tasklet from.</param>
        //internal TaskletBuilder(object content)
        //{
        //    if (content == null)
        //    {
        //        throw new ArgumentNullException("content");
        //    }

        //    var json = JsonConvert.SerializeObject(content, Formatting.None);
            
        //    _content = JObject.Parse(json);
        //    _contentType = "application/json";
        //}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content to build the Tasklet from.</param>
        /// <param name="contentType">The content type.</param>
        internal TaskletBuilder(string content, string contentType)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (contentType == null)
            {
                throw new ArgumentNullException("contentType");
            }

            _content = content;
            _contentType = contentType;
        }

        ///// <summary>
        ///// Constructor.
        ///// </summary>
        ///// <param name="content">The content to build the Tasklet from.</param>
        //internal TaskletBuilder(JObject content)
        //{
        //    if (content == null)
        //    {
        //        throw new ArgumentNullException("content");
        //    }

        //    _content = content;
        //    _contentType = "application/json";
        //}

        /// <summary>
        /// Builds the tasklet.
        /// </summary>
        /// <returns>The tasklet that was built.</returns>
        public Tasklet Build()
        {
            return new Tasklet(_content, _contentType, _headers);
        }

        /// <summary>
        /// Sets the media type of the request.
        /// </summary>
        /// <param name="mediaType">The media type for the request.</param>
        /// <returns>The tasklet builder to continue building.</returns>
        public TaskletBuilder ContentType(string mediaType)
        {
            _headers[X_TQ_ContentType] = mediaType;

            return this;
        }

        /// <summary>
        /// Sets the endpoint for the tasklet to be called back with.
        /// </summary>
        /// <param name="endpoint">The endpoint to callback to.</param>
        /// <returns>The tasklet builder to continue building.</returns>
        public TaskletBuilder Endpoint(string endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            _headers[X_TQ_Endpoint] = endpoint;

            return this;
        }

        /// <summary>
        /// Sets the endpoint for the tasklet to be called back with.
        /// </summary>
        /// <param name="endpoint">The endpoint to callback to.</param>
        /// <returns>The tasklet builder to continue building.</returns>
        public TaskletBuilder Endpoint(Uri endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            return Endpoint(endpoint.AbsoluteUri);
        }
    }
}
