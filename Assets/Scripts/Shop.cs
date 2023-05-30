using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject button;
    public GameObject[] srcPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Player.owned; i++) addButton(i);
    }

    private void addButton(int i)
    {
        UnitButton b = Instantiate(button, transform).GetComponent<UnitButton>();
        b.src = srcPrefabs[i];
        b.ID = i;
    }

    public void purchase(int i)
    {
        GameObject a = Instantiate(srcPrefabs[i]);
        a.transform.position = new Vector3(-12, -3, 0);
        Player.money -= srcPrefabs[i].GetComponent<Unit>().cost;
    }

}
