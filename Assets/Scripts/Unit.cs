using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 dest;
    [SerializeField] private float speed = 5f;
    private List<Collider2D> attacking; // First is current target
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private float atkspd;
    private float period;

    // Start is called before the first frame update
    void Start()
    {
        attacking = new List<Collider2D>();
        health = maxHealth;
        if (tag.CompareTo("Enemy") == 0) dest = new Vector3(-10, transform.position.y, 0); 
        else dest = new Vector3(10, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
            return;
        }
        if (attacking.Count == 0) {
            Vector3 dir = dest - transform.position;
            transform.Translate(dir.normalized*speed*Time.deltaTime);
        } else {
            if (period > atkspd) {
                period = 0;
                attacking[0].GetComponent<Unit>().dealDamage(2);
            }
            period += UnityEngine.Time.deltaTime;    
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && tag == "Unit" || other.tag == "Unit" && tag == "Enemy") {
            attacking.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        attacking.Remove(other);
    }

    public void dealDamage(int dmg) 
    {
        health -= dmg;
    }

}
