using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BombUnit : Unit, ISelectHandler, IDeselectHandler
{

    /*private bool toDestroy = false;
    private GameObject attacking; // First is current target

    [Header("Info")]
    [SerializeField] private int cost;
    [SerializeField] private int upgradeCost;
    public int ID;

    [Header("Stats")]
    [SerializeField] private float speed;
    public List<string> speedMods = new List<string>();
    [SerializeField] private float baseHp;
    private float hp;
    private float en = 0;
    [SerializeField] private float baseAtk;
    public List<string> atkMods = new List<string>();
    [SerializeField] private float baseAtkspd;
    public List<string> atkspdMods = new List<string>();
    private float period;
    [SerializeField] private float range = 0.75f;

    [Header("Graphics")]
    public GameObject barContainer;
    public GameObject strike; 
    public GameObject rangedItem;
    [SerializeField] private bool ranged;
    public GameObject dissolve;
    public GameObject unitInfo;
    private GameObject currentUI;
    public GameObject rangeInfo;
    private GameObject currentRI;
    private bool displaying = false;
    private int layer;
    private Color baseColor;

    void Start()
    {
        layer = GetComponent<SpriteRenderer>().sortingOrder;
        hp = baseHp;
        period = baseAtkspd/2;
        BarContainer bc = Instantiate(barContainer, transform, false).GetComponent<BarContainer>();
        if (speed == 0) bc.forCastle = true;
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            toDestroy = true;
            return;
        }
        if (displaying) updateUI();
        hp = Math.Min(hp, getCombatMaxHP());
        if (attacking == null) StartCoroutine(FindTarget());
        if (attacking == null) {
            period = getCombatAtkspd() / 2;
            if (tag == "Enemy" || tag == "Ally" && transform.position.x < Constants.ENEMYX - 10)
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
            p.startColor = baseColor;
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
    }*/

    protected override IEnumerator FindTarget()
    {
        GameObject[] targets;
        if (tag == "Ally") targets = GameObject.FindGameObjectsWithTag("Enemy");
        else targets = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject t in targets) {
            if (t.GetComponent<Unit>().getCombatSpeed() == 0 && Math.Abs(transform.position.x - t.transform.position.x) <= range) {
                // Enemy will start attacking 1 frame later than ally for QOL
                if (tag == "Enemy") yield return new WaitForSeconds(Time.deltaTime);
                attacking = t;
                break;
            }
        }
    }

    /*public void OnSelect(BaseEventData baseEventData)
    {
        currentUI = Instantiate(unitInfo, transform, false);
        currentRI = Instantiate(rangeInfo, transform, false);
        currentRI.GetComponent<SpriteRenderer>().sortingOrder = layer + 1;
        displaying = true;
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.color = new Color(s.color.r + 0.5f, s.color.g + 0.5f, s.color.b - 0f);
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        if (currentUI != null) Destroy(currentUI);
        if (currentRI != null) Destroy(currentRI);
        displaying = false;
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.color = new Color(s.color.r - 0.5f, s.color.g - 0.5f, s.color.b + 0f);
    }

    void updateUI() {
        currentUI.transform.Find("Atk").GetComponent<Text>().text = getCombatAtk() + "";
        currentUI.transform.Find("HP").GetComponent<Text>().text = getCurrentHP() + " / " + getCombatMaxHP();
        currentUI.transform.Find("Atkspd").GetComponent<Text>().text = getCombatAtkspd() + "";
        currentUI.transform.Find("Speed").GetComponent<Text>().text = getCombatSpeed() + "";
        currentRI.transform.localScale = new Vector3(range*2, range*2, 0);
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
        en = Math.Min(en+1, 100);
    }

    public void levelUpHp()
    {
        float toGain = baseHp * (Constants.ratios[getCurrentLv()+1] - Constants.ratios[getCurrentLv()]);
        hp += toGain;
    }
    
    public float getCurrentHP()
    {
        return hp;
    }

    public float getCombatMaxHP()
    {
        return baseHp * Constants.ratios[getCurrentLv()];
    }

    public float getCurrentEn()
    {
        return en;
    }

    public float getCombatSpeed()
    {
        float cur = speed;
        foreach (string s in speedMods){
            if (s.StartsWith("+") && speed > 0) cur += float.Parse(s.Substring(1));
            else cur *= float.Parse(s.Substring(1));
        }
        return cur;
    }

    public float getCombatAtk()
    {
        float cur = baseAtk * Constants.ratios[getCurrentLv()];
        foreach (string s in atkMods){
            if (s.StartsWith("+")) cur += float.Parse(s.Substring(1));
            else cur *= float.Parse(s.Substring(1));
        }
        return cur;
    }

    public float getCombatAtkspd()
    {
        float cur = baseAtkspd;
        foreach (string s in atkspdMods){
            if (s.StartsWith("+")) cur += float.Parse(s.Substring(1));
            else cur *= float.Parse(s.Substring(1));
        }
        return cur;
    }

    public int getCurrentLv()
    {
        return Player.levels[ID];
    }

    public int getCombatCost()
    {
        return (int) (cost * Constants.ratios[getCurrentLv()]);
    }

    public int getUpgradeCost()
    {
        return (int) (upgradeCost * Constants.ratios[getCurrentLv()]);
    }*/

}
