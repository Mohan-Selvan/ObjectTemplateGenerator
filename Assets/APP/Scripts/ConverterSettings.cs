using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using com.UOTG.Elements;

namespace com.UOTG
{
    public static class ConverterSettings
    {
        public readonly static JsonSerializerSettings GenericSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            Converters =
            {
                new UserInterfaceElementConverter()
            },
        };

        public class UserInterfaceElementConverter : CustomCreationConverter<IUserInterfaceElement>
        {
            private UserInterfaceElementType elementType;

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var jobj = JObject.ReadFrom(reader);
                elementType = jobj["type"].ToObject<UserInterfaceElementType>();

                Debug.Log($"ReadJson called, Deserializing {elementType}");

                return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
            }

            public override IUserInterfaceElement Create(Type objectType)
            {
                Debug.Log($"Create called, Deserializing {elementType}");

                switch (elementType)
                {
                    case UserInterfaceElementType.RECT:
                        return new UIEmptyRect();
                    
                    case UserInterfaceElementType.TEXT:
                        return new UIText();

                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}


