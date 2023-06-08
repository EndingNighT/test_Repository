using System;
using System.Reflection;
using System.Text;
using NATS.Client;



//客户端
class Publisher
{
    static void Mainnats()
    {
        // 连接到 NATS 服务器
        using (var connection = new ConnectionFactory().CreateConnection())
        {
            // 创建一个发布者，用于发送请求并接收响应
            var request = Encoding.UTF8.GetBytes("Hello NATS!");
            var response = connection.Request("mySubject", request, timeout: 1000);//TimeSpan.FromSeconds(5)

            // 打印接收到的响应消息
            Console.WriteLine("Received response: " + Encoding.UTF8.GetString(response.Data));
        }
    }



}
