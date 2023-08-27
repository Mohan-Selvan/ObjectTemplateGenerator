using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.UOTG 
{
    public static class TemplateManager
    {
        //[MenuItem("UOTG/CreateTemplate")]
        //public static void CreateUITemplateForGameObject(GameObject targetObject)
        //{
        //    if (targetObject == null)
        //    {
        //        return;
        //    }

        //    RectTransform rectTransfromComponent = targetObject.transform as RectTransform;

        //    if(rectTransfromComponent == null)
        //    {
        //        EditorUtility.DisplayDialog(
        //            title: "Error",
        //            message: $"No rect transform found on the selected object",
        //            ok: "Ok");
        //        return;
        //    }

        //    if(targetObject.TryGetComponent<TemplateHandler>(out TemplateHandler t))
        //    {
        //        EditorUtility.DisplayDialog(
        //            title: "Error",
        //            message: $"Selected object : ({targetObject.name}) is already a template",
        //            ok: "Ok");
        //        return;
        //    }

        //    TemplateHandler.BuildElementDataTree(rectTransfromComponent);
        //}
    }
}



