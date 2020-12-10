using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefeb;
        public int size;
    }

    
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; ++i)
            {
                GameObject obj = Instantiate(pool.prefeb);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }
    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

   


    public GameObject SpawnFromPool(string tag ,Vector3 Position , Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.position = Position;
        objectToSpawn.SetActive(true);


        //IPooledObj pooledObj = objectToSpawn.GetComponent<IPooledObj>();

        //if(pooledObj != null)
        //{
        //    pooledObj.OnObjectSpawn();
        //}

        
        return objectToSpawn;
    }

    public IEnumerator SpawnBack(string tag, GameObject obj, float time)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            yield return null;
        }

        yield return new WaitForSeconds(time);


        //if (obj.active)
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }

        poolDictionary[tag].Enqueue(obj);
        



    }
}
