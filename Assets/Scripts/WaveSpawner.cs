using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject[] srcPrefabs;
    public GameObject castle;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(castle, Constants.CASTLEPOS, Quaternion.identity);
        InvokeRepeating("spawn", 0.5f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        GameObject e = Instantiate(srcPrefabs[0]);
        e.transform.position = new Vector3(Constants.ENEMYX, Constants.GROUNDY, 0);
    }

}
