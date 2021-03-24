using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isLog;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z >= 20)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z <= -20)
        {
            Destroy(gameObject);
        }
    }
}

 
