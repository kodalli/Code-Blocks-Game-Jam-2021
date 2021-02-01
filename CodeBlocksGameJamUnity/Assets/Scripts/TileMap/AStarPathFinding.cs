using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding
{

    //private Vector2Int StartLocation;
    //private Vector2Int EndLocation;
    private bool[,] Map;
    private Node endNode, startNode;
    private float width, height;
    private Node[,] nodes;

    // Everything is done by index
    public AStarPathFinding(bool[,] map, Vector2Int start, Vector2Int end) 
    {
        Map = map;
        //StartLocation = start;
        //EndLocation = end;
        width = Map.GetLength(0);
        height = Map.GetLength(1);

        endNode.Location = end;
        endNode.IsWalkable = Map[end.x, end.y];
        endNode.State = NodeState.Untested;

        startNode.Location = start;
        startNode.IsWalkable = Map[start.x, start.y];
        startNode.State = NodeState.Untested;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                nodes[i, j].Location = new Vector2Int(i, j);
                nodes[i, j].IsWalkable = map[i, j];
                nodes[i, j].State = NodeState.Untested;
            }
        }
    }

    public class Node
    {
        public Vector2Int Location { get; set; }
        public bool IsWalkable { get; set; }
        public float Gdist { get; private set; } // start to current
        public float Heuristic { get; private set; } // end to current
        public float Fcost { get { return this.Gdist + this.Heuristic; } } // total cost
        public NodeState State { get; set; }
        public Node ParentNode { get;  set; }
    }

    public enum NodeState { Untested, Open, Closed }

    private bool Search(Node currentNode)
    {
        List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
        nextNodes.Sort((node1, node2) => node1.Fcost.CompareTo(node2.Fcost));
        foreach (var nextNode in nextNodes)
        {
            if (nextNode.Location == endNode.Location)
                return true;
            else if (Search(nextNode)) // Note: Recurses back into Search(Node)
                return true;
        }
        return false;
    }

    private List<Node> GetAdjacentWalkableNodes(Node fromNode)
    {
        List<Node> walkableNodes = new List<Node>();
        IEnumerable<Vector2Int> nextLocations = GetAdjacentLocations(fromNode.Location);

        foreach (var location in nextLocations)
        {
            int x = location.x;
            int y = location.y;

            // Stay within the grid's boundaries
            if (x < 0 || x >= width || y < 0 || y >= height)
                continue;

            Node node = nodes[x, y];
            // Ignore non-walkable nodes
            if (!node.IsWalkable)
                continue;

            // Ignore already-closed nodes
            if (node.State == NodeState.Closed)
                continue;

            // Already-open nodes are only added to the list if their G-value is lower going via this route.
            if (node.State == NodeState.Open)
            {
                float traversalCost = GetTraversalCost(node.Location, fromNode.Location);
                float gTemp = fromNode.Gdist + traversalCost;
                if (gTemp < node.Gdist)
                {
                    node.ParentNode = fromNode;
                    walkableNodes.Add(node);
                }
            }
            else
            {
                // If it's untested, set the parent and flag it as 'Open' for consideration
                node.ParentNode = fromNode;
                node.State = NodeState.Open;
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }

    public List<Vector2Int> FindPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        bool success = Search(startNode);
        if (success)
        {
            Node node = endNode;
            while (node.ParentNode != null)
            {
                path.Add(node.Location);
                node = node.ParentNode;
            }
            path.Reverse();
        }
        return path;
    }

    public virtual float GetTraversalCost(Vector2Int cur, Vector2Int parent) {
        // incur penalities for certain terrain
        return Mathf.Abs(cur.x - parent.x) + Mathf.Abs(cur.y - parent.y);
    }

    private IEnumerable<Vector2Int> GetAdjacentLocations (Vector2Int location)
    {
        // No diagonal tiles 
        List<Vector2Int> neighbors = new List<Vector2Int> {
            new Vector2Int(location.x, location.y + 1),
            new Vector2Int(location.x, location.y - 1),
            new Vector2Int(location.x - 1, location.y),
            new Vector2Int(location.x + 1, location.y)
        };

        

        return neighbors;
    }
}
