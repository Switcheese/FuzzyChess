using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2020-01-20-PM-4-42
// 작성자   : 김세중
// 간단설명 : Astar Grid

public class Grid : MonoBehaviour
{
    // Variable
    #region Variable

    public Transform player;
    public List<AstarNode> path;

    /// <summary>
    /// unWalkable Layer
    /// </summary>
    public LayerMask unWalkableMask;
    /// <summary>
    /// World Grid 크기
    /// </summary>
    public Vector2 gridWorldSize;
    /// <summary>
    /// Node의 지름(크기)
    /// </summary>
    public float nodeRadius;
    /// <summary>
    /// 빈공간 크기
    /// </summary>
    public float EmptySpace;
    /// <summary>
    /// 노드 실제 지름
    /// </summary>
    float nodeDiameter;
    AstarNode[,] grid;
    int gridSizeX, gridSizeY;
    #endregion

    // Property
    #region Property

    #endregion

    // MonoBehaviour
    #region MonoBehaviour
    private void Start()
    {
        //노드 실제 크기 설정
        nodeDiameter = nodeRadius * 2;

        //Gird 의 x크기 = Grid x의 갯수 / 노드의 실제 크기
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        //Gird 의 Y크기 = Grid Y의 갯수 / 노드의 실제 크기
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        
        //그리드 현황 업데이트
        CreateGrid();
    }

    #endregion

    // Private Method
    #region Private Method
    private void OnDrawGizmos()
    {
        //최대 그리드 크기를 그려줌
        Gizmos.DrawWireCube(this.transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            //AstarNode playerNode = NodeFromWorldPoint(player.position);
            foreach (AstarNode node in grid)
            {
                //걷기 가능여부에 따라 흰색, 빨간색 구분
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(node))
                        Gizmos.color = Color.black;
                }
                //if (playerNode == node)
                //{
                //    Gizmos.color = Color.cyan;
                //}
                //그리드 큐브 
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter- EmptySpace));
            }
        }
    }
    void CreateGrid()
    {
        grid = new AstarNode[gridSizeX, gridSizeY];
        //방향벡터를 뺌으로 왼쪽 기준 좌표와 맨 아래 좌표를 따냄
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //노드의 월드 기준 좌표 구하기
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                //해당 노드의 상태 구함
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));
                //그리드에 덮어 씌움
                grid[x, y] = new AstarNode(walkable, worldPoint,x,y);
            }
        }
    }
    #endregion

    // Public Method
    #region Public Method
    public List<AstarNode> GetNeighbors(AstarNode _Node)
    {
        List<AstarNode> neighbours = new List<AstarNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = _Node.gridX + x;
                int checkY = _Node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
                
            }
        }
        return neighbours;
    }
    public AstarNode NodeFromWorldPoint(Vector3 _worldposition)
    {
        float percentX = (_worldposition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (_worldposition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    #endregion
}
