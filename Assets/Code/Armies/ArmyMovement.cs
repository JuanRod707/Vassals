using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ArmyMovement : MonoBehaviour
{
    public float TileWait;
    public float MoveSpeed;
    public float MinDistanceToTile;
    public TileInfo CurrentTile;

    private NavMapGen NavMap;
    private float tileWaitElapsed = 0f;
    private List<NavNode> path;
    private NavNode nextNode;
    private NavNode[,] graph;
    private bool IsMoving = false;
    private bool IsResting = false;

    private void Start()
    {
        NavMap = GameObject.Find("NavMap").GetComponent<NavMapGen>();
        graph = NavMap.Graph;
        UpdateCurrentTile();
    }

    private void Update()
    {
        if (IsMoving)
        {
            Move();
            if (Vector3.Distance(this.transform.position, nextNode.transform.position) < MinDistanceToTile)
            {
                FinishSegment();
            }
        }

        if (IsResting)
        {
            Rest();
        }
    }

    private void UpdateCurrentTile()
    {
        CurrentTile = FindCurrentTile();
    }

    private void FinishSegment()
    {
        this.transform.position = nextNode.transform.position;
        UpdateCurrentTile();
        path.Remove(nextNode);
        IsMoving = false;

        if (path.Any())
        {
            IsResting = true;
            nextNode = path.First();
        }
        else
        {
            IsResting = false;
            nextNode = null;
        }
    }

    private void Move()
    {
        var vect = (nextNode.transform.position - this.transform.position).normalized;
        this.transform.Translate(vect * MoveSpeed);
    }

    private void Rest()
    {
        tileWaitElapsed += Time.deltaTime;
        if (tileWaitElapsed > TileWait)
        {
            IsResting = false;
            IsMoving = true;
            tileWaitElapsed = 0f;
        }
    }

    private TileInfo FindCurrentTile()
    {
        //var ray = new Ray(this.transform.position - Vector3.up, Vector3.down);
        var raycast = new RaycastHit();
        if (Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, out raycast))
        {
            if (raycast.collider.tag.Equals("Tile"))
            {
                Debug.Log("Found tile");
                return raycast.collider.GetComponent<TileInfo>();
            }
        }

        return null;
    }

    public void SetTarget(Coordinate coord)
    {
        var source = NavMap.GetNavNode(CurrentTile.Coord);
        var target = NavMap.GetNavNode(coord);
        path = Pathfinder.FindBestPath(source, target, graph).ToList();
        nextNode = path.First();
        IsMoving = true;
    }


}
