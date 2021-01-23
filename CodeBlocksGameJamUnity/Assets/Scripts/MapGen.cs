using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public GameObject[] Encounters;
    public int Depth;
    private float xPos;
    private float yPos;
    private const float RATIO = 1;
    private const float DEPTH_DIST = 5 * RATIO;
    private const float NODE_DIST = 10 * RATIO;
    private int totalNodes;
    private int[] encounterChoice;
    // Start is called before the first frame update
    void Start()
    {
        CreateMaze();
        DrawMaze();
    }

    private void CreateMaze()
    {
        totalNodes = (int)Mathf.Pow(2, Depth)-2;
        encounterChoice = new int[totalNodes];

        for (int i = 0; i < totalNodes; i++)
        {
            int randomCounter = Random.Range(0, 9);

            if (randomCounter <= 6)
            {
                encounterChoice[i] = 0;
            }

            if (randomCounter > 6)
            {
                encounterChoice[i] = 1;

            }
        }
    }
    private void DrawMaze()
    {
        xPos = 0;
        yPos = 0;
        float nD2 = NODE_DIST / 2;
        int f = 0;
        for (int i = 1; i < Depth; i++)
        {
            xPos += DEPTH_DIST;
            yPos += nD2;
            for (int j = 0; j < (int)Mathf.Pow(2, i); j++)
            {
                float xpoint1 = xPos;
                float ypoint1 = yPos;
                Instantiate(Encounters[encounterChoice[f]], new Vector3(xpoint1, ypoint1, 0), Quaternion.identity);
                f++;
                yPos -= 2 * nD2;
            }
            yPos = yPos + (Mathf.Pow(2, i+1) * nD2);
            nD2 = nD2/2;
        }



    }
}
