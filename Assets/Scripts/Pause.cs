using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private GameObject[] p;

    void Awake()
    {
        p = GameObject.FindGameObjectsWithTag("Paused");
        resume();
    }

    public void pause()
    {
        if (Player.dead) return;
        Time.timeScale = 0;
        foreach (GameObject g in p)
            g.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1;
        foreach (GameObject g in p)
            g.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
