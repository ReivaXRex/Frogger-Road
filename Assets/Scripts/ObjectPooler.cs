using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    private static ObjectPooler _instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("ObjectPooler doesn't exist!!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictonary = new Dictionary<string, Queue<GameObject>>();

    void Start()
    {
        CreatePools();
    }

    public void CreatePools()
    {
        foreach (Pool pool in pools) // Create an object pool for each Pool specified.
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) // Add objects to the Queue, up to it's max size limit.
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Take inactive objects and spawn them into active world.
    /// </summary>
    /// <param name="tag">
    /// Tag of the gameObject to spawn.
    /// </param>
    /// <param name="position">
    /// Where the object will be spawned.
    /// </param>
    /// <param name="rotation">
    /// The rotation of the gameObject.
    /// </param>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictonary.ContainsKey(tag)) // Check to make sure the Dictionary contains the Tag passed in.
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Take the object to spawn from the first entry of the Pool List.
        GameObject objectToSpawn = poolDictonary[tag].Dequeue();
        // Activate it.
        objectToSpawn.SetActive(true);
        // Move and rotate it to it's appropriate place in the world.
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Search for the interface on the object to be spawned.
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null) // null check
        {
            pooledObject.OnObjectSpawn(); // Apply starting effects on the object spawned.
        }

        // Add it back to the Queue so it can be reused.
        poolDictonary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
