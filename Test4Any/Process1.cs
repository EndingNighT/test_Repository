using System;
using System.Reflection;

public class MyClass
{
    public int MyProperty { get; set; }
    public string MyField;

    public void MyMethod()
    {
        Console.WriteLine("Hello, world!");
    }
}

public struct MyStruct
{
    public int MyProperty { get; set; }
    public string MyField;
}

public class Program1
{
    public static void AssignClassToStruct(object classObj, ref MyStruct structObj)
    {
        Type classType = classObj.GetType();
        Type structType = typeof(MyStruct);

        

        PropertyInfo[] properties = classType.GetProperties();
        FieldInfo[] fields = classType.GetFields();

        foreach (var property in properties)
        {
            PropertyInfo structProperty = structType.GetProperty(property.Name);
            if (structProperty != null && structProperty.CanWrite)
            {
                object value = property.GetValue(classObj);
                structProperty.SetValue(structObj, value);
            }
        }

        foreach (var field in fields)
        {
            FieldInfo structField = structType.GetField(field.Name);
            if (structField != null)
            {
                object value = field.GetValue(classObj);
                structField.SetValue(structObj, value);
            }
        }
    }


    public static void Main1()
    {
        // 示例用法
        MyClass myClass = new MyClass
        {
            MyProperty = 42,
            MyField = "Hello, world!"
        };

        MyStruct myStruct = new MyStruct();
        AssignClassToStruct(myClass, ref myStruct);

        int? a = null;
        //a.HasValue = false;

        Console.WriteLine("MyStruct.MyProperty: " + myStruct.MyProperty);
        Console.WriteLine("MyStruct.MyField: " + myStruct.MyField);
    }

}



