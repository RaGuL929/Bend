using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemanager : MonoBehaviour
{
    public GameObject[] tileprefabs;
    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 15.0f;
    private int amtoftiles = 7;
    private List<GameObject> activeTiles;
    private float safezone = 30.0f;
    private int lastprefabindex = 0;
    // Start is called before the first frame update
    private void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < amtoftiles; i++)
        {
            spawnTiles();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z -safezone> spawnZ - amtoftiles * tileLength)
        {
            spawnTiles();
            deleteTiles();
        }
    }
    private void  spawnTiles(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tileprefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    private void deleteTiles()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }
    private int RandomPrefabIndex()
    {
        if (tileprefabs.Length <= 1)
        {
            return 0;

        }
        int randomIndex = lastprefabindex;
        while (randomIndex == lastprefabindex)
        {
            randomIndex = Random.Range(0, tileprefabs.Length);

        }
        lastprefabindex = randomIndex;
        return randomIndex;
        
    }
    
}
