using Google.Protobuf;
//using Proto3FlightInfo;
//using OperFlightControl;

using System;
using Google.Protobuf.Examples.AddressBook;
using static Google.Protobuf.Examples.AddressBook.Person.Types;
using Google.Protobuf;
using Google.Protobuf.Reflection;

class Program_p
{

    public static void PrintMessage(IMessage message)
    {
        var descriptor = message.Descriptor;
        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            Console.WriteLine(
                "Field {0} ({1}): {2}",
                field.FieldNumber,
                field.Name,
                field.Accessor.GetValue(message));
        }
    }


    static void Main123456()
    {

        Person john = new Person
        {
            Id = 1234,
            Name = "John Doe",
            Email = "jdoe@example.com",
            Phones = { new Person.Types.PhoneNumber { Number = "555-4321", Type = Person.Types.PhoneType.Home } }
        };
        Person Kae = new Person
        {
            Id = 2345,
            Name = "Kae Doe",
            Email = "Kae@example.com",
            Phones = { new Person.Types.PhoneNumber { Number = "66668888", Type = Person.Types.PhoneType.Home } }
        };

        PrintMessage(Kae);


    }


}
