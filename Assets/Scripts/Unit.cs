using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 dest;
    
    private List<Collider2D> attacking; // First is current target

    [Header("Stats")]
    public float speed = 5f;
    public float maxHp;
    private float hp;
    private float en = 0;
    public float atk;
    public float atkspd;
    private float period;

    [Header("Graphics")]
    public GameObject barContainer;
    public GameObject strike;
    public GameObject dissolve;

    // Start is called before the first frame update
    void Start()
    {
        attacking = new List<Collider2D>();
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
        if (attacking.Count == 0) {
            transform.position += transform.right*speed*Time.deltaTime;
        } else {
            Attack();
        }

    }

    private void Attack()
    {
        if (period > atkspd) {
            period = 0;
            attacking[0].GetComponent<Unit>().receiveDamage(atk);
            en = Math.Min(en+25, 100);
            Instantiate(strike, transform.position + 0.8f*transform.right, transform.rotation);
        }
        period += Time.deltaTime;
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
        en = Math.Min(en+1, 100);
    }    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && tag == "Ally") {
            attacking.Add(other);
        }
        if (other.tag == "Ally" && tag == "Enemy") {
            StartCoroutine(temp(other));
        }
        
    }

    // Enemy will start attacking 1 frame later than ally for QOL
    private IEnumerator temp(Collider2D other){
        yield return new WaitForSeconds(Time.deltaTime);
        attacking.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        attacking.Remove(other);
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
