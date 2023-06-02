using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static int money;
    private int startMoney = 100;
    public static int owned = 4; //TEMP, not including template
    public static int[] levels = new int[owned + 1];

    // Start is called before the first frame update
    void Awake()
    {
        money = startMoney;
        InvokeRepeating("generate", 0.5f, 0.5f);
        for (int i = 1; i <= owned; i++) levels[i] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generate()
    {
        money += 10;
    }
    
}
