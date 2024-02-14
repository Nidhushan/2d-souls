using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    private void Update()
    {
        if (GlobalVars.waves > 1)
            tmp.text = "You Survived " + GlobalVars.waves + " Waves";
        else
            tmp.text = "You Survived " + GlobalVars.waves + " Wave";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
