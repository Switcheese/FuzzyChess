using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [CreateAssetMenu(fileName = "FuzzySet", menuName = "Fuzzy/FuzzySet")]
    public class FuzzySet : ScriptableObject
    {
        //[SerializeField]
       // private MembershipRange Kind_Start, Kind_End;
        [SerializeField]
        private DF_FuzzySet df_Fuzzy;
        private int end;
        [SerializeField]
        protected FuzzyData[] fuzzyDatas;
        protected EnumDictionary<Membership, AnimationCurve> dicFuzzyData;

        protected int currentValue;

        public int membershipCount
        {
            get { return this.dicFuzzyData.Count; }
        }
        public int Start
        {
            get { return (int)df_Fuzzy; }
        }
        public int End
        {
            get { return (int)end; }
        }

        private void OnEnable()
        {
            if (dicFuzzyData == null && fuzzyDatas != null)
            {
                dicFuzzyData = new EnumDictionary<Membership, AnimationCurve>();
                for (int i = 0; i < this.fuzzyDatas.Length; i++)
                {
                    var key = (Membership)((int)fuzzyDatas[i].kind + (int)df_Fuzzy/*(int)this.Kind_Start*/);
                    if (!dicFuzzyData.ContainsKey(key))
                    {
                        dicFuzzyData.Add(key, fuzzyDatas[i].membership);
                    }
                }
            }
        }


        public void SetValue(int val)
        {
            currentValue = val;
        }

        public float GetEvaluate(Membership kind)
        {
            if (dicFuzzyData.ContainsKey(kind))
            {
                return dicFuzzyData[kind].Evaluate(this.currentValue);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("GetEvaluate ContainKey error RetrunValue == 0");
#endif
                return 0;
            }
        }
        public float GetEvaluate(Membership kind, int val)
        {
            if (dicFuzzyData.ContainsKey(kind))
            {
                currentValue = val;
                return dicFuzzyData[kind].Evaluate(this.currentValue);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("GetEvaluate ContainKey error RetrunValue == 0");
#endif
                return 0;
            }
        }

        public void SetEditorEnum()
        {
            if (fuzzyDatas == null)
                return;
            for (int i = 0; i < fuzzyDatas.Length; i++)
            {
                switch (df_Fuzzy)
                {
                    case DF_FuzzySet.Personality:
                        this.end = (int)Membership.Personality_Distraction;
                        break;
                    case DF_FuzzySet.Fear:
                        this.end = (int)Membership.Fear_Coward;
                        break;
                    case DF_FuzzySet.VisualAcuity:
                        this.end = (int)Membership.VisualAcuity_Good;
                        break;
                }
                fuzzyDatas[i].SetEnum((int)this.df_Fuzzy, this.end + 1);
            }
        }
    }
}