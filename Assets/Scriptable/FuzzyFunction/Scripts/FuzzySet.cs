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
        [SerializeField]
        private FuzzySetKind Kind_Start, Kind_End;
        [SerializeField]
        protected FuzzyData[] fuzzyDatas;
        protected EnumDictionary<MembershipKind, AnimationCurve> dicFuzzyData;

        protected int currentValue;

        public int membershipCount
        {
            get { return this.dicFuzzyData.Count; }
        }
        public int Start
        {
            get { return (int)Kind_Start; }
        }
        public int End
        {
            get { return (int)Kind_End; }
        }

        private void OnEnable()
        {
            if (dicFuzzyData == null && fuzzyDatas != null)
            {
                dicFuzzyData = new EnumDictionary<MembershipKind, AnimationCurve>();
                for (int i = 0; i < this.fuzzyDatas.Length; i++)
                {
                    var key = (MembershipKind)((int)fuzzyDatas[i].kind + (int)this.Kind_Start);
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

        public float GetEvaluate(MembershipKind kind)
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
        public float GetEvaluate(MembershipKind kind, int val)
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

        public void SetEnumStartEnd()
        {
            if (fuzzyDatas == null)
                return;
            for (int i = 0; i < fuzzyDatas.Length; i++)
            {
                fuzzyDatas[i].SetEnum((int)this.Kind_Start, (int)this.Kind_End + 1);
            }
        }
    }
}