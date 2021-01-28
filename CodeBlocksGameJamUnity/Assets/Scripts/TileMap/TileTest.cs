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

        //DisplayTiles(allTiles, bounds);
        TilemapCoordinates(tilemap);
        //TilemapCoordinates2(tilemap);
    }

    private void DisplayTiles(TileBase[] allTiles, BoundsInt bounds)
    {
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null");
                }
            }
        }
    }

    private void TilemapCoordinates(Tilemap tileMap)
    {
        List<Vector3> availablePlaces = new List<Vector3>();


        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                    //TextMeshPro tMesh = CreateWorldText((n, p).ToString(), null, place, 3, null);
                    TextMeshPro t = CreateWorldText("1", null, place, 3, null);
                    t.SetText(place.x.ToString() + "," + place.y.ToString());
                }
                else
                {
                    //No tile at "place"
                    //CreateWorldText("0", null, place, 3, null);
                }
            }
        }

        Debug.Log(availablePlaces.Count);
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
