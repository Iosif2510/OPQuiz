using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    private List<GameObject> objectPool = new List<GameObject>(20);   
    private int currentNumber;
    private int poolCount { get { return objectPool.Count; }}

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform childObject in transform)
        {
            objectPool.Add(childObject.gameObject);
            childObject.gameObject.SetActive(false);
        }
        currentNumber = 0;
    }

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
            spawnedObject.transform.SetParent(gameObject.transform);
            objectPool.Add(spawnedObject);
            currentNumber++;
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
