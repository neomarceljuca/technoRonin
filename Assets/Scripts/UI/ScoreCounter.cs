using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    TextMeshProUGUI myScoreText;

    int score;
    int displayScore;
    public Player thePlayer;
    public FinalScoreDisplay myFSD;

    // Start is called before the first frame update
    void Start()
    {
        myScoreText = gameObject.GetComponent<TextMeshProUGUI>();
        score = displayScore = 0;

        StartCoroutine(ScoreUpdater());
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {

                displayScore++; //Increment the display score by 1
                myScoreText.text = "Score: " + displayScore.ToString(); //Write it to the UI

            if (thePlayer.gameOver)
            {
                myFSD.DisplayScore(displayScore);

                if (PlayerPrefs.GetInt("MaxScore", 0) < displayScore) 
                {
                    PlayerPrefs.SetInt("MaxScore", displayScore);
                    PlayerPrefs.Save();
                }
                
                
            }


            yield return new WaitForSeconds(0.5f); 
        }
    }

    public void UpdateScore(int points) 
    {
        displayScore += points;
    }
}
