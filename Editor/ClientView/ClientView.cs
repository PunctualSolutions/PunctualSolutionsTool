using System;
using System.IO;
using System.IO.Compression;
using PunctualSolutions.Samples;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PunctualSolutions.Tool.ClientView.Editor
{
    public static class ClientView
    {
        const string MenuName = "Assets/Punctual Solutions/Generate Client View";

        [MenuItem(MenuName)]
        public static void GenerateClientView()
        {
            Debug.Log("Generating Client View Start");
            EditorUtility.DisplayProgressBar("Generating Client View", "Creating build...", 0);
            //加载当前选中场景
            var scene = EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(Selection.activeObject));
            //获取场景中Main Camera
            var mainCamera = GameObject.FindWithTag("MainCamera");
            if (!mainCamera)
            {
                Debug.LogError("Cannot find MainCamera in the current scene");
                return;
            }

            //查询是否存在FreeCamera
            var        freeCamera          = mainCamera.GetComponent<FreeCamera>();
            FreeCamera addFreeCamera       = null;
            if (!freeCamera) addFreeCamera = mainCamera.AddComponent<FreeCamera>();
            EditorSceneManager.SaveScene(scene);
            //打包程序
            var buildPath = $"Builds/{Application.productName}_{scene.name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
            var buildPlayerOptions = new BuildPlayerOptions
                                     {
                                         scenes = new[] { scene.path, },
                                         locationPathName =
                                             Path.Combine(
                                                 buildPath, Application.productName + ".exe"),
                                         target = BuildTarget.StandaloneWindows64,
                                         options = BuildOptions.ShowBuiltPlayer |
                                                   BuildOptions.CompressWithLz4HC,
                                     };
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            EditorUtility.DisplayProgressBar("Generating Client View", "Compressing build...", 0.5f);
            if (addFreeCamera) Object.Destroy(addFreeCamera);
            EditorSceneManager.SaveScene(scene);
            var startPath = Path.Combine(Application.dataPath, "..", buildPath);
            var zipPath   = Path.Combine(Application.dataPath, "..", buildPath + ".zip");
            ZipFile.CreateFromDirectory(startPath, zipPath);
            EditorUtility.DisplayProgressBar("Generating Client View", "Cleaning up...", 1);
            EditorUtility.ClearProgressBar();
            Debug.Log("Generating Client View End");
        }

        [MenuItem(MenuName, true)]
        public static bool ValidateGenerateClientView() => Selection.activeObject is SceneAsset;
    }
}