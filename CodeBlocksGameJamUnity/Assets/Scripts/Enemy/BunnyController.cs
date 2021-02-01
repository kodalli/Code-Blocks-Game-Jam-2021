using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class BunnyController : MonoBehaviour
{
    private bool facingRight = false;
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 movement;
    public Vector2 player;
    private Rigidbody2D rb;
    [SerializeField] private GameObject itemDrop;
    [SerializeField] private int health = 100;
    [SerializeField] bool enablePathFinding;
    [SerializeField] GameObject walkableTiles;
    private Tilemap tilemap;
    private List<Vector2Int> pathToWalk = new List<Vector2Int>();
    private bool walking = false;
    [SerializeField] private GameObject pivot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tilemap = walkableTiles.GetComponent<Tilemap>();
        tilemap.CompressBounds();
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        movement = (player - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    void FixedUpdate()
    {
        if (enablePathFinding && !walking)
        {
            //pathToWalk.Clear();
            pathToWalk = FindPathToGoal(player, tilemap);
            pathToWalk.ForEach(item => Debug.Log(item));
            //StartCoroutine(WalkPath(pathToWalk));
            walking = true;
        } else if (!enablePathFinding)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        if (movement.x > 0 && !facingRight)
            Flip();
        else if (movement.x < 0 && facingRight)
            Flip();

        if (walking)
        {
            if (pathToWalk.Count > 0)
            {
                WalkPath();
            } else
            {
                walking = false;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Die()
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Instantiate(itemDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            health -= 75;
            Time.timeScale = 0.00001f;
            SFXManager.instance.PlayHitMarker();
        }
        if (health < 0)
            Die();
        Time.timeScale = 1;
    }


    private void WalkPath()
    {
        //Vector2 movement = (pathToWalk[0] - new Vector2(transform.position.x, transform.position.y)).normalized;
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        //rb.AddForce(movement * moveSpeed);
        transform.position = Vector2.MoveTowards(transform.position, pathToWalk[0], moveSpeed * Time.deltaTime);

        //Rigidbody2D rb = pivot.GetComponent<Rigidbody2D>();
        //Vector2 movement = (pathToWalk[0] - rb.position).normalized;
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //Debug.Log(pathToWalk[0]);
        Vector2Int curPos = new Vector2Int(Mathf.RoundToInt(rb.position.x), Mathf.RoundToInt(rb.position.y));
        //var curPos = new Vector2Int((int)rb.position.x, (int)rb.position.y);
        Debug.Log(curPos + ", " + pathToWalk[0]);
        if (curPos == pathToWalk[0])
            pathToWalk.RemoveAt(0);
    }


    #region
    private List<Vector2Int> FindPathToGoal(Vector3 goal, Tilemap tilemap)
    {
        // these all only need to be declared once as long as the map does not change, move out of this 
        var vals = TilemapCoordinates(tilemap);
        Vector2Int[,] locations = vals.Item1;
        bool[,] map = vals.Item2;

        List<Node> worldTiles = new List<Node>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Node n = new Node() { index = new Vector2Int(i, j), isWalkable = map[i, j], worldLocation = locations[i, j] };
                worldTiles.Add(n);
            }
        }

        AStarPath astar = new AStarPath();

        // these are world locations need index in tilemap
        Vector2Int startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Vector2Int goalPosition = new Vector2Int((int)goal.x, (int)goal.y);

        startPosition = CoordinatesOf(worldTiles, startPosition);
        goalPosition = CoordinatesOf(worldTiles, goalPosition);

        List<Vector2Int> path = astar.FindPath(map, startPosition, goalPosition);

        // get world coordinates from index values
        List<Vector2Int> worldPath= new List<Vector2Int>();
        path.ForEach(item => worldPath.Add(locations[item.x, item.y]));

        return worldPath;
    }

    public (Vector2Int[,], bool[,]) TilemapCoordinates(Tilemap tileMap)
    {
        // Get all the coordinates of tiles that exist on the tilemap, Vector2Int array for coords and bool array for if its walkable

        Vector2Int[,] worldCoordinates = new Vector2Int[tileMap.cellBounds.size.x, tileMap.cellBounds.size.y];
        bool[,] walkableTiles = new bool[tileMap.cellBounds.size.x, tileMap.cellBounds.size.y];

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);

                int i = n - tileMap.cellBounds.xMin;
                int j = p - tileMap.cellBounds.yMin;

                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    worldCoordinates[i, j].x = (int)place.x;
                    worldCoordinates[i, j].y = (int)place.y;
                    walkableTiles[i, j] = true;
                }
                else
                {
                    //No tile at "place"
                    walkableTiles[i, j] = false;
                }
            }
        }

        return (worldCoordinates, walkableTiles);
    }

    // Find index coordinates of world location, if the world location is not walkable finds an adjacent walkable tile
    private Vector2Int CoordinatesOf(List<Node> worldTiles, Vector2Int worldLocation)
    {
        Node n = worldTiles.FirstOrDefault(item => item.worldLocation == worldLocation && item.isWalkable);
        if (n != null)
            return n.index;
        else
        {
            var node = worldTiles.FirstOrDefault(item => item.worldLocation == new Vector2Int(worldLocation.x - 1, worldLocation.y) && item.isWalkable) ??
                worldTiles.FirstOrDefault(item => item.worldLocation == new Vector2Int(worldLocation.x + 1, worldLocation.y) && item.isWalkable) ??
                worldTiles.FirstOrDefault(item => item.worldLocation == new Vector2Int(worldLocation.x, worldLocation.y + 1) && item.isWalkable) ??
                worldTiles.FirstOrDefault(item => item.worldLocation == new Vector2Int(worldLocation.x, worldLocation.y - 1) && item.isWalkable);
            if (node == null)
                Debug.LogError("Coordinates not found");
            return node == null ? new Vector2Int(-1, -1) : node.index;
        }
        
    }

    // Holds infomration from map, locations, and the index of the world location
    private class Node
    {
        public Vector2Int index;
        public Vector2Int worldLocation;
        public bool isWalkable;
    }
    #endregion
}
