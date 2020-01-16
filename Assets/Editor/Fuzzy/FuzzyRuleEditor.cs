using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CustomEditor(typeof(FuzzyRule))]
    public class FuzzyRuleEditor : Editor
    {
        SerializedProperty ruleProp;
        FuzzyRule fuzzyRule;
        GUIStyle style;

        Object condition1, condition2;

        void OnEnable()
        {
            style = new GUIStyle();
            style.fontSize = 15;
            style.alignment = TextAnchor.MiddleCenter;

            ruleProp = serializedObject.FindProperty("rules");
            fuzzyRule = (FuzzyRule)serializedObject.targetObject;
        }
        //Inspector 동작 함수
        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update();
            GUI.enabled = false;
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true);
            GUI.enabled = true;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("규칙퍼지의 Enum값 범위를 지정해주세요.", style);
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Kind_Start"), new GUIContent("시작 ST"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Kind_End"), new GUIContent("끝 ED"));
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

            EditorGUILayout.LabelField("규칙퍼지를 제작해주세요.", style);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fuzzyDatas"), true);
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

            EditorGUILayout.LabelField("규칙을 제작해주세요.", style);
            EditorGUILayout.Space();
            fuzzyRule.SetEnumStartEnd();
            DrawList(ruleProp, "Rule Count", "Rule Define");

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        void DrawList(SerializedProperty _list, string _fieldName, string _labalName)
        {
            //리스트 갯수 표시
            EditorGUILayout.PropertyField(_list.FindPropertyRelative("Array.size"), new GUIContent(_fieldName));
            EditorGUILayout.Space();

            bool flag1 = false;
            bool flag2 = false;

            int Count = _list.arraySize;
            for (int i = 0; i < Count; ++i)
            {
                // 타이틀
                EditorGUILayout.PropertyField(_list.GetArrayElementAtIndex(i), new GUIContent(string.Format($"{_labalName} {i + 1}")));
                SerializedProperty prop = _list.GetArrayElementAtIndex(i);

                SerializedProperty v1 = prop.FindPropertyRelative("v1");
                SerializedProperty set1 = v1.FindPropertyRelative("fuzzySet");
                if (condition1 == null)
                {
                    if (set1 != null)
                    {
                        condition1 = set1.objectReferenceValue;
                    }
                }
                else
                {
                    if (set1.objectReferenceValue != null && set1.objectReferenceValue != condition1)
                    {
                        if (!flag1)
                        {
                            condition1 = set1.objectReferenceValue;
                            flag1 = true;
                        }
                    }
                    set1.objectReferenceValue = condition1;
                }

                SerializedProperty v2 = prop.FindPropertyRelative("v2");
                SerializedProperty set2 = v2.FindPropertyRelative("fuzzySet");
                if (condition2 == null)
                {
                    if (set2 != null)
                    {
                        condition2 = set2.objectReferenceValue;
                    }
                }
                else
                {
                    if (set2.objectReferenceValue != null && set2.objectReferenceValue != condition2)
                    {
                        if (!flag2)
                        {
                            condition2 = set2.objectReferenceValue;
                            flag2 = true;
                        }
                    }
                    set2.objectReferenceValue = condition2;
                }
            }
            flag1 = false;
            flag2 = false;
        }
        //public override void OnInspectorGUI()
        //{
        //    base.OnInspectorGUI();
        //    serializedObject.Update();
        //    var set = (FuzzyRule)serializedObject.targetObject;
        //    set.SetEnumStartEnd();
        //    serializedObject.ApplyModifiedProperties();

        //}
    }
}