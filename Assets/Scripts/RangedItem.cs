using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedItem : MonoBehaviour
{

    public GameObject attacking;
    public float speed;
    private float lifetime = 1;
    private float period;
    public GameObject dissolve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        period += Time.deltaTime;
        if (period >= lifetime) Destroy(gameObject);
        Vector3 dir;
        if (attacking == null) {
            if (tag == "EnemyP") dir = new Vector3(-1, 0, 0); 
            else dir = new Vector3(1, 0, 0);
        }
        else dir = (attacking.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (tag == "AllyP" && other.tag == "Enemy" || tag == "EnemyP" && other.tag == "Ally") {
            GameObject p0 = Instantiate(dissolve, transform.position, transform.rotation);
            ParticleSystem.MainModule p = p0.GetComponent<ParticleSystem>().main;
            p.startColor = GetComponent<SpriteRenderer>().color;
            p0.GetComponent<ParticleSystemRenderer>().sortingOrder = attacking.layer + 1;
            Destroy(gameObject);
        }
    }

}
