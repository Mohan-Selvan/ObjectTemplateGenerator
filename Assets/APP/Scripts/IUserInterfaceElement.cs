
using System.Collections.Generic;
using com.UOTG.Components;
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

    public interface IUserInterfaceElement
    {
        //public UserInterfaceElementType GetElementType();

        public List<IUserInterfaceElement> GetChildren();
    }

    [System.Serializable]
    public class UserInterfaceElementBase : IUserInterfaceElement
    {
        [JsonConstructor]
        public UserInterfaceElementBase()
        {
            Children = new List<IUserInterfaceElement>();
        }

        [JsonProperty("object_name")]
        [field: SerializeField] public string ObjectName { get; set; }

        [JsonProperty("transform")]
        [field: SerializeField] public UIRectTransformComponent RectTransformComponent { get; set; }

        [JsonProperty("type")]
        [field: SerializeField] public virtual UserInterfaceElementType ElementType { get; set; }

        [JsonProperty("children")]
        [field: SerializeField] public List<IUserInterfaceElement> Children { get; set; }

        public List<IUserInterfaceElement> GetChildren()
        {
            return new List<IUserInterfaceElement>() { };
        }
    }
}


