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
        int waves = GlobalVars.waves - 1;
        if (waves > 1 || waves == 0)
            tmp.text = "You Survived " + waves + " Waves";
        else
            tmp.text = "You Survived " + waves + " Wave";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
