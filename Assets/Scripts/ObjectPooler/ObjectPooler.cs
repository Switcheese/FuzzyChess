using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-16-PM-2-57
// 작성자   : 배형영
// 간단설명 : 오브젝트 풀링 시스템 최상위 클래스


public class ObjectPooler : MonoBehaviour
{
    // -------------  Variable  ------------- 
    public enum PoolerKind
    {
        EnemyPooler,
        EffectPooler,
    }
    [System.Serializable]
    private struct Poolers
    {
        public PoolerKind kind;
        public GameObject objPooler;
    }
    [SerializeField]
    private List<Poolers> listPoolers;
    private EnumDictionary<PoolerKind, IPooler> dicPoolers;

    // -------------  Property  ------------- 
    public IPooler Enemy
    {
        get { return GetPooler(PoolerKind.EnemyPooler); }
    }
    public IPooler Effect
    {
        get { return GetPooler(PoolerKind.EffectPooler); }
    }


    // -------------  Behaviour  ------------- 
    private void Awake()
    {
        dicPoolers = new EnumDictionary<PoolerKind, IPooler>();

        foreach (Poolers pooler in listPoolers)
        {
            if (!dicPoolers.ContainsKey(pooler.kind))
            {
                var iPooler = pooler.objPooler.GetComponent(typeof(IPooler)) as IPooler;
                if (iPooler != null)
                {
                    dicPoolers.Add(pooler.kind, iPooler);
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogWarning(string.Format($"Can't Find IPooledObject in {pooler.objPooler.name}"));
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning(string.Format($"{pooler.kind.ToString()} is Exist"));
#endif
            }
        }
    }


    // ------------- Private Method  ------------- 



    // ------------- ProtectedMethod  ------------- 



    // -------------  Public Method  ------------- 

    public void InitPoolers()
    {
        foreach (var pooler in dicPoolers.Values)
        {
            pooler.Init();
        }
    }

    public IPooler GetPooler(PoolerKind kind)
    {
        if (dicPoolers.ContainsKey(kind))
        {
            return dicPoolers[kind];
        }
        else
            return null;
    }

}
