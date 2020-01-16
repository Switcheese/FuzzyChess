using UnityEngine;
using UnityEditor;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{

    [CustomPropertyDrawer(typeof(FuzzyData))]
    public class FuzzyDataEditor : PropertyDrawer
    {
        const int kindWidth = 250;
        const int CurveWidth = 200;
        private string[] display;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.BeginProperty(pos, label, prop);
            SerializedProperty kind = prop.FindPropertyRelative("kind");
            SerializedProperty curve = prop.FindPropertyRelative("membership");

            SerializedProperty start = prop.FindPropertyRelative("Kind_Start");
            SerializedProperty end = prop.FindPropertyRelative("Kind_End");

            if (end.intValue != 0 && start.intValue <= end.intValue)
            {
                display = new string[end.intValue - start.intValue];
                System.Array.Copy(kind.enumDisplayNames, start.intValue, display, 0, end.intValue - start.intValue);

                kind.enumValueIndex = EditorGUI.Popup(new Rect(pos.x, pos.y, kindWidth, pos.height), kind.enumValueIndex, display);
            }
            else
            {
                kind.enumValueIndex = EditorGUI.Popup(new Rect(pos.x, pos.y, kindWidth, pos.height), kind.enumValueIndex, kind.enumDisplayNames);
            }
            // Draw curve
            //int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;
            EditorGUI.PropertyField(
                new Rect(pos.x + kindWidth, pos.y, CurveWidth, pos.height),
                curve, GUIContent.none);
            //EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }


}