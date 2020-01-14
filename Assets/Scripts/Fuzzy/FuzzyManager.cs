using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    public class FuzzyManager : SingletonMono<FuzzyManager>
    {
        // Variable
        #region Variable
        [SerializeField]
        private FuzzyFunction[] fuzzyFunctions;
        [SerializeField]
        private FuzzyRuleFunction[] fuzzyRules;

        private EnumDictionary<FuzzyFunctionKind, FuzzySet> dicFuzzyFunctions;
        private EnumDictionary<FuzzyRuleKind, FuzzyRule> dicFuzzyRules;
        #endregion

        // Property
        #region Property

        #endregion

        // MonoBehaviour
        #region MonoBehaviour
        private void Awake()
        {
            dicFuzzyFunctions = new EnumDictionary<FuzzyFunctionKind, FuzzySet>();
            dicFuzzyRules = new EnumDictionary<FuzzyRuleKind, FuzzyRule>();

            for (int i = 0; i < fuzzyFunctions.Length; i++)
            {
                if (!dicFuzzyFunctions.ContainsKey(fuzzyFunctions[i].kind))
                {
                    dicFuzzyFunctions.Add(fuzzyFunctions[i].kind, fuzzyFunctions[i].fuzzySet);
                }
            }
            for (int i = 0; i < fuzzyRules.Length; i++)
            {
                if (!dicFuzzyRules.ContainsKey(fuzzyRules[i].kind))
                {
                    dicFuzzyRules.Add(fuzzyRules[i].kind, fuzzyRules[i].fuzzyRule);
                }
            }
        }
        #endregion

        // Private Method
        #region Private Method

        #endregion

        // Public Method
        #region Public Method

        public float GetRuleWeight(FuzzyRuleKind ruleKind)
        {
            if (dicFuzzyRules.ContainsKey(ruleKind))
            {
                return dicFuzzyRules[ruleKind].GetWeight(ruleKind);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Dictionary Key가 존재하지 않습니다.");
#endif
                return 0f;
            }
        }

        public void SetFuzzyValue(FuzzyFunctionKind functionKind, int value)
        {
            if (dicFuzzyFunctions.ContainsKey(functionKind))
            {
                dicFuzzyFunctions[functionKind].SetValue(value);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Dictionary Key가 존재하지 않습니다.");
#endif
            }
        }


        public static float AND(float v1, float v2)
        {
            if (v1 == v2)
                return v1;
            else if (v1 > v2)
                return v2;
            else
                return v1;
        }
        public static float OR(float v1, float v2)
        {
            if (v1 == v2)
                return v1;
            else if (v1 > v2)
                return v1;
            else
                return v2;
        }
        #endregion
    }
}