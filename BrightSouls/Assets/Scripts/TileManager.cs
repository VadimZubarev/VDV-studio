using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpown = 0;
    public float tileLength = 10;
    public int numOfTiles = 5;
    public Transform playerTransform;
    private List <GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numOfTiles; i++)
        {
            if (i == 0)
                SpownTile(i);
            SpownTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransform.position.z-35 > zSpown - (numOfTiles * tileLength))
        {
            SpownTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpownTile (int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpown, transform.rotation);
        activeTiles.Add(go);
        zSpown += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
