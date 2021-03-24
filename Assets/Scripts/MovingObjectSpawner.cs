using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingObject;
    // [SerializeField] private GameObject movingObject;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] bool isLeft;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            int randomObject = Random.Range(0, movingObject.Count);
            GameObject go = Instantiate(movingObject[randomObject], spawnPos.position, Quaternion.identity);

            if (!isLeft)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
     
    }
}
