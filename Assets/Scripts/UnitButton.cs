using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{

    public GameObject src;
    public int ID;

    void Awake()
    {
        GetComponent<Image>().color = src.GetComponent<SpriteRenderer>().color;
        Transform t = transform.Find("Cost");
        t.GetComponent<Text>().text = src.GetComponent<Unit>().cost + "";
        GetComponent<Button>().onClick.AddListener(clicked);
    }

    void clicked()
    {
        GetComponentInParent<Shop>().purchase(ID);
    }
}
