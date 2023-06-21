using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;


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


    public string TestString; // string
    public string[] TestStrings; // repeated string

    //MetaData.Proto_FCControlInnerLoopLonModes LonInnerMode;//枚举
    //

    //MetaData.Proto_LLA LLA;//message

}
