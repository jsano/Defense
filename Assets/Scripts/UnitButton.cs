using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{

    public GameObject src;
    public int ID;
    private Button b;

    void Start()
    {
        GetComponent<Image>().color = src.GetComponent<SpriteRenderer>().color;
        Transform t = transform.Find("Cost");
        t.GetComponent<Text>().text = src.GetComponent<Unit>().cost + "";
        b = GetComponent<Button>();
        b.onClick.AddListener(clicked);
    }

    void clicked()
    {
        GetComponentInParent<Shop>().purchase(ID);
    }

    void Update()
    {
        b.interactable = Player.money >= src.GetComponent<Unit>().cost;
    }

}
