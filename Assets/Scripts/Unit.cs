using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 dest;
    
    private List<Collider2D> attacking; // First is current target

    [Header("Stats")]
    public float speed = 5f;
    public float maxHp;
    private float hp;
    private float hpbarlen = 1;
    public float atk;
    public float atkspd;
    private float period;

    // Start is called before the first frame update
    void Start()
    {
        attacking = new List<Collider2D>();
        hp = maxHp;
        Transform bar = transform.Find("HP");
        Vector3 temp = bar.transform.localScale;
        bar.transform.localScale = new Vector3(hpbarlen, temp.y, temp.z);
        if (tag.CompareTo("Enemy") == 0) dest = new Vector3(-10, transform.position.y, 0); 
        else dest = new Vector3(10, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            Destroy(gameObject);
            return;
        }
        Transform bar = transform.Find("HP");
        Vector3 temp = bar.transform.localScale;
        bar.transform.localScale = new Vector3(hpbarlen * (hp/maxHp), temp.y, temp.z);
        if (attacking.Count == 0) {
            Vector3 dir = dest - transform.position;
            transform.Translate(dir.normalized*speed*Time.deltaTime);
        } else {
            Attack();
        }

    }

    private void Attack()
    {
        if (period > atkspd) {
            period = 0;
            attacking[0].GetComponent<Unit>().receiveDamage(atk);
        }
        period += Time.deltaTime;
    }

    public void receiveDamage(float dmg) 
    {
        hp -= dmg;
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

}
