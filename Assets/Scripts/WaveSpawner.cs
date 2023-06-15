using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject[] srcPrefabs;
    public GameObject castle;

    private class Spawn {
        public int id;
        public float time; // Seconds
        public int level;
        public Spawn(int _id, float _time, int _level) {
            id = _id;
            time = _time;
            level = _level;
        }
    }

    private Spawn[] lv = {
        new Spawn(1, 0, 1),
        new Spawn(1, 5, 1),
        new Spawn(1, 10, 1),
        new Spawn(1, 11, 1),
        new Spawn(1, 12, 1),
        new Spawn(1, 13, 1),
        new Spawn(1, 15, 2),
        new Spawn(1, 20, 2),
        new Spawn(1, 25, 2),
        new Spawn(1, 26, 2),
        new Spawn(1, 30, 3),
        new Spawn(1, 35, 3),
        new Spawn(1, 40, 3),
        new Spawn(1, 41, 3),
    };

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(castle, Constants.CASTLEPOS, Quaternion.identity);
        //foreach (Spawn s in lv) StartCoroutine(spawn(s));
        InvokeRepeating("spawnEndless", 0, 5);
    }

    private IEnumerator spawn(Spawn s)
    {
        yield return new WaitForSeconds(s.time);
        GameObject e = Instantiate(srcPrefabs[s.id]);
        e.transform.position = new Vector3(Constants.ENEMYX, Constants.GROUNDY, 0);
        for (int i = 1; i < s.level; i++) {
            e.GetComponent<Unit>().levelUpHp();
            e.GetComponent<Unit>().enemyLevel += 1;
        }
    }

    private void spawnEndless()
    {
        if (Player.dead) return;
        GameObject e = Instantiate(srcPrefabs[(int)(Random.Range(1, Player.eowned + 1))]);
        e.transform.position = new Vector3(Constants.ENEMYX, Constants.GROUNDY, 0);
        for (int i = 1; i < Mathf.Min(Constants.ratios.Length - 1, Time.realtimeSinceStartup / 30); i++) {
            e.GetComponent<Unit>().levelUpHp();
            e.GetComponent<Unit>().enemyLevel += 1;
        }
    }

}
