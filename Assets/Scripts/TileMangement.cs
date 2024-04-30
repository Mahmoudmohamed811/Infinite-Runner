using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMangement : MonoBehaviour
{
    public GameObject[] tilePrefaps;
    public float zSpawn = 0f;
    public float tileLenght = 30f;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefaps.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLenght))
        {
            SpawnTile(Random.Range(0, tilePrefaps.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefaps[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLenght;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

}