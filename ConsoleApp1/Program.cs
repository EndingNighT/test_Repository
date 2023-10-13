using System;
using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;







public class Program_Class
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Status
    {
        public int GS_AGC;
        public int GS_SNR;
        public int GS_AntType;
        public int GS_Temperature;
        public int GS_FrequencyPoint;
        public int FD_AGC;
        public int FD_SNR;
        public int FD_Temperature;
        public int FD_FrequencyPoint;
        public int GPS_flushd_number;
        public float horizontal_angle;
        public float vertical_angle;
        public double power;
        public bool ground_location_status;//true 地面站位置已设置， false 地面站位置未设置
        public int trace_status;//0 未操作 1 自启动 2 伺服跟踪
        public bool init;//是否初始化
        public float Bit_rate;//速率
        public bool link_data_status; //是否收到链路数据
        public bool turntable_data_status; //是否收到转台数据
        public int FD_Channel; //机载端信道
        public int GS_Channel; //地面端信道
    };


    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "MyFunction", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern void MyFunction();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetStatus", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern Status GetStatus();

    public static void Main()
    {
        while(true)
        {
            var st1 = GetStatus();


            Type statusType = typeof(Status);
            FieldInfo[] fields = statusType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(st1);
                Console.WriteLine($"{field.Name}: {value}");
            }

            Thread.Sleep(500);
        }
        //Publisher_Request.Mainnats2();

        //Console.WriteLine("asd");


    }
}



