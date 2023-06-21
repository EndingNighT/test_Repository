using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;


public struct Proto_TestInfo_list
{
    public bool IsUsingBattery;
    public Int32 FuelRemaining1;
    public float MinMotorVoltage1; // 油箱剩余油量
    public bool[] CruiseFuelEngineRPMs1;//int16
    public Int32[] Milliseconds1;//int16
    public float[] BatteryRemaining;   // 电池电流

    public string TestString; // string
    public string[] TestStrings; // repeated string

    //MetaData.Proto_FCControlInnerLoopLonModes LonInnerMode;//枚举
    //
    //MetaData.Proto_LLA LLA;//message
}

public struct Proto_TestInfo_list2
{
    public bool? IsUsingBattery;
    public Int32? FuelRemaining1;
    public float? MinMotorVoltage1; // 油箱剩余油量
    public bool[]? CruiseFuelEngineRPMs1;//int16
    public Int32[]? Milliseconds1;//int16
    public float[]? BatteryRemaining;   // 电池电流

    public string? TestString; // string
    public string[]? TestStrings; // repeated string

    //MetaData.Proto_FCControlInnerLoopLonModes LonInnerMode;//枚举
    //
    //MetaData.Proto_LLA LLA;//message
}


// 使用 Protobuf 定义的类
//Proto_PowerInfo Info = new Proto_PowerInfo();
//Proto_GenerateInfo Info = new Proto_GenerateInfo();

//用来给Proto赋值的结构体struct
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