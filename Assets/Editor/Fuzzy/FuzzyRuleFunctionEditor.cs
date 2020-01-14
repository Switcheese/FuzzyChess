using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CustomPropertyDrawer(typeof(FuzzyRuleFunction))]
    public class FuzzyRuleFunctionEditor : PropertyDrawer
    {
        const int kindWidth = 200;
        const int objectWidth = 200;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.BeginProperty(pos, label, prop);

            SerializedProperty kind = prop.FindPropertyRelative("kind");
            SerializedProperty fuzzyRule = prop.FindPropertyRelative("fuzzyRule");

            EditorGUI.PropertyField(
                new Rect(pos.x, pos.y, kindWidth, pos.height),
                kind, GUIContent.none);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;
            EditorGUI.PropertyField(
                new Rect(pos.x + kindWidth, pos.y, objectWidth, pos.height),
                fuzzyRule, GUIContent.none);
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

    }
}