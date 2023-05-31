using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{

    public GameObject src;
    public int ID;
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
        //GetComponentInParent<Shop>().purchase(ID);
        GameObject a = Instantiate(src);
        a.transform.position = new Vector3(Constants.ALLYX, Constants.GROUNDY, 0);
        Player.money -= src.GetComponent<Unit>().getCombatCost();
        Debug.Log("bought " + ID);
    }

    void Update()
    {
        cost.GetComponent<Text>().text = src.GetComponent<Unit>().getCombatCost() + "";
        b.interactable = Player.money >= src.GetComponent<Unit>().getCombatCost();
    }

}
