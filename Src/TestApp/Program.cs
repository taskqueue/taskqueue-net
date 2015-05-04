using System;
using System.IdentityModel.Tokens;
using System.Threading;
using TaskQueue.Client;
using TaskQueue.Client.Jwt;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = new JwtTokenBuilder()
                .Issuer("localhost")
                .Secret("UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfc")
                .ExpiresIn(TimeSpan.FromMinutes(30))
                .Build();

            var callbackEndpoint = "http://localhost:9100/callback";

            var tasklet1 = Tasklet
                .From(new SayHelloTask { Name = "Test 201" })
                .Endpoint(callbackEndpoint)
                .AuthorizeWithJwt(token)
                .Build();

            var tasklet2 = Tasklet
                .From(new SayHelloTask { Name = "Test 202" })
                .Endpoint(callbackEndpoint)
                .AuthorizeWithJwt(token)
                .Build();

            var tasklet3 = Tasklet
                .From(new SayHelloTask { Name = "Test 203" })
                .Endpoint(callbackEndpoint)
                .AuthorizeWithJwt(token)
                .Build();

            ////var endpoint = "http://api.taskqueue.io";
            //var endpoint = "http://localhost:9001";
            //using (var client = new TaskQueueClient(endpoint, Guid.NewGuid()))
            //{
            //    //while (true)
            //    //{
            //        client.EnqueueAsync(tasklet1, tasklet2, tasklet3).Wait();
            //        //Thread.Sleep(50);
            //    //}
            //}
        }

        public class SayHelloTask
        {
            public string Name { get; set; }
        }
    }
}
