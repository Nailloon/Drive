using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Generation : MonoBehaviour
{
    public GameObject[] floorPrefabs;
    private List <GameObject> activeFloor = new List <GameObject>();
    private float spawnPosition = 3;
    private float tileLength = 100;
    private int startFloors = 4;
    [SerializeField] private Transform car;
    void Start()
    {
        for (int i = 0; i < startFloors; i++)
        {
            SpawnFloor(Random.Range(0, floorPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (car.position.z + 60 < (startFloors * tileLength) - spawnPosition)
        {
            SpawnFloor(Random.Range(0, floorPrefabs.Length));
            DeleteFloor();
        }
    }
    private void SpawnFloor(int floorIndex)
    {
        GameObject nextFloor = Instantiate(floorPrefabs[floorIndex], transform.right * spawnPosition, transform.rotation);
        activeFloor.Add(nextFloor);
        spawnPosition += tileLength;
    }
    private void DeleteFloor()
    {
        Destroy(activeFloor[0]);
        activeFloor.RemoveAt(0);
    }
}
