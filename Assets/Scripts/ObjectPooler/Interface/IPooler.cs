using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-16-PM-4-44
// 작성자   : 배형영
// 간단설명 : 오브젝트 풀러가 가져야하는 인터페이스

public interface IPooler 
{
    
    void Init();
    GameObject SpawnObject(int id, Vector3 position, Quaternion rotation);
    void DestroyPool();

}
