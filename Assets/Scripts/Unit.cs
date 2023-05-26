using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 dest;
    
    private GameObject attacking; // First is current target

    [Header("Info")]
    public int cost;

    [Header("Stats")]
    public float speed = 5f;
    public float maxHp;
    private float hp;
    private float en = 0;
    public float atk;
    public float atkspd;
    private float period;
    public float range = 0.75f;

    [Header("Graphics")]
    public GameObject barContainer;
    public GameObject strike; // null if ranged
    public GameObject rangedItem; // null if physical
    public GameObject dissolve;
    private int layer;

    void Awake()
    {
        //InvokeRepeating("FindTarget", 0, 0.1f);
        layer = GetComponent<SpriteRenderer>().sortingOrder;
        hp = maxHp;
        period = atkspd/2;
        Instantiate(barContainer, transform, false);
        if (tag.CompareTo("Enemy") == 0) dest = new Vector3(-10, transform.position.y, 0); 
        else dest = new Vector3(10, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            GameObject p0 = Instantiate(dissolve, transform.position, transform.rotation);
            ParticleSystem.MainModule p = p0.GetComponent<ParticleSystem>().main;
            p.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(gameObject);
            return;
        }
        if (hp > maxHp) hp = maxHp;
        StartCoroutine(FindTarget());
        if (attacking == null) {
            transform.position += transform.right*speed*Time.deltaTime;
        } else {
            Attack();
        }

    }

    private void Attack()
    {
        if (period > atkspd) {
            period = 0;
            attacking.GetComponent<Unit>().receiveDamage(atk);
            en = Math.Min(en+25, 100);
            GameObject s;
            if (rangedItem == null) s = Instantiate(strike, attacking.transform.position, transform.rotation);
            else s = Instantiate(rangedItem);
            s.GetComponent<SpriteRenderer>().sortingOrder = layer + 1;
        }
        period += Time.deltaTime;
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
