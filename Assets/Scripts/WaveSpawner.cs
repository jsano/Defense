using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject[] srcPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0.5f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        GameObject e = Instantiate(srcPrefabs[0]);
        e.transform.position = new Vector3(12, -3, 0);
    }

}
