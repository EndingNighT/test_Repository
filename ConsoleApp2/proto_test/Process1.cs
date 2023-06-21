using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;


public class Program1
{
    public static void process_1()
    {

        Proto_TestInfo_list2 testInfo = new Proto_TestInfo_list2
        {
            IsUsingBattery = true,
            FuelRemaining1 = 50,
            MinMotorVoltage1 = 3.5f,
            CruiseFuelEngineRPMs1 = new bool[10] { true, false, true, true, false, true, false, false, true, false },
            Milliseconds1 = new Int32[5] { 100, 200, 300, 400, 500 },
            BatteryRemaining = new float[3] { 1.5f, 2.0f, 1.8f },
            TestString = "sensor message"
            // Initialize other members as needed
        };

        Proto_TestInfo protoTestInfo = new Proto_TestInfo();

        RepeatedField<bool> cruiseFuelEngineRPMs1 = protoTestInfo.CruiseFuelEngineRPMs1;

        //StructToProtobufMapper.CopyStructToProtobuf(testInfo, protoTestInfo);

        //int a = 0;

        //Program_message.func(a);
        //Console.WriteLine("------------------------------------");
        //Console.WriteLine(a);
        //Console.WriteLine("------------------------------------");


        var outputinfo = protoTestInfo;
        Program_message.PrintMessage(protoTestInfo);

        Program_message.Cocopy(testInfo, protoTestInfo);

        Program_message.PrintMessage(protoTestInfo);


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

    //RemoteReadonlyTs<bool>

    public static void Cocopy<TStruct, TMessage>(TStruct source, TMessage message)
        where TStruct : struct
        where TMessage : IMessage
    {
        var structType = typeof(TStruct);
        var descriptor = message.Descriptor;

        var od = descriptor.Fields.InDeclarationOrder();

        var sf = structType.GetFields();
        var a = descriptor.Fields.InDeclarationOrder();

        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            try
            {
                var proto_value = field.Accessor.GetValue(message);
                if (!field.IsRepeated)
                {
                    if (field.FieldType == FieldType.Bool | field.FieldType == FieldType.Int32 | field.FieldType == FieldType.Float | field.FieldType == FieldType.Double)
                    {
                        var structfield = structType.GetField(field.Name);
                        if (structfield != null)
                        {
                            var value = structfield.GetValue(source);
                            if (value != null)
                            {
                                var type = value.GetType();
                                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    dynamic v = value;
                                    field.Accessor.SetValue(message, v.Value);
                                    continue;
                                }
                                else if (type == typeof(Nullable<float>))
                                {
                                    Nullable<float> v = (Nullable<float>)value;
                                    field.Accessor.SetValue(message, v.Value);
                                    continue;
                                }
                                else if (type == typeof(bool) | type == typeof(int) | type == typeof(float) | type == typeof(double))
                                {
                                    value = Convert.ChangeType(value, proto_value.GetType());
                                    field.Accessor.SetValue(message, value);
                                    continue;
                                }

                                value = Convert.ChangeType(value, proto_value.GetType());
                                field.Accessor.SetValue(message, value);
                            }
                        }
                        proto_value = field.Accessor.GetValue(message);
                    }
                }

                if (field.IsRepeated)
                {
                    if (field.FieldType == FieldType.Bool | field.FieldType == FieldType.Int32 | field.FieldType == FieldType.Float | field.FieldType == FieldType.Double)
                    {
                        var structfield = structType.GetField(field.Name);
                        if (structfield != null)
                        {
                            var value = structfield.GetValue(source);
                            if (value != null)
                            {
                                var type = value.GetType();
                                if (type.IsArray)
                                {
                                    dynamic repeatedInt32Field = field.Accessor.GetValue(message);

                                    value = Convert.ChangeType(value, value.GetType());

                                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    {

                                        Console.WriteLine("aaaa");
                                    }

                                    //if (value is IEnumerable<Nullable<bool>> enumerable)
                                    //{
                                    //    repeatedInt32Field?.AddRange(enumerable);
                                    //}

                                    //if (value is IEnumerable<float> enumerable)
                                    //{
                                    //    repeatedInt32Field?.AddRange(enumerable);
                                    //}


                                    if (type == typeof(bool[]))
                                    {
                                        IEnumerable<bool> value1 = (IEnumerable<bool>)value;
                                        repeatedInt32Field?.AddRange(value1);
                                    }
                                    else if (type == typeof(int[]))
                                    {
                                        IEnumerable<int> value1 = (IEnumerable<int>)value;
                                        repeatedInt32Field?.AddRange(value1);
                                    }
                                    else if (type == typeof(float[]))
                                    {
                                        //IEnumerable<float> value1 = (IEnumerable<float>)value;
                                        //repeatedInt32Field?.AddRange(value1);
                                    }
                                    else if (type == typeof(double[]))
                                    {
                                        IEnumerable<double> value1 = (IEnumerable<double>)value;
                                        repeatedInt32Field?.AddRange(value1);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }

}

