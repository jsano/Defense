using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static int money;
    private int startMoney = 100;
    public static int owned = 4; //TEMP, not including template
    public static int eowned = 3; //TEMP, not including template
    public static int[] levels = new int[owned + 1];
    public static int kills;
    public static bool dead;

    private static GameObject[] p;

    void Awake()
    {
        money = startMoney;
        dead = false;
        kills = 0;
        InvokeRepeating("generate", 0.5f, 0.5f);
        for (int i = 1; i <= owned; i++) levels[i] = 1;
        p = GameObject.FindGameObjectsWithTag("GameOver");
        foreach (GameObject g in p)
            g.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void died()
    {
        foreach (GameObject g in p)
            g.SetActive(true);
    }

    private void generate()
    {
        if (!dead) money += 10;
    }
    
}
