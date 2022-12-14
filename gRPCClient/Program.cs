// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Grpc.Net.Client;
using gRPCClient;
using GrpcService;
using GrpcService.Protos;
var message = new HelloRequest { Name = "Bruno" };

//await ExecuteTraditionalgRPCClient(message);

//await ExecuteClientPooling(message);

//FundBeanStream f;
//f = new FundBeanStream();
//f = new FundBeanStream();

BenchmarkRunner.Run<BenchmarksClientPool>();

Console.ReadLine();

static async Task ExecuteClientPooling(HelloRequest message)
{
    ClientPool<Greeter.GreeterClient> pool = new ClientPool<Greeter.GreeterClient>();
    //factory.Create();
    var myclient = pool.Get();
    var mySrerveReply = await myclient.SayHelloAsync(message);
    Console.WriteLine(mySrerveReply.Message);
    pool.Return(myclient);

    var myclient2 = pool.Get();
    var mySrerveReply2 = await myclient2.SayHelloAsync(message);
    Console.WriteLine(mySrerveReply2.Message);
    Console.WriteLine(myclient == myclient2); //Return true
                                              //factory.Return(myclient2);


    var myclient3 = pool.Get();
    var mySrerveReply3 = await myclient3.SayHelloAsync(message);
    Console.WriteLine(mySrerveReply3.Message);
    pool.Return(myclient3);
    Console.WriteLine(myclient == myclient3);//Return false
}

static async Task ExecuteTraditionalgRPCClient(HelloRequest message)
{
    var channel = GrpcChannel.ForAddress("http://localhost:5011");
    var client = new Greeter.GreeterClient(channel);
    var srerveReply = await client.SayHelloAsync(message);
    Console.WriteLine(srerveReply.Message);
    Console.ReadLine();
}