using UnityEngine;
using System;
public class LandscapeMaker : MonoBehaviour {



    private int heightMapSideLength = 512;

    private float scale = 0.5f;         //scale of noise


    // Use this for initialization
    void Start () {

        TerrainData landscapeData = GetComponent<Terrain>().terrainData;

        landscapeData.heightmapResolution = heightMapSideLength + 1;

        generateLandscape(landscapeData);
        paintLandscape(landscapeData);
    }

    void generateLandscape(TerrainData data)
    {

        float [,] heightData = new float[heightMapSideLength + 1, heightMapSideLength + 1];

        heightData[0, 0] = 0.5f;
        heightData[heightMapSideLength, 0] = 0.5f;
        heightData[0, heightMapSideLength] = 0.5f;
        heightData[heightMapSideLength, heightMapSideLength] = 0.5f;

        // nine passes needed to finish building the map
        for (int i = 0;i < 9; i++)
        {
           int squareSideLength = (int) (heightMapSideLength / Math.Pow(2, i));

            for (int row = 0; row < heightMapSideLength; row += squareSideLength)
            {
                for (int col = 0; col < heightMapSideLength; col += squareSideLength)
                {
                    //diamond step
                    heightData[row + squareSideLength / 2, col + squareSideLength / 2] = 
                        (heightData[row, col] 
                        + heightData[row + squareSideLength, col] 
                        + heightData[row, col + squareSideLength] 
                        + heightData[row + squareSideLength, col + squareSideLength])/4 + UnityEngine.Random.Range(-1f, 1f) * scale;

                    //square step

                    // North
                    heightData[row + squareSideLength / 2, col] = (heightData[row, col] + heightData[row + squareSideLength, col])/2+ UnityEngine.Random.Range(-1f, 1f) * scale;
                    // West
                    heightData[row, col + squareSideLength / 2] = (heightData[row, col] + heightData[row, col + squareSideLength])/2 + UnityEngine.Random.Range(-1f, 1f) *scale;
                    // East
                    heightData[row + squareSideLength, col + squareSideLength / 2] = (heightData[row + squareSideLength, col] + heightData[row + squareSideLength, col + squareSideLength])/2 + UnityEngine.Random.Range(-1f, 1f) * scale;
                    // South
                    heightData[row + squareSideLength/2, col + squareSideLength] = (heightData[row , col + squareSideLength] + heightData[row + squareSideLength, col + squareSideLength]) / 2 + UnityEngine.Random.Range(-1f, 1f) * scale;

                }
            }

            // smaller random amount of noise
            scale /= 2f;
        }

        //set the landscape
        data.SetHeights(0, 0, heightData);

    }

    void paintLandscape(TerrainData data)
    {


        float[,,] texureMap = new float[data.alphamapWidth, data.alphamapHeight, data.alphamapLayers];

        for (int y = 0; y < data.alphamapHeight; y++)
        {
            for (int x = 0; x < data.alphamapWidth; x++)
            {

                float normY = (float)y / (float)data.alphamapHeight;
                float normX = (float)x / (float)data.alphamapWidth;

                float height = data.GetHeight(Mathf.RoundToInt(normY * data.heightmapHeight), Mathf.RoundToInt(normX * data.heightmapWidth));

                if (height > 0.9 * data.heightmapHeight)
                {
                    texureMap[x, y, 1] = 1f;
                }
                else
                {
                    texureMap[x, y, 0] = 1f;
                }

            }
        }

        data.SetAlphamaps(0, 0, texureMap);
    }
}
