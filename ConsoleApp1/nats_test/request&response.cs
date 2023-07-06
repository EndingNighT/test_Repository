using System;
using System.Reflection;
using System.Text;
using NATS.Client;



//客户端  发送请求
class Publisher_Request
{
    static void Mainnats()
    {
        // 连接到 NATS 服务器
        using (var connection = new ConnectionFactory().CreateConnection())
        {
            // 创建一个发布者，用于发送请求并接收响应
            var request = Encoding.UTF8.GetBytes("Hello NATS!");//字符串转byte[]，对应Encoding.UTF8.GetString(request)
            var response = connection.Request("mySubject", request, timeout: 1000);//TimeSpan.FromSeconds(5)
            //var response = connection.RequestAsync("mySubject", request, 1000);


            // 打印接收到的响应消息
            Console.WriteLine("Received response: " + Encoding.UTF8.GetString(response.Data));
        }

        Options opts = ConnectionFactory.GetDefaultOptions();
        string[] servers = new string[] {
                "192.168.20.18:4222",
            };
        opts.User = "usr";
        opts.Password = "pwd";
        opts.Servers = servers;
        opts.ReconnectWait = 10000;
        opts.MaxReconnect = int.MaxValue;

        var nats_con = new ConnectionFactory().CreateConnection(opts);

        if (nats_con != null && nats_con.State == ConnState.CONNECTED)
        {
            Console.WriteLine("connected");
        }

    }



}


//服务端  响应请求
class Subscriber_Respond
{
    static void Main2332()
    {
        // 连接到 NATS 服务器
        using (var connection = new ConnectionFactory().CreateConnection())
        {
            // 创建一个订阅者，用于接收请求并返回响应
            connection.SubscribeAsync("mySubject", (sender, args) =>
            {
                // 接收到请求消息
                var request = args.Message.Data;
                Console.WriteLine("Received request: " + Encoding.UTF8.GetString(request));

                // 发送响应消息
                var response = Encoding.UTF8.GetBytes("Hello Reply!");
                //connection.Publish(args.Message.Reply, response);
                args.Message.Respond(response);
            });

            // 等待退出信号
            Console.WriteLine("Subscriber started. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}