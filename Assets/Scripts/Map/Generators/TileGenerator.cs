using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Given a floor scale and a tile prefab, cover the floor by placing tiles on top
/// This script should only be a component of a floor
/// </summary>
public class TileGenerator : MonoBehaviour
{
    #region Properties

    // Size of the map. Scale should be a multiple of 10
    public int floorScale;

    // Tile prefab. Scale should be 5
    public GameObject baseTilePrefab;
    public GameObject streetTilePrefab;
    public GameObject buildingTilePrefab;
    public GameObject[] obstructionTilePrefabs;

    // Contains a map of tile types prefabs per row
    // Regenerated as each row is processed
    private Hashtable tileMap = new Hashtable();

    // Other
    private float tilesPerRow;
    private int tilesPerHalf;
    private float tileSize;
    private int buildingPosition = 2;

    #endregion


    #region Public Methods

    /// <summary>
    /// Cover the floor by placing tiles on top
    /// </summary>
    public void GenerateTiles()
    {
        // Set floor scale and tiles per row
        gameObject.transform.localScale = new Vector3(floorScale, floorScale, floorScale);
        tilesPerRow = gameObject.transform.localScale.x / baseTilePrefab.transform.localScale.x;
        tilesPerHalf = (int)Mathf.Floor(tilesPerRow / 2.0f);
        tileSize = baseTilePrefab.transform.localScale.x * 10.0f;

        // Variables
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

        // Building flag check to set rotation
        bool isBuildingTile;

        for (int i = 0; i < tilesPerRow; i++)
        {
            // Generate tile map to determine what type of tile goes where in each row
            StartCoroutine("TileSelector");

            for (int j = 0; j < tilesPerRow; j++)
            {
                // Select tile
                string key = j.ToString();
                GameObject tile = (GameObject)tileMap[key];

                // Check if tile is Building
                if (tile == buildingTilePrefab)
                    isBuildingTile = true;
                else
                    isBuildingTile = false;

                tile = Instantiate(tile, new Vector3(tilePositionX, 0.0f, tilePositionZ), Quaternion.identity);
                tile.name = "Tile_" + tileCounter;

                // Position tile
                //tile.transform.position = new Vector3(tilePositionX, 0.0f, tilePositionZ);

                // Rotate Building tile so it faces the right direction
                if (isBuildingTile)
                {
                    if (j <= tilesPerHalf)
                        tile.transform.Rotate(0.0f, 0.0f, 0.0f);
                    else
                        tile.transform.Rotate(0.0f, -180.0f, 0.0f);
                }

                // Make child of floor
                tile.transform.SetParent(gameObject.transform);

                // Update values
                tilePositionX += tileSize;
                tileCounter += 1;
            }

            // New row
            tilePositionX = tileBound;
            tilePositionZ += tileSize;

            // Reset tileMap
            tileMap.Clear();
        }
    }

    public void SetFloorScale(int scale)
    {
        floorScale = scale;
    }

    #endregion


    #region Private Methods

    /// <summary>
    /// Considering a probability of spawning an obstruction tile prefab,
    /// select one of the obstructors in obstructionTilePrefabs and return it,
    /// otherwise return the base tile prefab
    /// </summary>
    /// <returns>A randomly selected obstructionPrefab or the baseTilePrefab</returns>
    private GameObject ObstructionSelector()
    {
        int index = Random.Range(0, 2);

        if (index == 1)
        {
            index = Random.Range(0, obstructionTilePrefabs.Length);
            return obstructionTilePrefabs[index];
        }

        return baseTilePrefab;
    }

    /// <summary>
    /// Considering a probability of spawning a building tile prefab,
    /// return it, otherwise return the base tile prefab 
    /// </summary>
    /// <returns>A randomly selected obstructionPrefab or the baseTilePrefab</returns>
    private GameObject BuildingSelector()
    {
        int index = Random.Range(0, 2);

        if (index == 1)
            return buildingTilePrefab;

        return baseTilePrefab;
    }

    #endregion


    #region Coroutines

    /// <summary>
    /// Sets a row's tile map to determine what tile prefab goes in what position
    /// </summary>
    /// <returns>Null</returns>
    IEnumerator TileSelector()
    {
        // Scale must be a multiple of 10
        if (gameObject.transform.localScale.x % 10 == 0 &&
            gameObject.transform.localScale.x >= 20)
        {
            // Base cases
            if (gameObject.transform.localScale.x == 20)
            {
                tileMap.Add("0", BuildingSelector());
                tileMap.Add("1", streetTilePrefab);
                tileMap.Add("2", streetTilePrefab);
                tileMap.Add("3", BuildingSelector());
            }
            else if (gameObject.transform.localScale.x == 30)
            {
                tileMap.Add("0", ObstructionSelector());
                tileMap.Add("1", BuildingSelector());
                tileMap.Add("2", streetTilePrefab);
                tileMap.Add("3", streetTilePrefab);
                tileMap.Add("4", BuildingSelector());
                tileMap.Add("5", ObstructionSelector());
            }
            else if (gameObject.transform.localScale.x == 40)
            {
                tileMap.Add("0", ObstructionSelector());
                tileMap.Add("1", ObstructionSelector());
                tileMap.Add("2", BuildingSelector());
                tileMap.Add("3", streetTilePrefab);
                tileMap.Add("4", streetTilePrefab);
                tileMap.Add("5", BuildingSelector());
                tileMap.Add("6", ObstructionSelector());
                tileMap.Add("7", ObstructionSelector());
            }
            else
            {
                int counter = 0;

                /* One side is a mirror of the other (ignoring probabilities
                 * involved in tile creation). Therefore, assign the mapping
                 * using absolute value to only account for indeces above 0.
                 */
                for (int i = -tilesPerHalf; i < tilesPerHalf; i++)
                {
                    int positionFromCenter = Mathf.Abs(i);

                    // Adjust for 0 when i < 0
                    if (i < 0)
                        positionFromCenter -= 1;

                    // Add tile type to map
                    if (positionFromCenter > buildingPosition)
                        tileMap.Add(counter.ToString(), ObstructionSelector());
                    else if (positionFromCenter == buildingPosition)
                        tileMap.Add(counter.ToString(), BuildingSelector());
                    else if (positionFromCenter < buildingPosition)
                        tileMap.Add(counter.ToString(), streetTilePrefab);

                    counter += 1;
                }
            }
        }

        yield return null;
    }

    #endregion
}
