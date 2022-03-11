using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxScoreDisplay : MonoBehaviour
{
    
    void Start()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        TextMeshProUGUI myScoreText = gameObject.GetComponent<TextMeshProUGUI>();

        myScoreText.text = "Max Score: " + maxScore;

    }

    
}
