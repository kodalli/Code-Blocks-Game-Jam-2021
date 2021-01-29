using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class TileTest : MonoBehaviour
{
    private void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        var vals = TilemapCoordinates(tilemap);
        Vector2Int[,] locations = vals.Item1;
        bool[,] map = vals.Item2;

        //DisplayTiles(allTiles, bounds);
        //TilemapCoordinates(tilemap);
        //TilemapCoordinates2(tilemap);
    }

    #region
    private Vector2Int[,] DisplayTiles(TileBase[] allTiles, BoundsInt bounds)
    {
        Vector2Int[,] Points = new Vector2Int[bounds.size.x, bounds.size.y];
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    Points[x, y] = new Vector2Int(x, y);
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null");
                }
            }
        }
        return Points;
    }

    private void TilemapCoordinates2(Tilemap tilemap)
    {
        List<Vector3> availablePlaces = new List<Vector3>();
        foreach (Vector3 position in tilemap.cellBounds.allPositionsWithin)
        {
            availablePlaces.Add(position);
        }
        //Debug.Log(availablePlaces.Count);
    }
    #endregion

    private (Vector2Int[,], bool[,]) TilemapCoordinates(Tilemap tileMap)
    {
        // Get all the coordinates of tiles that exist on the tilemap, Vector2Int array for coords and bool array for if it exists
        // Vector2Int is not a nullable type

        //List<Vector3> availablePlaces = new List<Vector3>();
        Vector2Int[,] Points = new Vector2Int[tileMap.cellBounds.size.x, tileMap.cellBounds.size.y];
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
                    //availablePlaces.Add(place);

                    Points[i, j] = new Vector2Int((int)place.x, (int)place.y);
                    Map[i, j] = true;

                    TextMeshPro t = CreateWorldText("1", null, place, 3, null);
                    //t.SetText(place.x.ToString() + "," + place.y.ToString() + " " + Points[i,j].x + "," + Points[i,j].y);
                    t.SetText(Points[i, j].x + "," + Points[i, j].y);
                    
                }
                else
                {
                    //No tile at "place"
                    Map[i, j] = false;
                    TextMeshPro t = CreateWorldText("0", null, place, 3, null);
                    t.SetText(Points[i, j].x + "," + Points[i,j].y);
                }
            }
        }

        //Debug.Log(availablePlaces.Count);
        return (Points, Map);
    }



    private TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPostion = default, int fontSize = 40, Color? color = null, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center, int sortingOrder = 5000)
    {
        GameObject gObj = new GameObject("World Text", typeof(TextMeshPro));
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
