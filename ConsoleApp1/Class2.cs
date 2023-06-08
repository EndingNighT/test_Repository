// Assembly1.cs  
// Compile with: /target:library  
using System.Reflection.Metadata.Ecma335;

internal class BaseClass
{
    public static int intM = 0;

    public string Name { get; set; } = "NotExist";

    public void Print()
    {
        Console.WriteLine("{0}", intM);
    }


    public void Print2(string str) => Console.WriteLine("yes");

    //public delegate void Print3 = ()  { int a = 0; return a; };

    

}



class BaseClass2
{
    // Only accessible within the same assembly.
    internal static int x = 0;







}