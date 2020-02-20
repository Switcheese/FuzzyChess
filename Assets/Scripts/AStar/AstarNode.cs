using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-20-PM-4-45
// 작성자   : 김세중
// 간단설명 :

public class AstarNode
{
    // Variable
    #region Variable
    [SerializeField]
    private bool m_walkable;
    [SerializeField]
    private Vector3 m_worldPosition;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    public AstarNode Parent;
    #endregion

    // Property
    #region Property
    public bool walkable
    {
        get => m_walkable;
        set => m_walkable = value;
    }
    public Vector3 worldPosition
    {
        get => m_worldPosition;
        set => m_worldPosition = value;
    }
    public int fCost
    {
        get => gCost + hCost;
    }
    #endregion

    // MonoBehaviour
    #region MonoBehaviour

    #endregion

    // Private Method
    #region Private Method

    #endregion

    // Public Method
    #region Public Method
    public AstarNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {                                                       
        m_walkable = _walkable;
        m_worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }


    #endregion
}
