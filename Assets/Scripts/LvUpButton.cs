using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvUpButton : MonoBehaviour
{
    
    [HideInInspector] public GameObject src;
    [HideInInspector] public int ID;
    private Button b;
    private Transform cost;

    void Start()
    {
        GetComponent<Image>().color = src.GetComponent<SpriteRenderer>().color;
        cost = transform.Find("Cost");
        
        b = GetComponent<Button>();
        b.onClick.AddListener(clicked);
    }

    void clicked()
    {
        Unit[] targets = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit t in targets) {
            if (t.ID == ID && t.getCombatSpeed() > 0) {
                t.levelUpHp();
            }
        }
        Player.money -= (int) (src.GetComponent<Unit>().getUpgradeCost());
        Player.levels[ID] += 1;
    }

    void Update()
    {
        cost.GetComponent<Text>().text = src.GetComponent<Unit>().getUpgradeCost() + " " + Player.levels[ID];
        b.interactable = Player.money >= src.GetComponent<Unit>().getUpgradeCost();
    }
}
