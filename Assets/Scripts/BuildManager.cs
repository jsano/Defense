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
    private Transform kills;

    // Start is called before the first frame update
    void Start()
    {
        gold = addText("GoldImage");
        kills = addText("KillsImage");
    }

    // Update is called once per frame
    void Update()
    {
        gold.GetComponent<Text>().text = Player.money + "";
        kills.GetComponent<Text>().text = Player.kills + "";
    }

    private Transform addText(string name)
    {
        Transform tr = transform.Find(name);
        GameObject g = new GameObject();
        g.transform.SetParent(tr, false);
        g.layer = 5;
        Text t = g.AddComponent<Text>();
        t.fontSize = 50;
        t.color = Color.black;
        t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        t.alignment = TextAnchor.MiddleCenter;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        return g.transform;
    }

}
