using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{

    public GameObject src;

    void Awake()
    {
        GetComponent<Image>().color = src.GetComponent<SpriteRenderer>().color;
        Transform t = transform.Find("Cost");
        t.GetComponent<Text>().text = src.GetComponent<Unit>().cost + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
