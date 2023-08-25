
using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;

namespace com.UOTG.Elements
{
    public enum UserInterfaceElementType
    {
        DEFAULT = 0,
        RECT = 1,
        IMAGE = 2,
        BUTTON = 3,
        TEXT = 4
    }

    public class UserInterfaceElement
    {
        public virtual UserInterfaceElementType ElementType { get; }

        public UserInterfaceElement()
        {
            Children = new List<UserInterfaceElement>();
        }

        [JsonProperty("children")]
        public virtual List<UserInterfaceElement> Children { get; set; }
    }
}


