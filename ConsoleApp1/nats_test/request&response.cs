using System;
using System.Drawing;
using System.Reflection;
using System.Text;
using NATS.Client;



//客户端  发送请求
public class Publisher_Request
{


    const string suball = ">";

    //方式一
    public static void Mainnats()
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



    }

    //方式二 使用配置
    public static void Mainnats2()
    {
        Options opts = ConnectionFactory.GetDefaultOptions();
        string[] servers = new string[] {
                "192.168.20.18:4222",
                //"127.0.0.1:4222",
            };//url
        opts.User = "usr";
        opts.Password = "pwd";
        opts.Servers = servers;
        opts.ReconnectWait = 10000;
        opts.MaxReconnect = int.MaxValue;
        Console.ForegroundColor = ConsoleColor.Yellow;
        opts.DisconnectedEventHandler += (s, e) =>
        {

            Console.WriteLine("和Nats服务端 断开", ConsoleColor.Red);
        };
        opts.ReconnectedEventHandler += (s, e) =>
        {
            Console.WriteLine("已重连到NATS服务", ConsoleColor.Green);
        };


        IConnection? nats_con = null;

        try
        {
            nats_con = new ConnectionFactory().CreateConnection(opts);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        while (true)
        {
            if (nats_con ==null)
            {
                Console.WriteLine("null");
                continue;
            }
            if (nats_con.State != ConnState.CONNECTED)
                Console.WriteLine($"connection state  {nats_con.State}");

            // 订阅主题
            //IAsyncSubscription subscription = nats_con.SubscribeAsync(suball, (sender, args) => {
            //    // 处理接收到的消息
            //    string message = System.Text.Encoding.UTF8.GetString(args.Message.Data);
            //    System.Console.WriteLine("接收到消息: " + message);
            //});


            
            //Thread.Sleep(1000);


        }




    }


}


//服务端  响应请求
public class Subscriber_Respond
{
    public static void Main2332()
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