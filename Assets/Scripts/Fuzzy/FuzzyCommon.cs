using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : #DATE#
// 작성자   : #AUTHOR#
// 간단설명 :

namespace FuzzyLogic
{
    public class FuzzyCommon
    {
    }

    [System.Serializable]
    public struct FuzzyFunction
    {
        public FuzzyFunctionKind kind;
        public FuzzySet fuzzySet;
    }
    [System.Serializable]
    public struct FuzzyRuleFunction
    {
        public FuzzyRuleKind kind;
        public FuzzyRule fuzzyRule;
    }

    /// <summary>
    /// 조건에 쓸 퍼지 함수 종류 선언
    /// </summary>
    public enum FuzzyFunctionKind
    {
        Tendency,
        Feeling,
    }
    /// <summary>
    /// 규칙에 쓸 퍼지 함수 종류 선언
    /// </summary>
    public enum FuzzyRuleKind
    {
        IgnoreGuardAttack
    }

    /// <summary>
    /// 퍼지 멤버쉽함수 종류선언
    /// </summary>
    public enum MembershipKind
    {
        Goodness,
        Normal,
        Evil,


        Fine,
        Angry,


        Ignore,
        Guard,
        Attack,

    }
    /// <summary>
    /// 퍼지 멤버십 함수 범위 설정 선언
    /// </summary>
    public enum FuzzySetKind
    {
        Tendency_Start = MembershipKind.Goodness,
        Tendency_End = MembershipKind.Evil,

        Feeling_Start = MembershipKind.Fine,
        Feeling_End = MembershipKind.Angry,

        AttackRule_Start = MembershipKind.Ignore,
        AttackRule_End = MembershipKind.Attack,
    }

    /// <summary>
    /// 규칙 연산 설정
    /// </summary>
    public enum Calculate
    {
        AND,
        OR,
    }
}