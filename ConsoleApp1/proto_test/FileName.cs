using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using NATS.Client.KeyValue;


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


public class Program_message
{
    public static void PrintMessage(IMessage message)
    {
        var descriptor = message.Descriptor;
        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            Console.WriteLine(
                "Field {0} {1} ({2}): {3}",
                field.FieldNumber,
                field.FieldType,
                field.Name,
                field.Accessor.GetValue(message));

            //Console.WriteLine(field.FieldType); // FieldType 枚举类型


        }
    }

    public static void CopyMessage(IMessage message)
    {
        var descriptor = message.Descriptor;
        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
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

                    repeatedInt32Field?.Add(0);

                    var test = new Int32[5] { 100, 200, 300, 400, 500 };
                    repeatedInt32Field?.AddRange(test);

                }
                else if (field.FieldType == FieldType.Float && field.IsRepeated)
                {
                    var repeatedFloatField = field.Accessor.GetValue(message) as RepeatedField<float>;
                }

                else if (field.FieldType == FieldType.Enum && !field.IsRepeated)
                {


                    EnumDescriptor enumDescriptor = field.EnumType;
                    EnumValueDescriptor enumValueDescriptor = enumDescriptor.FindValueByNumber(2);
                    int enumValue = enumValueDescriptor.Number;// Warning
                    field.Accessor.SetValue(message, enumValue);
                    //field.Accessor.SetValue(message, 2);//Error


                    ////var enumValueDescriptor = field.Accessor.GetValue(message) as EnumValueDescriptor;
                    ////var testss = field.Accessor.GetValue(message) as IEnumerable<EnumValueDescriptor>;


                    //string n = field.Name; // MessageLevel
                    //string s = field.EnumType.Name; // Proto_MessageLevel
                    //System.Type ct = field.EnumType.ClrType; // MetaData.Proto_MessageLevel

                    var value = (int)field.Accessor.GetValue(message);
                    //var a = value.GetType();


                    }
                else if (field.FieldType == FieldType.Enum && field.IsRepeated)
                {
                    //continue;
                    // TODO repeated的Enum类型
                    var inpu = field.Accessor.GetValue(message);



                    Console.WriteLine("---------    {0}", field.Accessor.GetValue(message).GetType());



                    var testss = field.Accessor.GetValue(message) as IEnumerable<EnumValueDescriptor>;

                    var aa = field.Accessor.GetValue(message);
                    var repeatedEnumField = field.Accessor.GetValue(message) as RepeatedField<IEnumerable<EnumValueDescriptor>>;//object { Google.Protobuf.Collections.RepeatedField<MetaData.Proto_MessageLevel>}

                    Console.WriteLine("after    {0}", field.EnumType.ClrType); // MetaData.Proto_MessageLevel

                    Console.WriteLine("after    {0}", field.Name); // MessageLevel
                    continue;
                    foreach (var enumValue in repeatedEnumField)
                    {

                    }

                    var enumValueDescriptor = field.EnumType.FindValueByNumber(1);
                    //field.Accessor.SetValue(message, 2);

                    //repeatedEnumField.Add(enumValueDescriptor);

                }
                else if (field.FieldType == FieldType.Message)
                {
                    continue;
                    // TODO optional的Message类型
                    // TODO repeated的Message类型
                    var subMessage = field.Accessor.GetValue(message) as IMessage; //自行决定是否判空

                    //var repeatedMessageField = field.Accessor.GetValue(message) as RepeatedField<IMessage>;
                }
                else { continue; }
            }
            finally { }


        }
    }

    public static void Cocopy<TStruct, TMessage>(TStruct source, TMessage message)
        where TStruct : struct
        where TMessage : IMessage
    {
        var structType = typeof(TStruct);
        var descriptor = message.Descriptor;
        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            if (!field.IsRepeated)
            {
                if (field.FieldType == FieldType.Bool | field.FieldType == FieldType.Int32 | field.FieldType == FieldType.Float | field.FieldType == FieldType.Double)
                {
                    var structfield = structType.GetField(field.Name);
                    if (structfield != null)
                    {
                        var value = structfield.GetValue(source);
                        if (value != null)
                            field.Accessor.SetValue(message, value);
                    }
                }
            }
        }
    }
    enum Person
    {
        none=0,
        good=1,
        bad=2,
    }

    object person = Person.good;


    public static void func<T>(T source)
    {
        System.Type type= typeof(T);
        Console.WriteLine(type.FullName);

        if(source is int)
        {
            //int value = (int)(object)source;

            var value = source as int?;


            Console.WriteLine($"int   {value}");
            value = 1;

            int intValue = Convert.ToInt32(source);
            intValue = 1;

        }

        var src = source as int?;
        if (src != null)
        {
            Console.WriteLine($"int   {src}");
        }



    }

    public void AssignValue<T>(ref T variable, T value)
    {
        variable = value;
    }


    public IEnumerable<T>? GetValue<T>(object a)
    {
        //return a;
        var testss = a as IEnumerable<T>;

        return testss;
    }


}

