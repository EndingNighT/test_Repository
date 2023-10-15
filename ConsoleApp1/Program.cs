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
        //[MarshalAs(UnmanagedType.I4)]
        public bool ground_location_status;//true 地面站位置已设置， false 地面站位置未设置
        public int trace_status;//0 未操作 1 自启动 2 伺服跟踪
        public bool init;//是否初始化
        public float Bit_rate;//速率
        public bool link_data_status; //是否收到链路数据
        public bool turntable_data_status; //是否收到转台数据
        public int FD_Channel; //机载端信道
        public int GS_Channel; //地面端信道
        public int test;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MyStruct
    {
        public int a;
        [MarshalAs(UnmanagedType.U1)]
        public bool b;
        [MarshalAs(UnmanagedType.U1)]
        public bool b1;
        public double c;

    };


    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "MyFunction", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern void MyFunction();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetStatus", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern Status GetStatus();

    [DllImport(@"C:\Users\chenxy\Desktop\win_test\Project1\x64\Debug\Project1.dll", EntryPoint = "GetStruct", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern MyStruct GetStruct();


    void testStruct()
    {
        var ms = GetStruct();

        Type statusType = typeof(MyStruct);
        FieldInfo[] fields = statusType.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(ms);
            Console.WriteLine($"{field.Name}: {value}");
        }

        int size = Marshal.SizeOf(typeof(MyStruct));
        Console.WriteLine($"C# Size of MyStruct: " + size + " bytes ");
        int size12 = Marshal.SizeOf(ms); // 获取结构体的大小
        Console.WriteLine("Size of ms: " + size12 + " bytes");

        // 获取每个成员的偏移量
        Console.WriteLine("Offset of a: " + Marshal.OffsetOf(typeof(MyStruct), "a").ToInt32() + " bytes");
        Console.WriteLine("Offset of b: " + Marshal.OffsetOf(typeof(MyStruct), "b").ToInt32() + " bytes");
        Console.WriteLine("Offset of b1: " + Marshal.OffsetOf(typeof(MyStruct), "b1").ToInt32() + " bytes");
        Console.WriteLine("Offset of c: " + Marshal.OffsetOf(typeof(MyStruct), "c").ToInt32() + " bytes");



        unsafe
        {
            MyStruct* pointer = &ms;


            //Console.WriteLine($"变量 myVariable 的内存地址：{(IntPtr)pointer}");
            Console.WriteLine($"变量 myVariable 的值：{ms} 内存大小 ：{sizeof(MyStruct)} {Marshal.SizeOf(typeof(MyStruct))} {Marshal.SizeOf(ms)}");
            Console.WriteLine($"变量 myVariable 的内存表示：{(int)sizeof(MyStruct)}");

            byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针

            for (int i = 0; i < sizeof(MyStruct); i++)
            {
                Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
            }
            Console.WriteLine("");
        }


    }

    void testStatus()
    {
        while (true)
        {
            

            Thread.Sleep(500);
        }
        //Publisher_Request.Mainnats2();

        //Console.WriteLine("asd");


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

    //[StructLayout(LayoutKind.Sequential, Pack = 4)]


    public unsafe static void Main()
    {

        var status = GetStatus();


        Type statusType = typeof(Status);
        FieldInfo[] fields = statusType.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(status);
            Console.WriteLine($"{field.Name}: {value}");
        }

        //int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Status));
        //Console.WriteLine("C#  Size of Status: " + size + " bytes ");
        //int size12 = Marshal.SizeOf(status); // 获取结构体的大小
        //Console.WriteLine("Size of status: " + size12 + " bytes");

        unsafe
        {
            Status* pointer = &status;

            //Console.WriteLine($"变量 myVariable 的内存地址：{(IntPtr)pointer}");
            //Console.WriteLine($"变量 myVariable 的值：{status} 内存大小 ：{sizeof(Status)} {Marshal.SizeOf(status)}");
            Console.WriteLine($"C# 变量 myVariable 的值：{status} 内存大小 ：{sizeof(Status)} {Marshal.SizeOf(typeof(Status))} {Marshal.SizeOf(status)}");
            Console.WriteLine("变量 myVariable 的内存表示：");

            byte* bytePointer4 = (byte*)pointer; // 将整数指针转换为字节指针

            for (int i = 0; i < Marshal.SizeOf(status); i++)
            {
                Console.Write($"{bytePointer4[i]:X2} "); // 输出每个字节的十六进制表示
            }
            Console.WriteLine("");
        }









    }
}



