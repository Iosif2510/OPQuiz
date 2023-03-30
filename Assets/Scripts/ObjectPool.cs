using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    private List<GameObject> objectPool = new List<GameObject>(100);   
    private int currentNumber = 0;
    private int poolCount = 0;

    // Start is called before the first frame update

    public void SetPrefab(GameObject prefab)
    {
        objectPrefab = prefab;
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObject;
        if (currentNumber == poolCount) 
        {
            spawnedObject = Instantiate(objectPrefab, position, rotation);
            objectPool.Add(spawnedObject);
            currentNumber++;
            poolCount++;
        }
        else 
        {
            spawnedObject = objectPool[currentNumber++];
            spawnedObject.transform.position = position;
            spawnedObject.transform.rotation = rotation;
            spawnedObject.SetActive(true);
        }
        return spawnedObject;
    }

    public void DeleteObject()
    {
        if (currentNumber > 0) objectPool[--currentNumber].SetActive(false);
    }
}
