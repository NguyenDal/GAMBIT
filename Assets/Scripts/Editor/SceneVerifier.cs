using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneVerifier : Editor
{
    [MenuItem("Tools/Verify Scenes")]
    public static void VerifyScenes()
    {
        string[] requiredScenes = {
            "Assets/_Scenes/Levels/Level1.unity.orig",
            "Assets/_Scenes/Levels/Level2.unity.orig",
            "Assets/_Scenes/Levels/Level3.unity.orig"
        };

        foreach (var scenePath in requiredScenes)
        {
            if (!System.IO.File.Exists(scenePath))
            {
                Debug.LogError("Scene not found: " + scenePath);
                EditorApplication.Exit(1); 
            }
        }
        Debug.Log("All required scenes are present.");
        EditorApplication.Exit(0); 
    }
}
