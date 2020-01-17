using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyLogic;

// 작성일자 : 2020-01-16-PM-12-52
// 작성자   : 배형영
// 간단설명 :

public class Manager : SingletonMono<Manager>
{
    // -------------  Variable  ------------- 
    [SerializeField]
    private ObjectPooler objectPooler = null;
    

    // -------------  Property  ------------- 
    public FuzzyManager Fuzzy
    {
        get { return FuzzyManager.Instance; }
    }
    public ObjectPooler Pooler
    {
        get { return objectPooler; }
    }

    // -------------  Behaviour  ------------- 



    // ------------- Private Method  ------------- 



    // ------------- ProtectedMethod  ------------- 



    // -------------  Public Method  ------------- 



}
