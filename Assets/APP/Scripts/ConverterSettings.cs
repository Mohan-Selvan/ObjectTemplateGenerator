using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace com.UOTG
{
    public static class ConverterSettings
    {
        public readonly static JsonSerializerSettings GenericSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };
    }
}


