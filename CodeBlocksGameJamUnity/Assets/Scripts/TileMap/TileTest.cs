using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class TileTest : MonoBehaviour
{
    public Vector2Int startPosition;
    public Vector2Int goalPosition;
    private Tilemap tilemap;
    private readonly string ttag = "Player";
    private Vector3[,] locations;
    private bool[,] map;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        startPosition = new Vector2Int(23, 30);
        //startPosition = new Vector2Int(7, 9);
        goalPosition = new Vector2Int(27, 19);

        var vals = TilemapCoordinates(tilemap, true);
        locations = vals.Item1;
        map = vals.Item2;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var g in GameObject.FindGameObjectsWithTag(ttag))
                Destroy(g);

            AStarPath astar = new AStarPath();
            List<Vector2Int> path = astar.FindPath(map, startPosition, goalPosition);
            foreach (var thing in path)
            {
                Debug.Log(thing);
                Vector3 loc = locations[thing.x, thing.y];
                CreateWorldText("*", null, loc, 10, Color.green, true);
            }
        }
    }


    public (Vector3[,], bool[,]) TilemapCoordinates(Tilemap tileMap, bool display)
    {
        // Get all the coordinates of tiles that exist on the tilemap, Vector2Int array for coords and bool array for if it exists
        // Vector2Int is not a nullable type

        Vector3[,] Points = new Vector3[tileMap.cellBounds.size.x, tileMap.cellBounds.size.y];
        bool[,] Map = new bool[tileMap.cellBounds.size.x, tileMap.cellBounds.size.y];

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
                    Points[i, j] = place;
                    Map[i, j] = true;

                    if (display)
                    {
                        //CreateWorldText(i + "," + j, null, place, 3, null, false);
                        CreateWorldText(place.x + "," + place.y, null, place, 3, null, false);
                    }
                    

                }
                else
                {
                    //No tile at "place"
                    Map[i, j] = false;
                    //TextMeshPro t = CreateWorldText("null", null, place, 3, null);
                    //t.SetText(Points[i, j].x + "," + Points[i,j].y);
                }
            }
        }

        return (Points, Map);
    }

    private TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPostion = default, int fontSize = 40, Color? color = null, bool isTagged = true, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center, int sortingOrder = 5000)
    {
        GameObject gObj = new GameObject("World Text", typeof(TextMeshPro));
        if (isTagged)
            gObj.tag = ttag;
        Transform tForm = gObj.transform;
        tForm.SetParent(parent, false);
        tForm.localPosition = localPostion;
        TextMeshPro textMesh = gObj.GetComponent<TextMeshPro>();
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color ?? Color.white;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

}
