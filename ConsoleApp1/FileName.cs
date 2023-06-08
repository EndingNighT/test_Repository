using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;




public static class StructToProtobufMapper
{
    public static void CopyStructToProtobuf<TStruct, TMessage>(TStruct source, TMessage destination)
        where TStruct : struct
        where TMessage : IMessage
    {
        Type structType = typeof(TStruct);
        Type messageType = typeof(TMessage);

        foreach (FieldInfo structField in structType.GetFields())
        {
            PropertyInfo messageProperty = messageType.GetProperty(structField.Name);
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structField.GetValue(source);
                messageProperty.SetValue(destination, value);
            }
            if (messageProperty != null && messageProperty.CanWrite)
            {
                object value = structField.GetValue(source);
                if (structField.FieldType == typeof(float[]))
                {
                    float[]? floatArray = (float[]?)value;
                    if (floatArray != null)
                    {
                        float[] destinationArray = new float[floatArray.Length];
                        Array.Copy(floatArray, destinationArray, floatArray.Length);
                        messageProperty.SetValue(destination, destinationArray);
                    }
                }
                else
                {
                    messageProperty.SetValue(destination, value);
                }
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
    public static void Main()
    {
        PowerInfo_list powerInfoList = new PowerInfo_list
        {
            IsUsingBattery = true,
            BatteryVoltage = new float[] { 3.7f, 3.6f, 3.8f },
            BatteryCurrent = new float[] { 1.2f, 1.5f, 1.3f },
            BatteryRemaining = new float[] { 70.5f, 65.2f, 75.0f },
            BatteryCapacity = new float[] { 1000f, 1500f, 1200f },
            IsUsingFuel = false,
            FuelRemaining = null
        };

        Proto_PowerInfo protoPowerInfo = new Proto_PowerInfo();

        StructToProtobufMapper.CopyStructToProtobuf(powerInfoList, protoPowerInfo);

        Console.WriteLine($"IsUsingBattery: {protoPowerInfo.IsUsingBattery}, " +
                          $"BatteryVoltage: {string.Join(", ", protoPowerInfo.BatteryVoltage)}, " +
                          $"BatteryCurrent: {string.Join(", ", protoPowerInfo.BatteryCurrent)}, " +
                          $"BatteryRemaining: {string.Join(", ", protoPowerInfo.BatteryRemaining)}, " +
                          $"BatteryCapacity: {string.Join(", ", protoPowerInfo.BatteryCapacity)}, " +
                          $"IsUsingFuel: {protoPowerInfo.IsUsingFuel}, " +
                          $"FuelRemaining: {protoPowerInfo.FuelRemaining}");
    }
}

