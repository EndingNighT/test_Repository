using System;

public class Person
{
    public String FirstName;


    public bool Equals(object obj)
    {
        var p2 = obj as Person;
        if (p2 == null)
            return false;
        else
            return FirstName.Equals(p2.FirstName);
    }

    public static int employeeCounter;
    public static string DataName => "FuelEngine";

    public static readonly string DataName2 = "FuelEngine";
}

class Animal
{
    public void Eat() => Console.WriteLine("Eating.");
    public  string String1 => "I am an animal.";
}



public class ClassTypeExample
{
    public static void Main123()
    {

        Person p1 = new Person();


        string[] words = { "bot", "apple", "apricot" };
        int minimalLength = words
          .Where(w => w.StartsWith("a"))
          .Min(w => w.Length);
        Console.WriteLine(minimalLength);   // output: 5

        int[] numbers = { 4, 7, 10 };
        int product = numbers.Aggregate(1, (interim, next) => interim * next);
        Console.WriteLine(product);   // output: 280

        

        int[] numbers2 = { 4, 7, 10 };
        int product2 = numbers.Aggregate(1, (int interim, int next) => interim * next);
        Console.WriteLine(product2);   // output: 280

        Func<string> greet = () => "Hello, World!";//greet: System.Func<string> 类型
        Console.WriteLine(greet());
        var a = greet();//a: string 类型
        Console.WriteLine(a);
    }
}
















