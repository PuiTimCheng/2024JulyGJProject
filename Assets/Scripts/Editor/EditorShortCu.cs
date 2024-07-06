using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public static class EditorShortCut
    {
        [MenuItem("Tools/快速启动 %w")]
        public static void LoadMainMenuScene()
        {
            const string scenePath = "Assets/Scenes/Menu.unity";
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}