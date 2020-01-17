using UnityEditor;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CustomPropertyDrawer(typeof(Rules))]
    public class RuleEditor : PropertyDrawer
    {

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(pos, label, property);
            Rect contentPosition = pos;

            if (property.isExpanded = EditorGUI.Foldout(pos, property.isExpanded, label))
            {
                if (pos.height > 16f)
                {
                    pos.height = 16f;
                    EditorGUI.indentLevel += 1;
                    contentPosition = EditorGUI.IndentedRect(pos);
                    contentPosition.y += 18f;
                }
                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 1;
                EditorGUILayout.BeginVertical();

                EditorGUILayout.PropertyField(property.FindPropertyRelative("v1"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("calculate"), new GUIContent("Calculate"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("v2"));

                SerializedProperty result = property.FindPropertyRelative("result");
                var ruleClass = (FuzzyRule)result.serializedObject.targetObject;// serializedObject.targetObject;
                if (ruleClass != null)
                {
                    int start = ruleClass.Start;
                    int end = ruleClass.End + 1;

                    if (end != 0 && start <= end)
                    {
                        string[] display = new string[end - start];
                        System.Array.Copy(result.enumDisplayNames, start, display, 0, end - start);

                        result.enumValueIndex = EditorGUILayout.Popup(new GUIContent("Target"), result.enumValueIndex, display);
                    }
                    else
                    {
                        result.enumValueIndex = EditorGUILayout.Popup(new GUIContent("Target"), result.enumValueIndex, result.enumDisplayNames);
                    }
                }

                EditorGUI.indentLevel = indent;

                EditorGUILayout.EndVertical();
            }
            EditorGUI.EndProperty();
        }
    }
}