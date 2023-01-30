using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class CreateUtility
{
    public static void Create(string path, string name)
    {
        var newObject = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        newObject.name = name;
        Place(newObject);
    }

    public static void CreateObject(string name, params Type[] types)
    {
        var newObject = ObjectFactory.CreateGameObject(name, types);
        Place(newObject);
    }

    private static void Place(GameObject go)
    {
        //Find location
        SceneView lastView = SceneView.lastActiveSceneView;
        go.transform.position = lastView ? lastView.pivot : Vector3.zero;
        
        //Make sure we place the object un the proper scene, with relevant name
        StageUtility.PlaceGameObjectInCurrentStage(go);
        GameObjectUtility.EnsureUniqueNameForSibling(go);
        
        //Record undo, and select
        Undo.RegisterCreatedObjectUndo(go, "Create Object :" + go.name);
        //Selection.activeGameObject = go;
        
        //For prefab, let's mark the scene as dirty for saving
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
