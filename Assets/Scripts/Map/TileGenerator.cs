using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Given a floor scale and a tile prefab, cover the floor by placing tiles on top
/// This script should only be a component of a floor
/// </summary>
public class TileGenerator : MonoBehaviour
{
    // Size of the map. Scale should be a multiple of 10
    public int floorScale;

    // Tile prefab. Scale should be 5
    public GameObject baseTilePrefab;
    public GameObject streetTilePrefab;
    public GameObject rightBuildingTilePrefab;
    public GameObject leftBuildingTilePrefab;
    public GameObject[] obstructionTilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        // Set floor scale
        gameObject.transform.localScale = new Vector3(floorScale, floorScale, floorScale);

        GenerateTiles();
    }

    /// <summary>
    /// Cover the floor by placing tiles on top
    /// </summary>
    private void GenerateTiles()
    {
        // Variables
        float tilesPerRow = gameObject.transform.localScale.x / baseTilePrefab.transform.localScale.x;
        float tileSize = baseTilePrefab.transform.localScale.x * 10.0f;
        float tileBound;
        float tilePositionX;
        float tilePositionZ;

        // Find bound
        /*
         * Example 1: Floor scale 40, tile scale 5
         *  number of tiles per row = floor scale / tile scale
         *  8 = 40 / 5
         *
         *  positions of tiles:
         *  -175 -125 -75 -25 25 75 125 175
         *
         *  tiles positioned after 0 = number of tiles per row / 2
         *
         *  tile size = tile scale * 10
         *  
         *  position1 after 0 = tile size / 2
         *  position2 after 0 = position1 + tile size
         *  position3 after 0 = position2 + tile size
         *  ...
         *
         *
         * Example 2: Floor scale 35, tile scale 5  
         *  7 = 35 / 5
         *
         *  positions of tiles:
         *  -150 -100 -50 0 50 100 150
         *
         *  tiles positioned after 0 = number of tiles per row / 2
         *
         *  tile size = tile scale * 10
         *
         *  position1 after 0 = tile size
         *  position2 after 0 = position1 + tile size
         *  ...
         *
         */
        if (tilesPerRow % 2 == 0)
            tileBound = (tileSize / 2.0f) + (tileSize * ((tilesPerRow / 2.0f) - 1));
        else
            tileBound = tileSize * Mathf.Floor(tilesPerRow / 2.0f);

        // Turn it into left bound so we go from left to right
        tileBound *= -1.0f;
        tilePositionX = tilePositionZ = tileBound;

        // Counter for tile naming purposes
        float tileCounter = 0;

        for (int i = 0; i < tilesPerRow; i++)
        {
            for (int j = 0; j < tilesPerRow; j++)
            {
                // Create tile
                GameObject tile = Instantiate(baseTilePrefab);
                tile.name = "Tile_" + tileCounter;

                // Position tile
                tile.transform.position = new Vector3(tilePositionX, 0.0f, tilePositionZ);

                // Make child of floor
                tile.transform.SetParent(gameObject.transform);

                // Update values
                tilePositionX += tileSize;
                tileCounter += 1;
            }

            // New row
            tilePositionX = tileBound;
            tilePositionZ += tileSize;
        }
    }
}
