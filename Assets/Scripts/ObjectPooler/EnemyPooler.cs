using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-16-PM-4-43
// 작성자   : 배형영
// 간단설명 :

public class EnemyPooler : MonoBehaviour, IPooler
{
    // -------------  Variable  ------------- 

    [SerializeField]
    private List<Pool> listPools;
    private Dictionary<int, Queue<GameObject>> dicPool;

    // -------------  Property  ------------- 



    // -------------  Behaviour  ------------- 



    // ------------- Private Method  ------------- 



    // ------------- ProtectedMethod  ------------- 



    // -------------  Public Method  ------------- 


    public void Init()
    {
        this.dicPool = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in listPools)
        {
            if (!dicPool.ContainsKey(pool.poolID))
            {
                Queue<GameObject> objPool = new Queue<GameObject>();

                for (int i = 0; i < pool.poolSize; i++)
                {
                    var obj = Instantiate(pool.poolPrefab,this.transform);
                    obj.SetActive(false);
                    objPool.Enqueue(obj);
                }

                dicPool.Add(pool.poolID, objPool);
            }
        }
    }

    public GameObject SpawnObject(int id, Vector3 position, Quaternion rotation)
    {
        if (!dicPool.ContainsKey(id))
        {
#if UNITY_EDITOR
            Debug.LogWarning(string.Format($"Not Found ID : {id} in Pooler"));
#endif
            return null;
        }

        // 나중에 수정해야할수도 있음 풀링할게 모자라면 새로만들어서 enqueue하는걸로
        GameObject spawnObject = dicPool[id].Dequeue();

        spawnObject.SetActive(true);
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;

        dicPool[id].Enqueue(spawnObject);

        return spawnObject;
    }

    public void DestroyPool()
    {
        foreach (var objPool in dicPool.Values)
        {
            for (int i = 0; i < objPool.Count; i++)
            {
                Destroy(objPool.Dequeue());
            }
            objPool.Clear();
        }
        dicPool.Clear();
        dicPool = null;
    }

}
