//using System;
//using System.Reflection;


//public class Program_Test
//{

//    public static void Main()
//    {
//        //Program1.Main1();

//        //Program2.Main2();

//        Program3.Main3();

//    }

//}

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {

        Console.WriteLine("Rotor reg thrust(m/s²):" + " ");
        Console.WriteLine("Rotor reg roll(m/s²)  \t:" + " ");
        Console.WriteLine("Rotor reg pitch(m/s²) :" + " ");
        Console.WriteLine("Rotor reg yaw(m/s²)   :" + " ");

        object obj = new float[] { 1.0f, 2.0f, 3.0f };

        double a = 1.0000;
        double b = -1.21684;
        double c = 0;
        double d = 0.000000;
        double e = 0.000001;
        double f = 1;
        String fm = "g3";
        Console.WriteLine(a.ToString(fm).PadRight(1).PadLeft(6));
        Console.WriteLine(b.ToString(fm).PadRight(1).PadLeft(6));
        Console.WriteLine(c.ToString(fm));
        Console.WriteLine(d.ToString(fm));
        Console.WriteLine(e.ToString(fm));
        Console.WriteLine(f.ToString(fm));

    }
}


