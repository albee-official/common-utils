// using UnityEngine;
// using UnityEditor;
// using CommonUtils.StateMachines;

// namespace CommonUtils.Editor.StateMachines
// {
//     using System;
//     using UnityEditor;
//     using UnityEditor.Graphs;

//     [CustomPropertyDrawer(typeof(StateMachine), true)]
//     public class StateMachineDrawer: PropertyDrawer
//     {
//         bool opened = false;

//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             return EditorGUI.GetPropertyHeight(property, true);
//         }

//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
//         {
//             StateMachine obj = (StateMachine)property.managedReferenceValue;

//             if (obj is not null)
//             {
//                 if (obj.CurrentState is null)
//                 {
//                     label.text = $"{property.displayName} :: Null.";
//                 }
//                 else
//                 {
//                     label.text = $"{property.displayName} :: {obj.CurrentState.Name}.";
//                 }
//             }
//             else
//             {
//                 label.text = $"{property.displayName} :: Unavailable in editor.";
//             }

//             EditorGUI.BeginProperty(position, label, property);

//             opened = EditorGUILayout.BeginFoldoutHeaderGroup(opened, label);

//             while (property.NextVisible(true))
//             {
//                 EditorGUILayout.PropertyField(property);
//             }

//             EditorGUILayout.EndFoldoutHeaderGroup();

//             EditorGUI.EndProperty();
//         }
//     }
// }