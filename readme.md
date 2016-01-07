# TaskQueue Client
The official [TaskQueue](http://taskqueue.io/) client for .Net.

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

`Tasklets` are a single messages that will be routed by TaskQueue.

### String tasklets
You can create a tasklet from a string.

```csharp
var tasklets = Tasklet.From("Hello!")
                .CallbackEndpoint("http://requestb.in/1i8i5zu1")
                .Build();
```

### Object tasklets
You can also create a tasklet from an object and it will be automatically serialized to JSON.

```csharp
var tasklets = Tasklet.From(new Message { Text = "Hello!" })
                .CallbackEndpoint("http://requestb.in/1i8i5zu1")
                .Build();

public class Message
{
    public string Text { get; set; }
}
```

### Publishing a Task
Once your tasklet is ready to send, simply add it to your task queue.

```csharp
var apiKey = "00000000000000000000000000000000";

using (var queue = new TaskQueueClient(new Uri("http://api.taskqueue.io"), apiKey))
{
    queue.EnqueueAsync(tasklet).Wait();
}
```