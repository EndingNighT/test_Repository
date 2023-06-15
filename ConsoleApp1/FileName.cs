using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

// 使用 Protobuf 定义的类
//message Proto_TestInfo
//{
//    optional bool IsUsingBattery = 1;             // 是否使用电池
//    optional bool IsUsingFuel = 2;    // 是否使用油箱

//    optional int32 FuelRemaining1 = 3;
//    optional int32 FuelRemaining2 = 4;

//    optional uint32 FlySeconds1 = 5;
//    optional uint32 FlySeconds2 = 6;

//    optional float MinMotorVoltage1 = 7; // 油箱剩余油量
//    optional float MinMotorVoltage2 = 8; // 油箱剩余油量

//    optional double BatteryRemaining1 = 9; // 电池剩余电量
//    optional double BatteryRemaining2 = 10; // 电池剩余电量

//    repeated bool CruiseFuelEngineRPMs1 = 11;//int16
//    repeated bool CruiseFuelEngineRPMs2 = 12;//int16

//    repeated int32 Milliseconds1 = 13;//int16
//    repeated int32 Milliseconds2 = 14;//int16

//    repeated float BatteryCurrent = 15;   // 电池电流
//    repeated float BatteryRemaining = 16; // 电池剩余电量

//    repeated double SentBytesPerSecond1 = 17; // 电池剩余电量
//    repeated double SentBytesPerSecond2 = 18; // 电池剩余电量

//    optional string SensorInvalidMessage = 19; // 获取无效的传感器消息。已经关闭的传感器不会报无效状态。
//    optional MetaData.Proto_FCControlInnerLoopLonModes LonInnerMode = 20;//枚举enum
//    optional MetaData.Proto_LLA LLA =21;//message
//}

public struct Proto_TestInfo_list
{
    public bool IsUsingBattery;             // 是否使用电池
    public bool IsUsingFuel;    // 是否使用油箱

    public Int32 FuelRemaining1;
    public Int32 FuelRemaining2;

    public float MinMotorVoltage1; // 油箱剩余油量
    public float MinMotorVoltage2; // 油箱剩余油量

    public bool[] CruiseFuelEngineRPMs1;//int16
    public bool[] CruiseFuelEngineRPMs2;//int16

    public Int32[] Milliseconds1;//int16
    public Int32[] Milliseconds2;//int16

    public float[] BatteryCurrent;   // 电池电流
    public float[] BatteryRemaining; // 电池剩余电量


    public string TestString ; // string
    public string[] TestStrings; // repeated string

    //MetaData.Proto_FCControlInnerLoopLonModes LonInnerMode;//枚举enum
    //MetaData.Proto_LLA LLA;//message

}

public static class StructToProtobufMapper
{
    public static void CopyStructToProtobuftest<TStruct, TMessage>(TStruct source, TMessage destination)
    where TStruct : struct
    where TMessage : IMessage
    {
        System.Type structType = typeof(TStruct);
        System.Type messageType = typeof(TMessage);

        foreach (FieldInfo structField in structType.GetFields())
        {
            PropertyInfo messageProperty = messageType.GetProperty(structField.Name);
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structField.GetValue(source);
                messageProperty.SetValue(destination, value);
                continue;
            }
        }
        //destination.Descriptor
    }

    public static void CopyStructToProtobuf<TStruct, TMessage>(TStruct source, TMessage destination)
        where TStruct : struct
        where TMessage : IMessage
    {
        System.Type structType = typeof(TStruct);
        System.Type messageType = typeof(TMessage);

        var test1 = structType.GetFields();
        var test2 = structType.GetProperties();

        var test3 = messageType.GetFields();
        var test4 = messageType.GetProperties();

        PropertyInfo test = messageType.GetProperty("CruiseFuelEngineRPMs1");

        //messageProperty.

        foreach (FieldInfo structField in structType.GetFields())
        {
            PropertyInfo messageProperty = messageType.GetProperty(structField.Name);
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structField.GetValue(source);
                messageProperty.SetValue(destination, value);
                continue;
            }

            FieldInfo messageField = messageType.GetField(structField.Name);

            if (messageField != null)
            {

                object value = structField.GetValue(source);


                //FieldDescriptor fieldDescriptor = messageDescriptor.FindFieldByName(structField.Name);

                //if (fieldDescriptor != null && fieldDescriptor.FieldType == FieldType.Bool)


                if (structField.FieldType == typeof(bool[]))
                {
                    bool[] boolArray = (bool[])structField.GetValue(source);

                    if (boolArray != null)
                    {
                        // 创建对应长度的 repeated bool 列表
                        RepeatedField<bool> boolList = new RepeatedField<bool>();

                        // 将结构体中的 bool 数组复制到列表中
                        boolList.AddRange(boolArray);

                        // 将列表赋值给消息的字段
                        messageField.SetValue(destination, boolList);//message,boolList
                    }
                }
            }




            if (messageProperty != null)
            {
                object value = structField.GetValue(source);

                //data.BatteryCapacity.Add


                if (structField.FieldType == typeof(bool[]))
                {
                    continue;
                    bool[]? boolArray = (bool[]?)value;


                    if (boolArray != null)
                    {
                        // 创建对应长度的 repeated bool 列表
                        //RepeatedField<bool> boolList = new RepeatedField<bool>(boolArray.Length);
                        RepeatedField<bool> boolList = new RepeatedField<bool>();

                        // 将结构体中的 bool 数组复制到列表中
                        boolList.AddRange(boolArray);

                        // 将列表赋值给消息的属性
                        messageProperty.SetValue(destination, boolList);
                    }

                    if (boolArray != null)
                    {
                        bool[] destinationArray = new bool[boolArray.Length];
                        Array.Copy(boolArray, destinationArray, boolArray.Length);
                        //messageProperty.SetValue(destination, destinationArray);


                        for (var i = 0; i < boolArray.Length; i++)
                        {
                            // 电池容量
                            //destination.Add(boolArray[i]);


                        }
                    }

                }


                //if (structField.FieldType == typeof(float[]))
                //{
                //    float[]? floatArray = (float[]?)value;
                //    if (floatArray != null)
                //    {
                //        float[] destinationArray = new float[floatArray.Length];
                //        Array.Copy(floatArray, destinationArray, floatArray.Length);
                //        messageProperty.SetValue(destination, destinationArray);
                //    }
                //}
                //else
                //{
                //    messageProperty.SetValue(destination, value);
                //}


                //Console.WriteLine("##");
                //Console.WriteLine(source);
                //Console.WriteLine(structType);
                //Console.WriteLine(structField);
                //Console.WriteLine(value);

                //Console.WriteLine(destination);
                //Console.WriteLine(messageType);
                //Console.WriteLine(messageProperty);
                //Console.WriteLine(messageProperty.GetValue(destination));

            }


        }
        var asw = structType.GetProperties();
        foreach (PropertyInfo structProperty in structType.GetProperties())
        {
            PropertyInfo messageProperty = messageType.GetProperty(structProperty.Name);
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structProperty.GetValue(source);
                messageProperty.SetValue(destination, value);
            }
        }
    }


}


public class Program2323
{
    public static void PrintMessage(IMessage message)
    {
        var descriptor = message.Descriptor;
        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            Console.WriteLine(
                "Field {0} ({1}): {2}",
                field.FieldNumber,
                field.Name,
                field.Accessor.GetValue(message));

            Console.WriteLine(field.FieldType);

            try
            {
                if (field.FieldType == FieldType.Bool && !field.IsRepeated)
                {
                    field.Accessor.SetValue(message, false);
                }
                else if (field.FieldType == FieldType.Int32 && !field.IsRepeated)
                {
                    field.Accessor.SetValue(message, 1);
                }
                else if (field.FieldType == FieldType.Float && !field.IsRepeated)
                {
                    field.Accessor.SetValue(message, 0.1f);
                }
                else if (field.FieldType == FieldType.Bool && field.IsRepeated)
                {
                    var repeatedBoolField = field.Accessor.GetValue(message) as RepeatedField<bool>;
                }
                else if (field.FieldType == FieldType.Int32 && field.IsRepeated)
                {

                    var repeatedInt32Field = field.Accessor.GetValue(message) as RepeatedField<int>;

                    repeatedInt32Field.Add(0);

                    var test = new Int32[5] { 100, 200, 300, 400, 500 };
                    repeatedInt32Field.AddRange(test);

                }
                else if (field.FieldType == FieldType.Float && field.IsRepeated)
                {


                    var repeatedFloatField = field.Accessor.GetValue(message) as RepeatedField<float>;
                }

                else if (field.FieldType == FieldType.Enum && !field.IsRepeated)
                {
                    var enumValueDescriptor = field.Accessor.GetValue(message) as EnumValueDescriptor;
                    field.Accessor.SetValue(message, 2);

                    //Console.WriteLine("after    {0}", field.EnumType.ClrType); // MetaData.Proto_MessageLevel

                    //Console.WriteLine("after    {0}", field.Name); // MessageLevel
                    

                }
                else if (field.FieldType == FieldType.Enum && field.IsRepeated)
                {
                    var testss = field.Accessor.GetValue(message) as IEnumerable<EnumValueDescriptor>;

                    var aa = field.Accessor.GetValue(message);
                    var repeatedEnumField = field.Accessor.GetValue(message) as RepeatedField<IEnumerable<EnumValueDescriptor>>;//object { Google.Protobuf.Collections.RepeatedField<MetaData.Proto_MessageLevel>}

                    Console.WriteLine("after    {0}", field.EnumType.ClrType); // MetaData.Proto_MessageLevel

                    Console.WriteLine("after    {0}", field.Name); // MessageLevel
                    
                    foreach (var enumValue in repeatedEnumField)
                    {

                    }

                    var enumValueDescriptor = field.EnumType.FindValueByNumber(1);
                    //field.Accessor.SetValue(message, 2);

                    //repeatedEnumField.Add(enumValueDescriptor);

                }
                else if (field.FieldType == FieldType.Message)
                {
                    var subMessage = field.Accessor.GetValue(message) as IMessage; //自行决定是否判空

                    //var repeatedMessageField = field.Accessor.GetValue(message) as RepeatedField<IMessage>;
                }
            }
            finally { }


        }
    }

    public static void Main()
    {


        Proto_TestInfo_list testInfo = new Proto_TestInfo_list
        {
            IsUsingBattery = true,
            IsUsingFuel = false,
            FuelRemaining1 = 50,
            FuelRemaining2 = 75,
            MinMotorVoltage1 = 3.5f,
            MinMotorVoltage2 = 4.2f,
            CruiseFuelEngineRPMs1 = new bool[10] { true, false, true, true, false, true, false, false, true, false },
            CruiseFuelEngineRPMs2 = new bool[10] { false, true, false, false, true, false, true, true, false, true },
            Milliseconds1 = new Int32[5] { 100, 200, 300, 400, 500 },
            Milliseconds2 = new Int32[5] { 600, 700, 800, 900, 1000 },
            BatteryCurrent = new float[3] { 1.5f, 2.0f, 1.8f },
            BatteryRemaining = new float[3] { 70.0f, 60.0f, 75.0f },
            TestString = "sensor message"
            // Initialize other members as needed
        };

        Proto_TestInfo protoTestInfo = new Proto_TestInfo();

        RepeatedField<bool> cruiseFuelEngineRPMs1 = protoTestInfo.CruiseFuelEngineRPMs1;


        StructToProtobufMapper.CopyStructToProtobuf(testInfo, protoTestInfo);


        PrintMessage(protoTestInfo);


        var outputinfo = protoTestInfo;

        //Console.WriteLine("------------------------------------");
        //Console.WriteLine("IsUsingBattery: " + outputinfo.IsUsingBattery);
        //Console.WriteLine("IsUsingFuel: " + outputinfo.IsUsingFuel);
        //Console.WriteLine("FuelRemaining1: " + outputinfo.FuelRemaining1);
        //Console.WriteLine("FuelRemaining2: " + outputinfo.FuelRemaining2);
        //Console.WriteLine("MinMotorVoltage1: " + outputinfo.MinMotorVoltage1);
        //Console.WriteLine("MinMotorVoltage2: " + outputinfo.MinMotorVoltage2);

        //Console.WriteLine($"CruiseFuelEngineRPMs1: {string.Join(", ", outputinfo.CruiseFuelEngineRPMs1)}");
        //Console.WriteLine($"CruiseFuelEngineRPMs2: {string.Join(", ", outputinfo.CruiseFuelEngineRPMs2)}");
        //Console.WriteLine($"Milliseconds1: {string.Join(", ", outputinfo.Milliseconds1)}");
        //Console.WriteLine($"Milliseconds2: {string.Join(", ", outputinfo.Milliseconds2)}");
        //Console.WriteLine($"BatteryCurrent: {string.Join(", ", outputinfo.BatteryCurrent)}");
        //Console.WriteLine($"BatteryRemaining: {string.Join(", ", outputinfo.BatteryRemaining)}");

        //Console.WriteLine("SensorInvalidMessage: " + outputinfo.SensorInvalidMessage);
        //// Output other members as needed



    }
}

