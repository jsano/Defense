using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private bool toDestroy = false;
    private GameObject attacking; // First is current target
    private float[] ratios = {0, 1, 1.5f, 2.5f, 4, 6, 8.5f, 12};

    [Header("Info")]
    public int cost;
    private int level = 1;

    [Header("Stats")]
    public float speed;
    public float baseHp;
    private float hp;
    private float en = 0;
    public float baseAtk;
    public List<string> atkMods = new List<string>();
    public float baseAtkspd;
    public List<string> atkspdMods = new List<string>();
    private float period;
    public float range = 0.75f;

    [Header("Graphics")]
    public GameObject barContainer;
    public GameObject strike; 
    public GameObject rangedItem;
    public bool ranged;
    public GameObject dissolve;
    private int layer;

    void Start()
    {
        layer = GetComponent<SpriteRenderer>().sortingOrder;
        hp = baseHp;
        period = baseAtkspd/2;
        BarContainer bc = Instantiate(barContainer, transform, false).GetComponent<BarContainer>();
        if (speed == 0) bc.forCastle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            toDestroy = true;
            return;
        }
        hp = Math.Min(hp, baseHp * ratios[level]);
        Debug.Log(baseHp * ratios[level]);
        if (attacking == null) StartCoroutine(FindTarget());
        if (attacking == null) {
            period = getCombatAtkspd() / 2;
            transform.position += transform.right*speed*Time.deltaTime;
        } else {
            if (period > getCombatAtkspd()) {
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
        en = Math.Min(en+25, 100);
        GameObject s;
        if (ranged) {
            int direction = (tag == "Ally") ? 1 : -1;
            s = Instantiate(rangedItem, transform.position + new Vector3(direction * 0.5f, 0, 0), Quaternion.Euler(0, transform.eulerAngles.y, -90));
            s.GetComponent<RangedItem>().attacking = attacking;
            s.GetComponent<RangedItem>().dmg = getCombatAtk();
            s.tag = tag + "P";
        }
        else {
            s = Instantiate(strike, attacking.transform.position, transform.rotation);
            attacking.GetComponent<Unit>().receiveDamage(getCombatAtk());
        }
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

    void onMouseDown()
    {
        Debug.Log("Show info");
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
        en = Math.Min(en+1, 100);
    }

    public void levelUp()
    {
        level += 1;
        float toGain = baseHp * (ratios[level] - ratios[level-1]);
        hp += toGain;
    }
    
    public float getCurrentHP()
    {
        return hp;
    }

    public float getCombatMaxHP()
    {
        return baseHp * ratios[level];
    }

    public float getCurrentEn()
    {
        return en;
    }

    public float getCombatAtk()
    {
        float cur = baseAtk * ratios[level];
        foreach (string s in atkMods){
            if (s.StartsWith("+")) cur += float.Parse(s.Substring(1));
            else cur *= float.Parse(s.Substring(1));
        }
        return cur;
    }

    public float getCombatAtkspd()
    {
        float cur = baseAtkspd * ratios[level];
        foreach (string s in atkspdMods){
            if (s.StartsWith("+")) cur += float.Parse(s.Substring(1));
            else cur *= float.Parse(s.Substring(1));
        }
        return cur;
    }

    public int getCurrentLv()
    {
        return level;
    }

}
