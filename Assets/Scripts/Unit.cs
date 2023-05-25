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
    public float atk;
    public float atkspd;
    private float period;

    [Header("Graphics")]
    private float barLength = 1;
    private float barWidth = 0.1f;
    public GameObject barContainer;
    public GameObject hpBar;
    public GameObject enBar;
    public GameObject strike;

    // Start is called before the first frame update
    void Start()
    {
        attacking = new List<Collider2D>();
        hp = maxHp;
        Transform bc = Instantiate(barContainer).transform;
        bc.transform.position = new Vector3(0, 0.935f, 0);
        bc.localScale = new Vector3(barLength + 0.02f, 2*barWidth+0.05f, 0);
        bc.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        bc.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        bc.SetParent(transform, false);
        makeBar(hpBar, "HP", 1);
        makeBar(enBar, "EN", 0.87f);
        if (tag.CompareTo("Enemy") == 0) dest = new Vector3(-10, transform.position.y, 0); 
        else dest = new Vector3(10, transform.position.y, 0);
    }

    private void makeBar(GameObject _bar, string name, float height)
    {
        GameObject pivot = new GameObject();
        pivot.name = name + "Pivot";
        pivot.transform.position = new Vector3(-0.5f, height, 0);
        pivot.transform.SetParent(transform, false); // Update world position
        Transform bar = Instantiate(_bar).transform;
        bar.transform.position = new Vector3(0.5f, 0, 0);
        bar.SetParent(pivot.transform, false);
        if (name == "EN")
            bar.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        bar.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        bar.localScale = new Vector3(barLength, barWidth, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            Destroy(gameObject);
            return;
        }
        Transform bar = transform.Find("HPPivot");
        Vector3 temp = bar.localScale;
        bar.localScale = new Vector3(barLength * (hp/maxHp), temp.y, temp.z);
        if (attacking.Count == 0) {
            Vector3 dir = dest - transform.position;
            transform.Translate(Vector3.Scale(dir.normalized, transform.right)*speed*Time.deltaTime);
        } else {
            Attack();
        }

    }

    private void Attack()
    {
        if (period > atkspd) {
            period = 0;
            attacking[0].GetComponent<Unit>().receiveDamage(atk);
            Instantiate(strike, transform.position + 0.5f*transform.right, transform.rotation);
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
