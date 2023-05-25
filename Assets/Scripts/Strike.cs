using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{

    private float life = 0.5f;
    private float period = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period >= life) Destroy(gameObject);
        period += Time.deltaTime;
    }
}
