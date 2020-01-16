using UnityEngine;
using UnityEditor;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CustomEditor(typeof(FuzzySet))]
    public class FuzzySetEditor : Editor
    {
        FuzzySet fuzzySet;
        GUIStyle style;

        private void OnEnable()
        {
            fuzzySet = (FuzzySet)serializedObject.targetObject;
            style = new GUIStyle();
            style.fontSize = 15;
            style.alignment = TextAnchor.MiddleCenter;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true);
            GUI.enabled = true;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("퍼지의 Enum값 범위를 지정해주세요.", style);
            EditorGUILayout.Space(); EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Kind_Start"), new GUIContent("시작 ST"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Kind_End"), new GUIContent("끝 ED"));
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

            fuzzySet.SetEnumStartEnd();

            EditorGUILayout.LabelField("퍼지를 제작해주세요.", style);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fuzzyDatas"), true);
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();


            serializedObject.ApplyModifiedProperties();

        }
    }
}