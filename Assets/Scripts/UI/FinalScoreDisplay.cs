using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI myScoreText;
    public void DisplayScore(int score) 
    {
        myScoreText.text = "Score Final: " + score;
    }


}
