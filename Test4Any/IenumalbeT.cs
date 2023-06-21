using Google.Protobuf.Collections;
using System;
using System.Reflection;

public class ProgramT
{
    public static void MainT()
    {
        RepeatedField<int> intField = new RepeatedField<int> { 1, 2, 3, 4, 5 };
        RepeatedField<bool> boolField = new RepeatedField<bool> { true, false, true };
        RepeatedField<float> floatField = new RepeatedField<float> { 1.0f, 2.0f, 3.0f };

        ReadAndAssignRepeatedField(intField);
        ReadAndAssignRepeatedField(boolField);
        ReadAndAssignRepeatedField(floatField);
    }

    public static void ReadAndAssignRepeatedField<T>(RepeatedField<T> field)
    {
        Type fieldType = typeof(RepeatedField<T>);
        PropertyInfo valuesProperty = fieldType.GetProperty("Values");
        MethodInfo addMethod = fieldType.GetMethod("Add");

        if (valuesProperty != null && addMethod != null)
        {
            var values = (IEnumerable<T>)valuesProperty.GetValue(field);
            var enumerator = values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                T value = enumerator.Current;
                // 在这里进行需要的赋值操作
                Console.WriteLine(value);
            }
        }
        else
        {
            Console.WriteLine("无法读取或赋值 RepeatedField 变量");
        }
    }
}
