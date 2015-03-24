using System;
using TaskQueue.Client;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasklet1 = Tasklet
                .From(new SayHelloTask {Name = "Cain 1"})
                .Endpoint("http://requestb.in/1chgvec1")
                .Build();

            var tasklet2 = Tasklet
                .From(new SayHelloTask { Name = "Cain 2" })
                .Endpoint("http://requestb.in/1chgvec1")
                .Build();

            using (var client = new TaskQueueClient("http://api.taskqueue.io", Guid.NewGuid()))
            {
                client.EnqueueAsync(tasklet1, tasklet2).Wait();
            }
        }

        public class SayHelloTask
        {
            public string Name { get; set; }
        }
    }
}
