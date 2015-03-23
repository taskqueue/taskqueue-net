namespace TaskQueue.Client
{
    internal static class HttpHeaders
    {
        internal const string Timeout = "X-TQ-Timeout";
        internal const string Retries = "X-TQ-Retries";
        internal const string Endpoint = "X-TQ-Endpoint";
    }
}
