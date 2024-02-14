using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int currentWave = 0;
    public TextMeshProUGUI textMeshProUGUI;

    public int banditAmt = 0;
    public int bringerAmt = 0;

    public GameObject player;

    public Transform bringerSpawn;
    public Transform banditSpawn;

    public GameObject bringer;
    public GameObject bandit;

    private int amtOfBandits = 0;
    private int amtOfBringers = 0;

    private bool spawningBandits = false;
    private bool spawningBringers = false;

    // Update is called once per frame
    void Update()
    {
        GlobalVars.waves = currentWave;
        if (!spawningBandits && !spawningBringers)
        {
            amtOfBandits = GameObject.FindGameObjectsWithTag("Bandit").Length;
            amtOfBringers = GameObject.FindGameObjectsWithTag("Death").Length;
            if (amtOfBringers <= 0 && amtOfBandits <= 0)
            {
                currentWave += 1;
                player.GetComponent<HeroKnight>().ResetHealth();
                if (currentWave % 2 == 0)
                {
                    banditAmt++;
                    banditAmt = Math.Clamp(banditAmt, 0, 4);
                    StartCoroutine(SpawnBandits());
                }
                else
                {
                    bringerAmt++;
                    bringerAmt = Math.Clamp(bringerAmt, 0, 4);
                    StartCoroutine(SpawnBringers());
                }
                StartCoroutine(Display());
            }
        }
    }

    IEnumerator Display()
    {
        textMeshProUGUI.text = "Wave " + currentWave;
        yield return new WaitForSeconds(1f);
        textMeshProUGUI.text = "";
    }

    IEnumerator SpawnBringers()
    {
        spawningBringers = true;
        for (int i = 0; i < bringerAmt; i++)
        {
            Instantiate(bringer, bringerSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
        spawningBringers = false;
        amtOfBringers = GameObject.FindGameObjectsWithTag("Death").Length;
    }

    IEnumerator SpawnBandits()
    {
        spawningBandits = true;
        for (int i = 0; i < banditAmt; i++)
        {
            Instantiate(bandit, banditSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
        spawningBandits = false;
        amtOfBandits = GameObject.FindGameObjectsWithTag("Bandit").Length;
    }
}
