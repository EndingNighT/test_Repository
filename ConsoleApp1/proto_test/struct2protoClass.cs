using System;
using System.Reflection;
using System.Text;
//using NATS.Client;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Proto3FlightInfo;

// 使用 Protobuf 定义的类
//Proto_PowerInfo Info = new Proto_PowerInfo();
//Proto_GenerateInfo Info = new Proto_GenerateInfo();

public struct PowerInfo_list
{
    public bool? IsUsingBattery;             // 是否使用电池
    public float[]? BatteryVoltage;   // 电池电压
    public float[]? BatteryCurrent;   // 电池电流
    public float[]? BatteryRemaining; // 电池剩余电量
    public float[]? BatteryCapacity;// 电池容量
    public bool? IsUsingFuel;    // 是否使用油箱
    public float? FuelRemaining; // 油箱剩余油量

}

//info list 2 proto data
public class Program2
{
    static void my_func(object src)//static void my_func(object src, ref Proto_PowerInfo dst)
    {
        System.Type src_type = src.GetType(); //PowerInfo_list
        System.Type dst_type = typeof(Proto_PowerInfo); //Proto_PowerInfo

        MemberInfo[] src_members = src_type.GetMembers();

        List<string> a1 = new List<string>();//存fieldName
        List<object> a2 = new List<object>();//存fieldValue

        foreach (var member in src_members)
        {
            // 如果成员是字段(Field)类型，则输出字段名和字段值
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)member;
                string fieldName = field.Name;
                object fieldValue = field.GetValue(src);



                //dst.fieldValue

                // i cant solve this
                //second



                Console.WriteLine(fieldName + ": " + fieldValue);
                a1.Add(fieldName); a2.Add(fieldValue);
            }
        }

        foreach (var a in a1)
        {


        }
        foreach (var a in a2)

        {
            //dst.get
            try
            {
                System.Type? type22 = a?.GetType();
            }
            catch { }
        }

    }

    public static void Mai12312n()
    {

        Proto_PowerInfo data = new Proto_PowerInfo();
        //Info.IsUsingBattery = true;

        PowerInfo_list info = new PowerInfo_list
        {
            IsUsingBattery = true,
            BatteryVoltage = new float[] { 3.7f, 3.8f, 3.6f },
            BatteryCurrent = new float[] { 4f, 3.8f, 3.6f },
            BatteryRemaining = new float[] { 5f, 3.8f, 3.6f },
            BatteryCapacity = new float[] { 6f, 3.8f, 3.6f },
            IsUsingFuel = false,
            //FuelRemaining= 50
        };


        //my_func(info, ref data);
        my_func(info);

        //testtool.CopyStructToClass(info, data);

        testtool.CopyStructToProtobuf(info, data);

        my_func(data);


        data.IsUsingBattery = false;
        System.Type src_type = typeof(Proto_PowerInfo);
        MemberInfo[] src_members = src_type.GetMembers();



        var IsUsingBattery = data.IsUsingBattery;
        var BatteryVoltage = data.BatteryVoltage;
        var BatteryCurrent = data.BatteryCurrent;
        var BatteryRemaining = data.BatteryRemaining;
        var BatteryCapacity = data.BatteryCapacity;
        var IsUsingFuel = data.IsUsingFuel;








    }
}

class testtool
{


    public static void CopyStructToClass<TStruct, TClass>(TStruct source, TClass destination)
    {
        System.Type structType = typeof(TStruct);
        System.Type classType = typeof(TClass);

        foreach (FieldInfo structField in structType.GetFields())
        {
            FieldInfo classField = classType.GetField(structField.Name);
            if (classField != null)
            {
                object value = structField.GetValue(source);
                classField.SetValue(destination, value);
            }
        }

        foreach (PropertyInfo structProperty in structType.GetProperties())
        {
            PropertyInfo classProperty = classType.GetProperty(structProperty.Name);
            if (classProperty != null && classProperty.CanWrite)
            {
                object value = structProperty.GetValue(source);
                classProperty.SetValue(destination, value);
            }
        }
    }
    public static void CopyStructToProtobuf<TStruct>(TStruct source, Proto_PowerInfo destination)
    where TStruct : struct
    {
        System.Type structType = typeof(TStruct);
        System.Type messageTypeInfo = typeof(Proto_PowerInfo);

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











