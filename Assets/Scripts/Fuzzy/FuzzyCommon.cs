using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 
// 작성자   : 배형영 
// 간단설명 :
/*
    퍼지 추가시 규칙
    1. 조건에 쓸 퍼지를 DF_FuzzySet 에 선언한다.
    2. 규칙에 쓸 퍼지를 DF_FuzzyRule 에 선언한다.
    3. 각 조건과 규칙에 쓸 함수 (Animation Curve 이름)을 Membership에 선언한다.
    4. 각 조건과 규칙의 MemberShip의 시작과 끝값을 MemberShipRange에 선언한다.
    5. FuzzyRule 스크립트의 GetWeight 메소드의 Switch Case에 각 규칙의 멤버쉽enum을 추가시켜준다.
     */

namespace FuzzyLogic
{
    public class FuzzyCommon
    {
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
    }

    [System.Serializable]
    public struct FuzzyFunction
    {
        public DF_FuzzySet kind;
        public FuzzySet fuzzySet;
    }
    [System.Serializable]
    public struct FuzzyRuleFunction
    {
        public DF_FuzzyRule kind;
        public FuzzyRule fuzzyRule;
    }

    /// <summary>
    /// 조건에 쓸 퍼지 선언
    /// </summary>
    public enum DF_FuzzySet
    {
        Personality,
        Fear,
        VisualAcuity,

    }
    /// <summary>
    /// 규칙에 쓸 퍼지 선언
    /// </summary>
    public enum DF_FuzzyRule
    {
        VisualField_Width,
        VisualField_Distance,
    }

    /// <summary>
    /// 퍼지 멤버쉽함수 선언
    /// </summary>
    public enum Membership
    {
        // Personality 성격
        Personality_Strict,          // 꼼꼼한
        Personality_Usually,         // 보통
        Personality_Distraction,     // 산만

        // Fear 겁
        Fear_Brave,       // 용감
        Fear_Usually,     // 평소
        Fear_Coward,      // 겁쟁이

        // VisualAcuity 시력
        VisualAcuity_Bad,       // 나쁨
        VisualAcuity_Good,      // 좋음




        // VisualField Width 시야 너비 규칙
        VisualField_Width_Narrow,       // 좁다
        VisualField_Width_Usually,     // 보통
        VisualField_Width_Wide,         // 넓다

        // VisualField Distance 시야 거리 규칙
        VisualField_Distance_Close,       // 가깝다
        VisualField_Distance_Usually,     // 보통
        VisualField_Distance_Far,         // 멀다

    }
    /// <summary>
    /// 퍼지 멤버십 함수 범위 선언
    /// </summary>
    public enum MembershipRange
    {
        ST_Personality = Membership.Personality_Strict,
        ED_Personality = Membership.Personality_Distraction,

        ST_Fear = Membership.Fear_Brave,
        ED_Fear = Membership.Fear_Coward,

        ST_VisualAcuity = Membership.VisualAcuity_Bad,
        ED_VisualAcuity = Membership.VisualAcuity_Good,

        ST_VisualField_Width = Membership.VisualField_Width_Narrow,
        ED_VisualField_Width = Membership.VisualField_Width_Wide,

        ST_VisualField_Distance = Membership.VisualField_Distance_Close,
        ED_VisualField_Distance = Membership.VisualField_Distance_Far,
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