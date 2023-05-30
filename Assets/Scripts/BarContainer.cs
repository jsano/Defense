using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarContainer : MonoBehaviour
{

    private float barLength = 1;
    private float barWidth = 0.1f;
    public GameObject _bar;
    Transform parent;
    private int layer = 0;

    void Awake()
    {
        parent = transform.parent;
        transform.localPosition = new Vector3(0, 0.935f, 0);
        if (parent.GetComponent<Unit>() == null)
            transform.localScale = new Vector3(barLength + 0.02f, barWidth+0.05f, 0);
        else 
            transform.localScale = new Vector3(barLength + 0.02f, 2*barWidth+0.05f, 0);
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.4f);
        GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

    public void makeBar(string name, float height)
    {
        //Pivot controls and is the anchor for scaling
        GameObject pivot = new GameObject();
        pivot.name = name + "Pivot";
        pivot.transform.localPosition = new Vector3(-0.5f, height, 0);
        pivot.transform.SetParent(parent, false); // Update world position
        Transform bar = Instantiate(_bar).transform;
        bar.transform.localPosition = new Vector3(0.5f, 0, 0);
        bar.SetParent(pivot.transform, false);
        if (name == "EN") {
            bar.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        pivot.transform.localScale = new Vector3(0, barWidth, 0);
        bar.GetComponent<SpriteRenderer>().sortingOrder = layer + 1;
    }

    // Update is called once per frame
    void Update()
    {
        Transform bar = parent.Find("HPPivot");
        Unit g = parent.GetComponent<Unit>();
        if (g == null) {
            Castle g1 = parent.GetComponent<Castle>();
            bar.localScale = new Vector3(barLength * (g1.getCurrentHP()/g1.maxHp), barWidth, 0);
            return;
        } else bar.localScale = new Vector3(barLength * (g.getCurrentHP()/g.maxHp), barWidth, 0);
        
        Transform bar1 = parent.Find("ENPivot");
        bar1.localScale = new Vector3(barLength * (g.getCurrentEn()/100.0f), barWidth, 0);
    }
}
