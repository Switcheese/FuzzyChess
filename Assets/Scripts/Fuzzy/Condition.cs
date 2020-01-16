using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :


namespace FuzzyLogic
{
    [System.Serializable]
    public class Condition
    {
        public FuzzySet fuzzySet;
        public Membership kind;

        public float GetEvaluate()
        {
            return fuzzySet.GetEvaluate(kind + fuzzySet.Start);
        }
    }
}