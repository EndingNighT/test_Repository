using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScrollerViewer_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Output(string str)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                textBox1.AppendText(str);
                //textBox1.ScrollToEnd();
                scrollViewer1.ScrollToEnd();
                //textBox1.CaretIndex = int.MaxValue;
            }));
        }
        public void OutputLine(string str) => Output(str + Environment.NewLine);

        private void buttonAction5_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void buttonAction5_Click2(object sender, RoutedEventArgs e)
        //{

        //    Task.Run(async() =>
        //    {
        //        while (true)
        //        {
        //            var ch = "hello";
        //            OutputLine(DateTime.Now.ToString() + "\t" + ch);
        //            //Thread.Sleep(100);
        //            await Task.Delay(100);
        //        }

        //    });



        //}

        //private async void buttonAction5_Click(object sender, RoutedEventArgs e)
        //{
        //    //test for nats

        //    NatsApi natsApi = new("192.168.20.18:4222");//FCNetConstant.NatsServerUrl
        //    natsApi.Init();
        //    while (!natsApi.IsOk())
        //    {
        //        OutputLine("try to connect");
        //        await Task.Delay(100);
        //    }
        //    OutputLine("connected");
        //    //var connection = new ConnectionFactory().CreateConnection();



        //    //natsApi.AddListenHandler("aaaa", (bs) =>
        //    //{
        //    //    natsApi.PublishData("aa",bs);
        //    //});

        //    var AircraftName = textBoxAircraftName.Text;

        //    Proto_Request request = new Proto_Request();
        //    request.RequestId = 121;
        //    //request.AcName = "E6H-SimE2245CA";
        //    request.AcName = AircraftName;
        //    request.FunName = Proto_CommandType.GimbalCmds;//GimbalCmds Engine


        //    var req = new Proto_GimbalStackCmdsRequest();
        //    req.Type = Proto_GimbalCamFIFOCmdsType.Defog;
        //    Proto_CameraDefogSettings defogSettings = Proto_CameraDefogSettings.Off;
        //    req.CarryingData = ByteString.CopyFrom(Widgets.Object2Bytes(defogSettings));
        //    request.Data = req.ToByteString();



        //    var bs = await natsApi.RequestDataAsync("Aircrafts.Command", request.ToByteArray(), 1000);
        //    //var bs = natsApi.RequestData("Aircrafts.Command", request.ToByteArray(), 1000);
        //    Proto_Respones recivedResponse = Proto_Respones.Parser.ParseFrom(bs);
        //    OutputLine(recivedResponse.IsSuccess.ToString());
        //    OutputLine(recivedResponse.Message.ToString());





        //    //Dictionary<string, object> variables = GetVariableValues(example);

        //    //static Dictionary<string, object> GetVariableValues(object obj)
        //    //{
        //    //    Dictionary<string, object> dic = new Dictionary<string, object>();

        //    //    System.Type type = obj.GetType();
        //    //    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    //    foreach (PropertyInfo property in properties)
        //    //    {
        //    //        string propertyName = property.Name;
        //    //        object propertyValue = property.GetValue(obj);

        //    //        dic.Add(propertyName, propertyValue);
        //    //    }

        //    //    return dic;
        //    //}

        //    try
        //    {
        //        //natsApi.PublishData(topic, data);
        //        var topic = "Aircrafts." + AircraftName + ".OperFlightControl.Proto_GenerateInfo";
        //        byte[] info = new byte[] { };

        //        natsApi.SubscribeData(topic, (bs) =>
        //        {

        //            //info = new byte[bs.Length];
        //            //bs.CopyTo(info, 0);
        //            var infolist = Proto_GenerateInfo.Parser.ParseFrom(bs);



        //            if (infolist.HasConnectedDataLinkName)
        //            {
        //                var ch = infolist.ConnectedDataLinkName;
        //                OutputLine(ch);
        //            }

        //            if (infolist.HasConnectedDataLinkChannel)
        //            {
        //                var ch1 = infolist.ConnectedDataLinkChannel;
        //                OutputLine(ch1.ToString());
        //            }



        //            //if (infolist.)


        //            Thread.Sleep(1000);
        //        });

        //        //var infolist2 = Proto_GenerateInfo.Parser.ParseFrom(info);



        //    }
        //    catch
        //    {

        //        return;
        //    }
        //}



    }
}
