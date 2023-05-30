using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static int money;
    private int startMoney = 50;
    public static int owned = 5; //TEMP

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        InvokeRepeating("generate", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generate()
    {
        money += 5;
    }
    
}
