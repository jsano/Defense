using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject[] srcPrefabs;
    public GameObject castle;
    private int idx = 1;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(castle, Constants.CASTLEPOS, Quaternion.identity);
        InvokeRepeating("spawn", 0.5f, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        GameObject e = Instantiate(srcPrefabs[idx]);
        e.transform.position = new Vector3(Constants.ENEMYX, Constants.GROUNDY, 0);
        idx = Mathf.Max(1, (idx + 1) % (Player.eowned + 1));
    }

}
