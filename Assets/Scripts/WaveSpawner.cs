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
        new Spawn(1, 10, 2),
        new Spawn(1, 15, 2),
        new Spawn(1, 20, 3),
        new Spawn(1, 25, 3),
    };

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(castle, Constants.CASTLEPOS, Quaternion.identity);
        foreach (Spawn s in lv) {
            StartCoroutine(spawn(s));
        }
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

}
