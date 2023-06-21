using System;
using System.Reflection;
using Google.Protobuf;
//using Proto3FlightInfo;
using Google.Protobuf.Reflection;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;



public static class StructToProtobufMapper
{


}


public class Program_message
{


    enum Person
    {
        none = 0,
        good = 1,
        bad = 2,
    }

    object person = Person.good;


    public static void func<T>(T source)
    {
        System.Type type = typeof(T);
        Console.WriteLine(type.FullName);

        if (source is int)
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
