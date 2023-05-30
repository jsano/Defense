using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 dest;
    private bool toDestroy = false;
    private GameObject attacking; // First is current target

    [Header("Info")]
    public int cost;

    [Header("Stats")]
    public float speed;
    public float maxHp;
    private float hp;
    private float en = 0;
    public float atk;
    public float atkspd;
    private float period;
    public float range = 0.75f;

    [Header("Graphics")]
    public GameObject barContainer;
    public GameObject strike; 
    public GameObject rangedItem;
    public bool ranged;
    public GameObject dissolve;
    private int layer;

    void Awake()
    {
        layer = GetComponent<SpriteRenderer>().sortingOrder;
        hp = maxHp;
        period = atkspd/2;
        BarContainer bc = Instantiate(barContainer, transform, false).GetComponent<BarContainer>();
        bc.makeBar("HP", 1);
        bc.makeBar("EN", 0.87f);
        if (tag == "Enemy") dest = new Vector3(-10, transform.position.y, 0); 
        else dest = new Vector3(10, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            toDestroy = true;
            return;
        }
        if (hp > maxHp) hp = maxHp;
        if (attacking == null) StartCoroutine(FindTarget());
        if (attacking == null) {
            period = atkspd/2;
            transform.position += transform.right*speed*Time.deltaTime;
        } else {
            if (period > atkspd) {
                period = 0;
                Attack();
            }
            period += Time.deltaTime;
        }

    }

    void LateUpdate()
    {
        if (toDestroy) {
            GameObject p0 = Instantiate(dissolve, transform.position, transform.rotation);
            ParticleSystem.MainModule p = p0.GetComponent<ParticleSystem>().main;
            p.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        if (attacking == null) return;
        if (attacking.GetComponent<Unit>() == null) attacking.GetComponent<Castle>().receiveDamage(atk);
        else attacking.GetComponent<Unit>().receiveDamage(atk);
        en = Math.Min(en+25, 100);
        GameObject s;
        if (ranged) {
            s = Instantiate(rangedItem, transform.position + new Vector3(0.5f, 0, 0), Quaternion.Euler(0, transform.eulerAngles.y, -90));
            s.GetComponent<RangedItem>().attacking = attacking;
            s.tag = tag + "P";
        }
        else s = Instantiate(strike, attacking.transform.position, transform.rotation);
        s.GetComponent<SpriteRenderer>().sortingOrder = attacking.layer + 1;
    }

    private IEnumerator FindTarget()
    {
        GameObject[] targets;
        if (tag == "Ally") targets = GameObject.FindGameObjectsWithTag("Enemy");
        else targets = GameObject.FindGameObjectsWithTag("Ally");
        float min = Mathf.Infinity;
        GameObject nearest = null;
        foreach (GameObject t in targets) {
            float dist = Math.Abs(transform.position.x - t.transform.position.x);
            if (dist < min) {
                min = dist;
                nearest = t;
            }
        }
        if (nearest != null && min <= range) {
            // Enemy will start attacking 1 frame later than ally for QOL
            if (tag == "Enemy") yield return new WaitForSeconds(Time.deltaTime);
            attacking = nearest;
        }
        else attacking = null;
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
        en = Math.Min(en+1, 100);
    }    
    
    public float getCurrentHP()
    {
        return hp;
    }

    public float getCurrentEn()
    {
        return en;
    }

}
