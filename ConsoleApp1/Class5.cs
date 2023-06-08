using System;
using System.Reflection;
using Google.Protobuf.Collections;
using Proto3FlightInfo;

// 使用 Protobuf 定义的类
//message Proto_PowerInfo
//{
//    optional bool IsUsingBattery = 1;
//    repeated float BatteryVoltage = 2;
//    repeated float BatteryCurrent = 3;
//    repeated float BatteryRemaining = 4;
//    repeated float BatteryCapacity = 5;
//    optional bool IsUsingFuel = 6;
//    optional float FuelRemaining = 7;
//}

public struct MyStruct
{
    public bool IsUsingBattery;
    public float[] BatteryVoltage;
    public float[] BatteryCurrent;
    public float[] BatteryRemaining;
    public float[] BatteryCapacity;
    public bool IsUsingFuel;
    public float FuelRemaining;
}

public class Program
{
    public static void Main231231()
    {
        MyStruct myStruct = new MyStruct
        {
            IsUsingBattery = true,
            BatteryVoltage = new float[] { 3.7f, 3.8f, 3.6f },
            BatteryCurrent = new float[] { 1.2f, 1.1f, 1.3f },
            BatteryRemaining = new float[] { 80.5f, 75.2f, 85.3f },
            BatteryCapacity = new float[] { 1000f, 1200f, 900f },
            IsUsingFuel = false,
            FuelRemaining = 50.2f
        };

        Proto_PowerInfo data = new Proto_PowerInfo();

        CopyStructToProtobuf(myStruct, data);

        var defaultlist = new List<float> { 3.7f, 3.8f, 3.6f };

        Console.WriteLine($"IsUsingBattery: {data.IsUsingBattery}");


        var test2 = myStruct.BatteryVoltage;
        var test = data.BatteryVoltage;

        Console.WriteLine("BatteryVoltage: " + string.Join(", ", values: data.BatteryVoltage));
        Console.WriteLine("BatteryCurrent: " + string.Join(", ", data.BatteryCurrent));
        Console.WriteLine("BatteryRemaining: " + string.Join(", ", data.BatteryRemaining));
        Console.WriteLine("BatteryCapacity: " + string.Join(", ", data.BatteryCapacity));
        Console.WriteLine($"IsUsingFuel: {data.IsUsingFuel}");
        Console.WriteLine($"FuelRemaining: {data.FuelRemaining}");
    }

    public static void CopyStructToProtobuf<TStruct>(TStruct source, Proto_PowerInfo destination)
        where TStruct : struct
    {
        Type structType = typeof(TStruct);
        Type messageTypeInfo = typeof(Proto_PowerInfo);

        foreach (FieldInfo structField in structType.GetFields())
        {
            PropertyInfo messageProperty = messageTypeInfo.GetProperty(structField.Name);
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structField.GetValue(source);
                messageProperty.SetValue(destination, value);
            }
        }
    }
}
