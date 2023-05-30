using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    void Awake()
    {
        if (instance != null) {
            return;
        }
        instance = this;
    }

    private Transform gold; // grandchild text gameobject
    private GameObject selectedUnit;

    // Start is called before the first frame update
    void Start()
    {
        gold = transform.Find("GoldImage");//.Find("Gold");
        GameObject g = new GameObject();
        g.transform.SetParent(gold, false);
        g.layer = 5;
        Text t = g.AddComponent<Text>();
        t.fontSize = 50;
        t.color = Color.black;
        t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        t.alignment = TextAnchor.MiddleCenter;
        gold = g.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gold.GetComponent<Text>().text = Player.money + "";
    }
}
