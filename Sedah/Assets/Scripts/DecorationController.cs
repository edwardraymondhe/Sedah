using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float z = Random.Range(-0.5f, 0.5f);
        float rotZ = Random.Range(0, 360);
        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        transform.forward = new Vector3(transform.forward.x, transform.forward.y, rotZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
