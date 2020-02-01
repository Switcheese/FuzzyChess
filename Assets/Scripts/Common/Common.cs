using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 작성일자 : 2020-01-16-PM-4-36
// 작성자   : 배형영
// 간단설명 : 공용 스크립트

public class Common
{
    public const string UI_Path = "UI/";
    public static GameObject ResourceLoad(string path)
    {
        return Resources.Load(path) as GameObject;
    }
}


[System.Serializable]
public class Pool
{
    public int poolID;
    public GameObject poolPrefab;
    public int poolSize;
}

[Flags]
public enum UI_Option
{
   // None = 0,
    Overlay = 1 << 0,        // 이전 ui 위에 덮혀 나온다
    Close = 1 << 1,      // 이전 ui를 닫고 나온다.
    CallBackClose = 1 << 2,  // 이전 ui를 닫고 나온다. 지금 ui가 닫혔을때 다시 이전 ui를 킨다.
    //All = int.MaxValue,
}
public enum UI_Kind
{
    UIWindow_Home,
    UIWindow_Test,
}