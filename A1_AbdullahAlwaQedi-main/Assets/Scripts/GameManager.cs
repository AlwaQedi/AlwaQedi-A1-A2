using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    int score = 0;
    public static GameManager _instance;
    public GameObject PauseScreen;
    public Toggle muteBtn;
    public Slider volume;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        muteBtn.isOn = PlayerPrefs.GetInt("Mute", 1)==0;
        volume.value = PlayerPrefs.GetFloat("Volume", 1);
        if (!muteBtn.isOn)
            AudioListener.volume = volume.value;
        else
            AudioListener.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                PauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                PauseScreen.SetActive(false);
            }
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MuteSound()
    {
        if(muteBtn.isOn)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
            AudioListener.volume = PlayerPrefs.GetInt("Volume", 1);
        }
    }

    public void VolumeChange()
    {
        PlayerPrefs.SetFloat("Volume", volume.value);
        if (!muteBtn.isOn)
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
    }
}
