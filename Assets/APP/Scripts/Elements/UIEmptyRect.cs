using Newtonsoft.Json;
using UnityEngine;


namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIEmptyRect : UserInterfaceElementBase
    {

        public UIEmptyRect() : base()
        {
            
        }

        public static string Serialize(UIEmptyRect content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIEmptyRect Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIEmptyRect>(message, ConverterSettings.GenericSettings);
        }
    }

}

