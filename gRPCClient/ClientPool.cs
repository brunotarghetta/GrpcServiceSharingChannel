using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrpcService.Greeter;

namespace gRPCClient
{
    internal class ClientPool<T> where T : ClientBase<T>
    {
        DefaultObjectPool<T>? _defaultPool;
        DefaultObjectPool<T>? defaultPool 
        {
            get {
                if (_defaultPool == null)
                {
                    _defaultPool = new DefaultObjectPool<T>(new DemoPooledObjectPolicy<T>(GrpcChannel.ForAddress("http://localhost:5011")), 1);
                }
                return _defaultPool;
            }
            set {
                _defaultPool = value;
            } 
        }

        public void Create() {

           
            GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5011");

            var defalutPolicy = new DemoPooledObjectPolicy<T>(channel);

            defaultPool = new DefaultObjectPool<T>(defalutPolicy, 1);


        }

        public T Get() {

            return defaultPool.Get();
        }

        public void Return(T c) {

            defaultPool.Return(c);
        }


    }

    public class DemoPooledObjectPolicy<T> : IPooledObjectPolicy<T> where T : ClientBase<T>
    {
        private readonly GrpcChannel channel;

        public DemoPooledObjectPolicy(GrpcChannel channel)
        {
            this.channel = channel;
        }

        public T Create()
        {
            Console.WriteLine("Creo una vez");
            //return new T (channel) ;
            return Activator.CreateInstance(typeof(T), channel) as T;
        }

        public bool Return(T obj)
        {
            return true;
        }
    }
}
