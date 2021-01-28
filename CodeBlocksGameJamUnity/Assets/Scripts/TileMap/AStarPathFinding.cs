using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding: MonoBehaviour
{
    Node endNode, startNode;
    float width, height;
    Node[,] nodes;
    public class SearchParameters
    {
        public Vector2Int StartLocation { get; set; }
        public Vector2Int EndLocation { get; set; }
        public bool[,] Map { get; set; }
    }

    public class Node
    {
        public Vector2Int Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float gdist { get; private set; } // make setter function
        public float heuristic { get; private set; } // make setter function
        public float fcost { get { return this.gdist + this.heuristic; } }
        public NodeState State { get; set; }
        public Node ParentNode { get;  set; }

    }

    public enum NodeState { Untested, Open, Closed }

    private bool Search(Node currentNode)
    {
        List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
        nextNodes.Sort((node1, node2) => node1.fcost.CompareTo(node2.fcost));
        foreach (var nextNode in nextNodes)
        {
            if (nextNode.Location == this.endNode.Location)
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
            if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                continue;

            Node node = this.nodes[x, y];
            // Ignore non-walkable nodes
            if (!node.IsWalkable)
                continue;

            // Ignore already-closed nodes
            if (node.State == NodeState.Closed)
                continue;

            // Already-open nodes are only added to the list if their G-value is lower going via this route.
            if (node.State == NodeState.Open)
            {
                float traversalCost = GetTraversalCost(node.Location, node.ParentNode.Location);
                float gTemp = fromNode.gdist + traversalCost;
                if (gTemp < node.gdist)
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
            Node node = this.endNode;
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
        return 0f; 
    }

    public virtual IEnumerable<Vector2Int> GetAdjacentLocations (Vector2Int location)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        return neighbors;
    }
}
