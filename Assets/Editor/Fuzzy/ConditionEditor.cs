using UnityEditor;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CustomPropertyDrawer(typeof(Condition))]
    public class ConditionEditor : PropertyDrawer
    {
        const int classWidth = 200;
        const int kindWidth = 100;
        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(pos, label, property);

            SerializedProperty set = property.FindPropertyRelative("fuzzySet");
            SerializedProperty kind = property.FindPropertyRelative("kind");

            EditorGUI.PropertyField(
                new Rect(pos.x, pos.y, classWidth, pos.height),
                set, GUIContent.none);

            EditorGUI.indentLevel = 1;
            var setClass = (FuzzySet)set.objectReferenceValue;// serializedObject.targetObject;
            if (setClass != null)
            {
                int start = setClass.Start;
                int end = setClass.End + 1;

                if (end != 0 && start <= end)
                {
                    string[] display = new string[end - start];
                    System.Array.Copy(kind.enumDisplayNames, start, display, 0, end - start);

                    kind.enumValueIndex = EditorGUI.Popup(new Rect(pos.x + classWidth, pos.y, kindWidth, pos.height), kind.enumValueIndex, display);
                }
                else
                {
                    kind.enumValueIndex = EditorGUI.Popup(new Rect(pos.x + classWidth, pos.y, kindWidth, pos.height), kind.enumValueIndex, kind.enumDisplayNames);
                }
            }

            EditorGUI.EndProperty();
        }
    }


}