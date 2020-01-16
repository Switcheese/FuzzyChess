using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    [System.Serializable]
    public class Rules
    {
        public Condition v1;
        public Calculate calculate;
        public Condition v2;
        public Membership result;

        public float ExcuteCalculate()
        {
            float resultVal = 0f;
            switch (calculate)
            {
                case Calculate.AND:
                    resultVal = FuzzyCommon.AND(v1.GetEvaluate(), v2.GetEvaluate());
                    break;
                case Calculate.OR:
                    resultVal = FuzzyCommon.OR(v1.GetEvaluate(), v2.GetEvaluate());
                    break;
            }
            return resultVal;
        }

    }
}