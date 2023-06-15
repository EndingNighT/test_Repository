//using Google.Protobuf;
//using Google.Protobuf.Collections;
//using Google.Protobuf.Reflection;

//// 假设已经加载了Proto文件并构建了相应的描述符对象
//FileDescriptorSet descriptorSet = ...; // 已加载的Proto文件描述符集合
//FileDescriptorProto fileDescriptor = descriptorSet.Files[0]; // 假设只有一个Proto文件
//MessageDescriptorProto messageDescriptor = fileDescriptor.MessageTypes[0]; // 假设要操作的消息类型是第一个

//// 获取 repeated int32 字段的描述符
//FieldDescriptorProto fieldDescriptor = messageDescriptor.Fields[0]; // 假设第一个字段是 repeated int32 类型

//// 创建消息类型的动态生成类
//MessageDescriptor dynamicDescriptor = MessageDescriptor.BuildFrom(messageDescriptor, fileDescriptor);
//IMessage dynamicMessage = dynamicDescriptor.Parser.DefaultTemplateInstance;

//// 将值赋给 repeated int32 字段
//int[] values = { 1, 2, 3, 4, 5 }; // 假设要赋的值是 1, 2, 3, 4, 5
//dynamicMessage[fieldDescriptor] = new RepeatedField<int>(values);

//// 可以通过以下方式获取字段的值
//RepeatedField<int> fieldValues = (RepeatedField<int>)dynamicMessage[fieldDescriptor];

//// 输出字段的值
//foreach (int value in fieldValues)
//{
//    Console.WriteLine(value);
//}
