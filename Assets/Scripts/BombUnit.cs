using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BombUnit : Unit, ISelectHandler, IDeselectHandler
{

    protected override IEnumerator FindTarget()
    {
        GameObject[] targets;
        if (tag == "Ally") targets = GameObject.FindGameObjectsWithTag("Enemy");
        else targets = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject t in targets) {
            if (t.GetComponent<Unit>().getCombatSpeed() == 0 && Math.Abs(transform.position.x - t.transform.position.x) <= range) {
                // Enemy will start attacking 1 frame later than ally for QOL
                if (tag == "Enemy") yield return new WaitForSeconds(Time.deltaTime);
                attacking = t;
                break;
            }
        }
    }

}
