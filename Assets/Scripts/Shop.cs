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
        for (int i = 1; i <= Player.owned; i++) addButton(i);
    }

    private void addButton(int i)
    {
        GameObject b = Instantiate(button, transform);
        if (b.GetComponent<UnitButton>() == null) {
            LvUpButton l = b.GetComponent<LvUpButton>();
            l.src = srcPrefabs[i];
            l.ID = i;
            return;
        }
        UnitButton b1 = b.GetComponent<UnitButton>();
        b1.src = srcPrefabs[i];
        b1.ID = i;
    }

}
