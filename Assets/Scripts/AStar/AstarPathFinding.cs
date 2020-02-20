using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-29-PM-4-56
// 작성자   : 김세중
// 간단설명 :

public class AstarPathFinding : MonoBehaviour
{
    // Variable
    #region Variable
    Grid grid;
    public Transform Seeker, Target;
    #endregion

    // Property
    #region Property

    #endregion

    // MonoBehaviour
    #region MonoBehaviour
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Update()
    {
        FindPath(Seeker.position, Target.position);
    }
    #endregion

    // Private Method
    #region Private Method
    /// <summary>
    /// 길찾기 메소드
    /// 1. 시작점과 끝점을 알아낸다
    /// </summary>
    /// <param name="_startPos">Start Position</param>
    /// <param name="_targetPos">End Position</param>
    void FindPath(Vector3 _startPos, Vector3 _targetPos)
    {
        //시작노드
        AstarNode StartNode = grid.NodeFromWorldPoint(_startPos);
        //목표노드
        AstarNode targetNode = grid.NodeFromWorldPoint(_targetPos);

        List<AstarNode> openSet = new List<AstarNode>();
        HashSet<AstarNode> closedSet = new HashSet<AstarNode>();
        openSet.Add(StartNode);

        while (openSet.Count > 0)
        {
            AstarNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                //if (openSet[i].fCost < currentNode.fCost)
                //노드 값중 F가 가장 작고 F 가 같다면 H
                if (openSet[i].fCost < currentNode.fCost ||
                    openSet[i].fCost == currentNode.fCost&&
                    openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            // List중에 Open -> Close로 옮기기
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(StartNode, targetNode);
                return;
            }
            foreach (AstarNode neighbour in grid.GetNeighbors(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(AstarNode _StartNode, AstarNode _EndNode)
    {
        List<AstarNode> path = new List<AstarNode>();
        AstarNode CurrnetNode = _EndNode;

        while (CurrnetNode != _StartNode)
        {
            path.Add(CurrnetNode);
            CurrnetNode = CurrnetNode.Parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(AstarNode nodeA, AstarNode nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceY + 10 * (distanceY - distanceX);
    }

    #endregion

    // Public Method
    #region Public Method

    #endregion
}
