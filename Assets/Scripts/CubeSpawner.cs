using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    /// <summary>
    /// 소환할 Cube들
    /// </summary>
    public GameObject CubePrefab;

    /// <summary>
    /// 소환할 위치
    /// </summary>
    public Vector3 SpawnPos;
    public Vector3 SpawnRot;
    private Quaternion rot;

    /// <summary>
    /// 몇 초마다 생성할 것인지?
    /// </summary>
    [Range(0.001f, 10)]
    public float SpawnTime;

    /// <summary>
    /// 생성된 큐브를 관리하는 리스트
    /// </summary>
    private List<GameObject> cubeList = new List<GameObject>();

    public ObjectPool objectPool;
    public bool PoolSpawn = true;
    public bool NonPoolSpawn = true;

    private WaitForSeconds waitSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPool.SetPrefab(CubePrefab);
        StartCoroutine(Timer());
        waitSeconds = new WaitForSeconds(SpawnTime);
        rot = Quaternion.Euler(SpawnRot);
    }

    // Update is called once per frame
    void Update()
    {
        // 키를 누르면 생성 및 제거
        if (Input.GetKey(KeyCode.Space))
        {
            if (PoolSpawn) objectPool.SpawnObject(SpawnPos, rot);
            if (NonPoolSpawn)
            {
                var go = Instantiate(CubePrefab, SpawnPos, rot);
                cubeList.Add(go);
            }

            
        }
        
        if (Input.GetKey(KeyCode.Return))
        {
            if (NonPoolSpawn)
            {
                if (cubeList.Count == 0)
                {
                    var item = cubeList.Last();
                    if (item != null)
                    {
                        Destroy(item);
                        cubeList.Remove(item);
                    }
                }
            }
            if (PoolSpawn) objectPool.DeleteObject();
        }
    }

    /// <summary>
    /// 일정 시간마다 큐브 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        while (true)
        {
            yield return waitSeconds;
            if (NonPoolSpawn)
            {
                var go = Instantiate(CubePrefab, SpawnPos, rot);
                cubeList.Add(go);
            }
            if (PoolSpawn) objectPool.SpawnObject(SpawnPos, rot);
        }
    }
    
    Quaternion RandomRot()
    {
        float x = Random.Range(0, 360);
        float y = Random.Range(0, 360);
        float z = Random.Range(0, 360);
        
        Quaternion ret = Quaternion.Euler(x, y, z);
        return ret;
    }
    
}
