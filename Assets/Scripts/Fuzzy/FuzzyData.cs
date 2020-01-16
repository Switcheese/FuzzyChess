using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :


namespace FuzzyLogic
{
    [System.Serializable]
    public class FuzzyData
    {
        public Membership kind;
        public AnimationCurve membership;

        public int Kind_Start, Kind_End;

        public void SetEnum(int start, int end)
        {
            this.Kind_Start = start;
            this.Kind_End = end;
        }
    }
}