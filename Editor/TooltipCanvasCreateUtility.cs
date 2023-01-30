
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


// ReSharper disable once CheckNamespace
public static class TooltipCanvasCreateUtility
{
    [MenuItem("GameObject/UI/Aurore/Tooltip Canvas")]
    public static void CreateObject(MenuCommand menuCommand)
    {
        CreateUtility.Create("tooltipCanvas", "ToolTip Canvas");
        //Check if an event system already exists or not
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem is not null) return;
        CreateUtility.CreateObject("Event System", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
    }
    
    [MenuItem("GameObject/UI/Aurore/Progress Bar")]
    public static void CreateProgressBar(MenuCommand menuCommand)
    {
        CreateUtility.Create("progressBar", "Progress Bar");
    }
}
