using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private AudioManager myAudioManager;
    List<Sound> pausedSounds;

    public void Awake()
    {
        HideCanvas();
        myAudioManager = AudioManager.instance;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void DisplayCanvas()
    {

        gameObject.SetActive(true);

    }

    void HideCanvas()
    {

        gameObject.SetActive(false);

    }

    void PauseTime()
    {
        Time.timeScale = 0;
    }

    void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void Pause() 
    {
        PauseTime();
        DisplayCanvas();


        pausedSounds = myAudioManager.PauseCurrentSounds();
        if(myAudioManager.playingSoundTrack != null) myAudioManager.playingSoundTrack.source.volume *= 0.5f;
    }

    public void ResumeGame() 
    {
        ResumeTime();
        HideCanvas();


        if (myAudioManager.playingSoundTrack != null)  myAudioManager.playingSoundTrack.source.volume *= 2f;

        foreach (Sound s in pausedSounds) 
        {
            s.source.UnPause();
        }

        pausedSounds.Clear();
    }


}
