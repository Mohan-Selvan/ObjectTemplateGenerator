using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

using com.UOTG.Elements;

namespace com.UOTG
{
    public static class ConverterSettings
    {
        public readonly static JsonSerializerSettings GenericSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
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

                return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
            }

            public override IUserInterfaceElement Create(Type objectType)
            {
                switch (elementType)
                {
                    case UserInterfaceElementType.RECT:
                        return new UIEmptyRect();
                    
                    case UserInterfaceElementType.TEXT:
                        return new UIText();

                    case UserInterfaceElementType.BUTTON:
                        return new UIButton();

                    case UserInterfaceElementType.IMAGE:
                        return new UIImage();

                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}


