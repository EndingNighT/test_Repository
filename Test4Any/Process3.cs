using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Program3
{
    public static void Main3()
    {
        List<bool> boolList = new List<bool> { true, false, true };
        List<float> floatList = new List<float> { 1.5f, 2.5f, 3.5f };

        object obj1 = boolList;
        object obj2 = floatList;

        if (IsListType(obj1))
        {
            // obj1 是 List<> 类型
            dynamic newList1 = CreateListFromObject(obj1);
            // 在这里可以使用泛型方式来操作 newList1
            newList1.Add(true);
            newList1.RemoveAt(1);
            Console.WriteLine(string.Join(", ", newList1)); // Output: True, True
        }

        if (IsListType(obj2))
        {
            // obj2 是 List<> 类型
            dynamic newList2 = CreateListFromObject(obj2);
            // 在这里可以使用泛型方式来操作 newList2
            newList2.Add(2.0f);
            newList2.Sort();
            Console.WriteLine(string.Join(", ", newList2)); // Output: 1.5, 2.5, 3.5, 4.5
        }
    }

    static bool IsListType(object obj)
    {
        Type objectType = obj.GetType();
        return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(List<>);
    }

    static dynamic CreateListFromObject(object obj)
    {
        Type objectType = obj.GetType();
        Type elementType = objectType.GetGenericArguments().First();
        Type listType = typeof(List<>).MakeGenericType(elementType);

        dynamic newList = Activator.CreateInstance(listType);

        // 复制对象中的元素到新的列表
        IEnumerable<object> items = ((IEnumerable)obj).Cast<object>();
        foreach (var item in items)
        {
            // 调用 Add 方法
            listType.InvokeMember("Add", BindingFlags.InvokeMethod, null, newList, new object[] { item });
        }

        return newList;
    }
}
