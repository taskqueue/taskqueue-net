using System;
using System.Collections.Generic;

namespace TaskQueue.Client
{
    public sealed class TaskletBuilder
    {
// ReSharper disable InconsistentNaming
        internal const string XtqContentType = "X-TQ-Content-Type";
        internal const string XtqTimeout = "X-TQ-Timeout";
        internal const string XtqRetries = "X-TQ-Retries";
        internal const string XtqEndpoint = "X-TQ-Endpoint";
        internal const string XtqAuthorization = "X-TQ-Authorization";
// ReSharper restore InconsistentNaming

        readonly string _content;
        readonly string _contentType;
        readonly Dictionary<string, string> _headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

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
            _headers[XtqContentType] = mediaType;

            return this;
        }

        /// <summary>
        /// Sets the authorization to supply to the callback.
        /// </summary>
        /// <param name="scheme">The authorization scheme.</param>
        /// <param name="parameter">The authorization parameter.</param>
        /// <returns>The tasklet builder to continue building.</returns>
        public TaskletBuilder Authorization(string scheme, string parameter)
        {
            _headers[XtqAuthorization] = String.Format("{0} {1}", scheme, parameter);

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

            _headers[XtqEndpoint] = endpoint;

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
