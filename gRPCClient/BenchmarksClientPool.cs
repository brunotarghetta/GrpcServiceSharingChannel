using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPCClient
{
    [IterationCount(5)]
    [WarmupCount(4)]
    [ProcessCount(2)]
    [InvocationCount(150)]
    [RankColumn(NumeralSystem.Arabic)]
    [MemoryDiagnoser(false)]
    public class BenchmarksClientPool
    {
        HelloRequest message = new HelloRequest { Name = "Bruno" };

        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark]
        public async Task TryGetTraditionalClient()
        {
            await SimpleGRPCServiceCall();
        }


        [Benchmark]
        public async Task TryGetTraditionalClient_Twice()
        {

            await SimpleGRPCServiceCall();

            await SimpleGRPCServiceCall();
        }

        [Benchmark]
        public async Task TryGetTraditionalClient_Twice_ReuseChannel()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5011");

            await SimpleGRPCServiceCall(channel);

            await SimpleGRPCServiceCall(channel);
        }

        [Benchmark]
        public async Task TryGetTraditionalClient_Twice_ReuseChannelClient()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5011");
            var client = new Greeter.GreeterClient(channel);
            
            await SimpleGRPCServiceCall(channel, client);
            await SimpleGRPCServiceCall(channel, client);
        }


        [Benchmark]
        public async Task TryGetFactoryClient()
        {
            ClientPool<Greeter.GreeterClient> factory = new ClientPool<Greeter.GreeterClient>();
            var myclient = factory.Get();
            var mySrerveReply = await myclient.SayHelloAsync(message);
            Console.WriteLine(mySrerveReply.Message);
            factory.Return(myclient);
        }

        [Benchmark]
        public async Task TryGetFactoryClient_Twice()
        {
            ClientPool<Greeter.GreeterClient> factory = new ClientPool<Greeter.GreeterClient>();
            var myclient = factory.Get();
            var mySrerveReply = await myclient.SayHelloAsync(message);
            Console.WriteLine(mySrerveReply.Message);
            factory.Return(myclient);

            var myclient2 = factory.Get();
            var mySrerveReply2 = await myclient2.SayHelloAsync(message);
            Console.WriteLine(mySrerveReply2.Message);
            //Console.WriteLine(myclient == myclient2); //Return true
            //factory.Return(myclient2);

        }

        [Benchmark]
        public async Task TryGetFactoryClient_Twice_NoReturnToPool()
        {
            ClientPool<Greeter.GreeterClient> factory = new ClientPool<Greeter.GreeterClient>();
            var myclient = factory.Get();
            var mySrerveReply = await myclient.SayHelloAsync(message);
            Console.WriteLine(mySrerveReply.Message);
            //factory.Return(myclient);

            var myclient2 = factory.Get();
            var mySrerveReply2 = await myclient2.SayHelloAsync(message);
            Console.WriteLine(mySrerveReply2.Message);
            Console.WriteLine(myclient == myclient2); //Return false
            //factory.Return(myclient2);

        }


        private async Task SimpleGRPCServiceCall(GrpcChannel pChannel = null, Greeter.GreeterClient pClient = null)
        {
            GrpcChannel channel;
            Greeter.GreeterClient client;
            if (pClient == null)
            {
                if (pChannel == null)
                    channel = GrpcChannel.ForAddress("http://localhost:5011");
                else
                    channel = pChannel;

                client = new Greeter.GreeterClient(channel);
            }
            else
                client = pClient;

            var srerveReply = await client.SayHelloAsync(message);
            Console.WriteLine(srerveReply.Message);
        }
    }
}
