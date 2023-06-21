using System;
using System.Reflection;
using Google.Protobuf;
using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;


public class Program_Test
{

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


        //StructToProtobufMapper.CopyStructToProtobuf(testInfo, protoTestInfo);

        //int a = 0;

        //Program_message.func(a);
        //Console.WriteLine("------------------------------------");
        //Console.WriteLine(a);
        //Console.WriteLine("------------------------------------");


        var outputinfo = protoTestInfo;
        Program_message.PrintMessage(protoTestInfo);

        //Program_message.CopyMessage(protoTestInfo);
        Program_message.Cocopy(testInfo, protoTestInfo);
        Program_message.PrintMessage(protoTestInfo);

        

    }
}

