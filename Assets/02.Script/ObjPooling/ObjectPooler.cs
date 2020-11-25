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


    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; ++i)
            {
               GameObject obj =  Instantiate(pool.prefeb);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);   
        }
    }


    public GameObject SpawnFromPool(string tag ,  Vector3 Position , Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = Position;
        objectToSpawn.transform.rotation = rotation;


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


        obj.SetActive(false);

        poolDictionary[tag].Enqueue(obj);

        

    }
}
