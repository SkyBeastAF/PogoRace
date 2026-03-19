using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// SceneLoader — attach to any TMP Button to load a scene on tap.
///
/// Required setup:
///   • Add the target scene to your Build Settings (File > Build Settings > Add Open Scenes).
///   • Set the sceneName field in the inspector to match exactly.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Tooltip("Exact name of the scene to load (must be added to Build Settings).")]
    public string sceneName;

    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("SceneLoader: No scene name set.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}