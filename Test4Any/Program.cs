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
        object obj = new float[] { 1.0f, 2.0f, 3.0f };

        if (obj is IEnumerable<float> enumerable)
        {
            foreach (float value in enumerable)
            {
                Console.WriteLine(value);
            }
        }
        else
        {
            Console.WriteLine("无法将 object 转换为 IEnumerable<float> 类型");
        }
    }
}


