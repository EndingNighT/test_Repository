using DelegateAppl;
using System;

delegate int NumberChanger(int n);
namespace DelegateAppl
{
    class TestDelegate
    {
        public static int num = 10;
        public int num2 = 20;



        public static int AddNum(int p)
        {
            num += p;
            return num;
        }

        public static int MultNum(int q)
        {
            num *= q;
            return num;
        }
        public static int getNum()
        {
            return num;
        }

        static void Main23(string[] args)
        {

            BaseClass.intM = 6;
            var baseClass = new BaseClass();
            //baseClass.Name = "Test";
            Console.WriteLine("{0}",baseClass.Name);
            baseClass.Print();

            
            BaseClass2 baseClass2 = new();
            Console.WriteLine("{0}", BaseClass2.x);
            BaseClass2.x = 2;
            //baseClass2.x;

            



            //Console.WriteLine("{0}", baseClass.intM);

            // 创建委托实例
            NumberChanger nc1 = new NumberChanger(AddNum);
            NumberChanger nc2 = new NumberChanger(MultNum);
            // 使用委托对象调用方法
            nc1(25);
            Console.WriteLine("Value of Num: {0}", getNum());
            nc2(5);
            Console.WriteLine("Value of Num: {0}", getNum());
            Console.ReadKey();
        }
    }
}




// Assembly2_a.cs  
// Compile with: /reference:Assembly2.dll  
public class TestAccess
{
    static void Main243()
    {
        var myBase = new BaseClass();   // Ok.  
        BaseClass.intM = 444;    // CS0117  

        Console.WriteLine("Value of Num: {0}", 2);


        TestDelegate testDelegate = new TestDelegate();
        Console.WriteLine("{0}", TestDelegate.num);
        Console.WriteLine("{0}", testDelegate.num2);


    }
}





