
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

    public interface IUserInterfaceElement
    {
        public UserInterfaceElementType GetElementType();

        public List<IUserInterfaceElement> GetChildren();
    }

    [System.Serializable]
    public abstract class UserInterfaceElementBase : IUserInterfaceElement
    {
        [JsonConstructor]
        public UserInterfaceElementBase()
        {
            //Children = new List<IUserInterfaceElement>();
        }

        //[JsonProperty("children")]
        //public List<IUserInterfaceElement> Children { get; set; }

        public List<IUserInterfaceElement> GetChildren()
        {
            return new List<IUserInterfaceElement>() { };
        }

        public abstract UserInterfaceElementType GetElementType();

    }
}


