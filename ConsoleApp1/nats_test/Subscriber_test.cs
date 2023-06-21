using NATS.Client;


//AircraftManager
//OperFlightControl
//E6A-Sim3881996C
//E6A-SimEC2EFE5A
class Subscriber
{
    const string sub = "your_topic";
    const string sub2 = "Aircrafts.E6A-Sim3881996C.AircraftManager.Proto_GnssInfo";
    const string suball = ">";
    static void Mai2312n()
    {
        // 连接到 NATS 服务器
        using (IConnection connection = new ConnectionFactory().CreateConnection())
        {
            // 订阅主题
            IAsyncSubscription subscription = connection.SubscribeAsync(suball, (sender, args) => {
                // 处理接收到的消息
                string message = System.Text.Encoding.UTF8.GetString(args.Message.Data);
                System.Console.WriteLine("接收到消息: " + message);
            });

            // 等待接收消息
            System.Console.WriteLine("等待接收消息...");
            System.Console.ReadLine();

            // 取消订阅
            subscription.Unsubscribe();
        }
    }
}









