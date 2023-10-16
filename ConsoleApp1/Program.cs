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
    //[StructLayout(LayoutKind.Sequential, Pack = 4)]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
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
        [MarshalAs(UnmanagedType.U1)]
        public bool ground_location_status;//true 地面站位置已设置， false 地面站位置未设置
        public int trace_status;//0 未操作 1 自启动 2 伺服跟踪
        [MarshalAs(UnmanagedType.U1)]
        public bool init;//是否初始化
        public float Bit_rate;//速率
        [MarshalAs(UnmanagedType.U1)]
        public bool link_data_status; //是否收到链路数据
        [MarshalAs(UnmanagedType.U1)]
        public bool turntable_data_status; //是否收到转台数据
        public int FD_Channel; //机载端信道
        public int GS_Channel; //地面端信道
        public int test;
    };

    //bool变量不添加前缀修饰，是Boolean类型，为4个字节 
    //[StructLayout(LayoutKind.Sequential, Pack = 8)] // 默认为8
    public struct MyStruct
    {
        public int a;
        [MarshalAs(UnmanagedType.U1)]
        public bool b;
        [MarshalAs(UnmanagedType.U1)]
        public bool b1;
        public double c;

    };

    public struct Channel
    {
        public int FD_Channel;
        public int GS_Channel;
        public bool turntable_data_status;
        public bool link_data_status;
    };


    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "MyFunction", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern void MyFunction();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetStatus", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern Status GetStatus();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetStruct", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern MyStruct GetStruct();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "printExam", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern ExampleStruct printExam();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetChannel", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern Channel GetChannel();

    static void testStruct()
    {
        var ms = GetStruct();

        Type statusType = typeof(MyStruct);
        FieldInfo[] fields = statusType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(ms);
            Console.WriteLine($"{field.Name}: {value}");
        }

        // 获取每个成员的偏移量
        Console.WriteLine("Offset of a: " + Marshal.OffsetOf(typeof(MyStruct), "a").ToInt32() + " bytes");
        Console.WriteLine("Offset of b: " + Marshal.OffsetOf(typeof(MyStruct), "b").ToInt32() + " bytes");
        Console.WriteLine("Offset of b1: " + Marshal.OffsetOf(typeof(MyStruct), "b1").ToInt32() + " bytes");
        Console.WriteLine("Offset of c: " + Marshal.OffsetOf(typeof(MyStruct), "c").ToInt32() + " bytes");

        unsafe
        {
            MyStruct* pointer = &ms;
            Console.WriteLine($"变量 myVariable 的值：{ms} 内存大小 ：{sizeof(MyStruct)} {Marshal.SizeOf(typeof(MyStruct))} {Marshal.SizeOf(ms)}");
            Console.WriteLine($"变量 myVariable 的内存表示：");
            byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针
            for (int i = 0; i < Marshal.SizeOf(ms); i++)
            {
                Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
            }
            Console.WriteLine("");
        }


    }

    static unsafe void testStatus()
    {
        var status = GetStatus();

        reflection1(status);

        offset1(status);


        Status* pointer = &status;
        Console.WriteLine($"C# 变量 myVariable 的值：{status} 内存大小 ：{sizeof(Status)} {Marshal.SizeOf(typeof(Status))} {Marshal.SizeOf(status)}");
        Console.WriteLine("变量 myVariable 的内存表示：");
        byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针
        for (int i = 0; i < Marshal.SizeOf(status); i++)
        {
            Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
        }
        Console.WriteLine("");

        for (int i = 0; i < Marshal.SizeOf(status); i++)
        {
            Console.Write($"{i:D2} "); // 输出每个字节的十六进制表示
        }
        Console.WriteLine("");
    }

    //[StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct ExampleStruct
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool b1;
        [MarshalAs(UnmanagedType.U1)]
        public bool b2;
        public int i3;
        public int i4;
        public double d5;
    }

    unsafe struct ExampleStruct2
    {
        public byte b1;
        public byte b2;
        public int i3;
        public int i4;
        public double d5;
    }
    unsafe void testExample()
    {

        ExampleStruct2 ex = new ExampleStruct2()
        {
            b1 = 1,
            b2 = 2,
            i3 = 3,
            i4 = 5,
            d5 = 6
        };


        byte* addr = (byte*)&ex;
        Console.WriteLine("Size:      {0}", sizeof(ExampleStruct2));
        Console.WriteLine("b1 Offset: {0}", &ex.b1 - addr);
        Console.WriteLine("b2 Offset: {0}", &ex.b2 - addr);
        Console.WriteLine("i3 Offset: {0}", (byte*)&ex.i3 - addr);
        Console.WriteLine("a4 Offset: {0}", (byte*)&ex.i4 - addr);
        Console.WriteLine("d5 Offset: {0}", (byte*)&ex.d5 - addr);



        ExampleStruct2* pointer = &ex;
        Console.WriteLine($"C# 变量 myVariable 的值：{ex} 内存大小 ：{sizeof(ExampleStruct2)} {Marshal.SizeOf(typeof(ExampleStruct2))} {Marshal.SizeOf(ex)}");
        Console.WriteLine("变量 myVariable 的内存表示：");
        byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针
        for (int i = 0; i < Marshal.SizeOf(ex); i++)
        {
            Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
        }
        Console.WriteLine("");


    }


    public unsafe static void Main()
    {
        //testStatus();

        var ch = GetChannel();

        Console.WriteLine($"{ch.FD_Channel}");
        Console.WriteLine($"{ch.GS_Channel}");
        Console.WriteLine($"{ch.link_data_status}");
        Console.WriteLine($"{ch.turntable_data_status}");


        Channel* pointer = &ch;
        Console.WriteLine($"变量 myVariable 的值：{ch} 内存大小为{sizeof(Channel)} {Marshal.SizeOf(typeof(Channel))} {Marshal.SizeOf(ch)}");
        Console.WriteLine("变量 myVariable 的内存表示：");
        byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针
        for (int i = 0; i < Marshal.SizeOf(ch); i++)
        {
            Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
        }
        Console.WriteLine("");


    }

    static unsafe void testExam()
    {
        var es = printExam();

        ExampleStruct* pointer = &es;
        Console.WriteLine($"C# 变量 myVariable 的值：{es} 内存大小 ：{sizeof(ExampleStruct)} {Marshal.SizeOf(typeof(ExampleStruct))} {Marshal.SizeOf(es)}");
        Console.WriteLine("变量 myVariable 的内存表示：");
        byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针
        for (int i = 0; i < Marshal.SizeOf(es); i++)
        {
            Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
        }
        Console.WriteLine("");

        Console.WriteLine($"{es.b1}");
        Console.WriteLine($"{es.b2}");
        Console.WriteLine($"{es.i3}");
        Console.WriteLine($"{es.i4}");
        Console.WriteLine($"{es.d5}");
    }

    static void reflection1(Status status)
    {
        Type statusType = typeof(Status);
        FieldInfo[] fields = statusType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(status);
            Console.WriteLine($"{field.Name}: {value}");
        }
    }

    static unsafe void offset1(Status status)
    {
        Status * statusPtr = &status;
        {
            Console.WriteLine("Size: {0}", sizeof(Status));
            Console.WriteLine("GS_AGC Offset: {0}", (byte*)&status.GS_AGC - (byte*)statusPtr);
            Console.WriteLine("GS_SNR Offset: {0}", (byte*)&status.GS_SNR - (byte*)statusPtr);
            Console.WriteLine("GS_AntType Offset: {0}", (byte*)&status.GS_AntType - (byte*)statusPtr);
            Console.WriteLine("GS_Temperature Offset: {0}", (byte*)&status.GS_Temperature - (byte*)statusPtr);
            Console.WriteLine("GS_FrequencyPoint Offset: {0}", (byte*)&status.GS_FrequencyPoint - (byte*)statusPtr);
            Console.WriteLine("FD_AGC Offset: {0}", (byte*)&status.FD_AGC - (byte*)statusPtr);
            Console.WriteLine("FD_SNR Offset: {0}", (byte*)&status.FD_SNR - (byte*)statusPtr);
            Console.WriteLine("FD_Temperature Offset: {0}", (byte*)&status.FD_Temperature - (byte*)statusPtr);
            Console.WriteLine("FD_FrequencyPoint Offset: {0}", (byte*)&status.FD_FrequencyPoint - (byte*)statusPtr);
            Console.WriteLine("GPS_flushd_number Offset: {0}", (byte*)&status.GPS_flushd_number - (byte*)statusPtr);
            Console.WriteLine("horizontal_angle Offset: {0}", (byte*)&status.horizontal_angle - (byte*)statusPtr);
            Console.WriteLine("vertical_angle Offset: {0}", (byte*)&status.vertical_angle - (byte*)statusPtr);
            Console.WriteLine("power Offset: {0}", (byte*)&status.power - (byte*)statusPtr);
            Console.WriteLine("ground_location_status Offset: {0}", (byte*)&status.ground_location_status - (byte*)statusPtr);
            Console.WriteLine("trace_status Offset: {0}", (byte*)&status.trace_status - (byte*)statusPtr);
            Console.WriteLine("init Offset: {0}", (byte*)&status.init - (byte*)statusPtr);
            Console.WriteLine("Bit_rate Offset: {0}", (byte*)&status.Bit_rate - (byte*)statusPtr);
            Console.WriteLine("link_data_status Offset: {0}", (byte*)&status.link_data_status - (byte*)statusPtr);
            Console.WriteLine("turntable_data_status Offset: {0}", (byte*)&status.turntable_data_status - (byte*)statusPtr);
            Console.WriteLine("FD_Channel Offset: {0}", (byte*)&status.FD_Channel - (byte*)statusPtr);
            Console.WriteLine("GS_Channel Offset: {0}", (byte*)&status.GS_Channel - (byte*)statusPtr);
            Console.WriteLine("test Offset: {0}", (byte*)&status.test - (byte*)statusPtr);
            Console.WriteLine("SizeOf GS_Channel: {0}", Marshal.SizeOf(status.ground_location_status));
            Console.WriteLine("SizeOf GS_Channel: {0}", Marshal.SizeOf(status.FD_Channel));
            Console.WriteLine("SizeOf GS_Channel: {0}", Marshal.SizeOf(status.GS_Channel));

        }
    }

}



