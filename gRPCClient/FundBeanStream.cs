using GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPCClient
{
    public class FundBeanStream
    {
        HelloRequest message = new HelloRequest { Name = "Bruno" };

        public FundBeanStream()
        {
            ClientPool<Greeter.GreeterClient> pool = new ClientPool<Greeter.GreeterClient>();
            var myclient = pool.Get();
            var mySrerveReply = myclient.SayHello(message);
            Console.WriteLine(mySrerveReply.Message);
            pool.Return(myclient);
        }
    }
}
