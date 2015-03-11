# TaskQueue Client
The official TaskQueue client for .Net.

## Supported Platforms
- .NET Framework 4.5
- Windows 8
- Windows Phone 8.1
- Windows Phone Silverlight 8
- Xamarin.Android
- Xamarin.iOS
- Xamarin.iOS (Classic)
- Portable Class Libraries

## Usage

### Sending strings
```csharp
// define your API key
var apiKey = new Guid("00000000-0000-0000-0000-000000000000");

using (var queue = new TaskQueueClient(new Uri("http://api.taskqueue.io"),apiKey))
{
    queue.EnqueueAsync("Hello!").Wait();
}
```

### Sending objects
You can also send objects and they will be automatically serialized.

```csharp
// define your API key
var apiKey = new Guid("00000000-0000-0000-0000-000000000000");

using (var queue = new TaskQueueClient(new Uri("http://api.taskqueue.io"),apiKey))
{
    queue.EnqueueAsync(new Message{ Text = "Hello!" }).Wait();
}

public class Message
{
    public string Text { get; set; }
}
```