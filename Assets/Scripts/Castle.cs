using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public float maxHp;
    private float hp;
    public GameObject barContainer;
    public GameObject dissolve;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        BarContainer bc = Instantiate(barContainer, transform, false).GetComponent<BarContainer>();
        bc.makeBar("HP", 0.935f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            Debug.Log("Died");
            GameObject p0 = Instantiate(dissolve, transform.position, transform.rotation);
            ParticleSystem.MainModule p = p0.GetComponent<ParticleSystem>().main;
            p.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(gameObject);
        }
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
    }  

    public float getCurrentHP()
    {
        return hp;
    }

}
