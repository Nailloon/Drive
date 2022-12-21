using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Generation : MonoBehaviour
{
    public GameObject[] floorPrefabs;
    private List <GameObject> activeFloor = new List <GameObject>();
    private float spawnPosition = 3;
    private float tileLength;
    private int startFloors = 15;
    [SerializeField] private Transform car;
    void Start()
    {
        SpawnFloor(0);
        tileLength=GameObject.Find("road").GetComponent<MeshRenderer>().bounds.size.z;
        for (int i = 0; i < startFloors; i++)
        {
            SpawnFloor(Random.Range(0, floorPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (car.position.z + 200 < (startFloors * tileLength) - spawnPosition)
        {
            SpawnFloor(Random.Range(0, floorPrefabs.Length));
            DeleteFloor();
        }
    }
    private void SpawnFloor(int floorIndex)
    {
        GameObject nextFloor = Instantiate(floorPrefabs[floorIndex], transform.right * spawnPosition, Quaternion.identity);
        activeFloor.Add(nextFloor);
        spawnPosition += tileLength;
    }
    private void DeleteFloor()
    {
        Destroy(activeFloor[0]);
        activeFloor.RemoveAt(0);
    }
}
