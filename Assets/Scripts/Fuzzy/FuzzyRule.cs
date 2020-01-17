using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :


namespace FuzzyLogic
{
    [CreateAssetMenu(fileName = "FuzzyRule", menuName = "Fuzzy/FuzzyRule")]
    public class FuzzyRule : ScriptableObject
    {
        //[SerializeField]
        //private MembershipRange Kind_Start, Kind_End;
        [SerializeField]
        private DF_FuzzyRule df_FuzzyRule;
        private int end;
        [SerializeField]
        protected FuzzyData[] fuzzyDatas;
        protected EnumDictionary<Membership, AnimationCurve> dicFuzzyData;

        protected int currentValue;

        public Rules[] rules;

        public int membershipCount
        {
            get { return this.dicFuzzyData.Count; }
        }
        public int Start
        {
            get { return (int)df_FuzzyRule; }
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
                    var key = (Membership)((int)fuzzyDatas[i].kind + (int)df_FuzzyRule);
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
        /// <summary>
        /// 해당 규칙의 무게중심을 계산한다.
        /// </summary>
        /// <param name="ruleKind"></param>
        /// <returns></returns>
        public float GetWeight(DF_FuzzyRule ruleKind)
        {
            List<int> memKind = new List<int>(membershipCount);

            float cardinal = 0f;
            float ordinal = 0f;

            // 추가시켜줘야 하는부분
            switch (ruleKind)
            {
                case DF_FuzzyRule.VisualField_Width:
                    {
                        memKind.AddRange(new int[] {
                            (int)Membership.VisualField_Width_Narrow,
                            (int)Membership.VisualField_Width_Usually,
                            (int)Membership.VisualField_Width_Wide });
                    }
                    break;
                case DF_FuzzyRule.VisualField_Distance:
                    {
                        memKind.AddRange(new int[] {
                            (int)Membership.VisualField_Distance_Close,
                            (int)Membership.VisualField_Distance_Usually,
                            (int)Membership.VisualField_Distance_Far });
                    }
                    break;
            }

            for (int i = 0; i < memKind.Count; i++)
            {
                var aver = GetKeyMaxAverage((Membership)memKind[i]);
                var rel = GetReliability((Membership)memKind[i]);
                cardinal += (aver * rel);
                ordinal += rel;
            }

            return cardinal / ordinal;

        }


        private float GetKeyMaxAverage(Membership kind)
        {
            if (dicFuzzyData.ContainsKey(kind))
            {
                int count = 0;
                float sum = 0f;
                foreach (Keyframe key in dicFuzzyData[kind].keys)
                {
                    if (key.value == 1f)
                    {
                        sum += key.time;
                        count++;
                    }
                }
                if (count == 1)
                    return sum;
                else
                    return sum / dicFuzzyData[kind].keys.Length;
            }
#if UNITY_EDITOR
            Debug.LogWarning("최대값들의 평균이 0입니다.");
#endif
            return 0f;
        }

        /// <summary>
        /// 신뢰도 구하기
        /// </summary>
        /// <param name="kind">구하고 싶은 퍼지</param>
        /// <returns></returns>
        private float GetReliability(Membership kind)
        {
            float resultVal = 0f;
            for (int i = 0; i < rules.Length; i++)
            {
                var rstKind = (Membership)((int)rules[i].result + (int)this.df_FuzzyRule);
                if (rstKind == kind)
                {
                    resultVal = FuzzyCommon.OR(resultVal, rules[i].ExcuteCalculate());
                }
            }
            return resultVal;
        }


        public void SetEditorEnum()
        {
            if (fuzzyDatas == null)
                return;
            for (int i = 0; i < fuzzyDatas.Length; i++)
            {
                switch (df_FuzzyRule)
                {
                    case DF_FuzzyRule.VisualField_Width:
                        this.end = (int)Membership.VisualField_Width_Wide;
                        break;
                    case DF_FuzzyRule.VisualField_Distance:
                        this.end = (int)Membership.VisualField_Distance_Far;
                        break;
                }
                fuzzyDatas[i].SetEnum((int)this.df_FuzzyRule, this.end + 1);
            }
        }

    }
}