using UnityEngine;
using CommonUtils.Debug;

namespace CommonUtils.Inspector
{
    #if UNITY_EDITOR

    using UnityEditor;

    public class InspectorUtilities : EditorWindow 
    {
        [MenuItem("Common Utils/Check Asset Type", false, 0)]
        public static void CheckAssetType(MenuCommand command) {
            string[] assetGUIDS = Selection.assetGUIDs;
            foreach (var guid in assetGUIDS)
            {
                string selectedAnimationClipPath = AssetDatabase.GUIDToAssetPath(guid);
                foreach (var item in AssetDatabase.LoadAllAssetsAtPath(selectedAnimationClipPath))
                {
                    GameConsole.Log("Asset at \"" + selectedAnimationClipPath + "\" has asset of type: " + item.GetType().ToString());
                }
            }
        }
    }

    #endif
}