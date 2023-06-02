using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{

    readonly static public float MAXCAMERAX = 30;
    
    readonly static public Vector3 CASTLEPOS = Camera.main.ScreenToWorldPoint(new Vector3(200, Camera.main.pixelHeight / 4, 10));

    readonly static public float ALLYX = CASTLEPOS.x - 3;
    
    readonly static public float GROUNDY = CASTLEPOS.y;
    
    readonly static public float AIRY = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight * 3 / 4, 10)).y;

    readonly static public float ENEMYX = ALLYX + MAXCAMERAX + 25;

    readonly static public float[] ratios = {1, 1, 1.5f, 2.5f, 4, 6, 8.5f, 12};

}
