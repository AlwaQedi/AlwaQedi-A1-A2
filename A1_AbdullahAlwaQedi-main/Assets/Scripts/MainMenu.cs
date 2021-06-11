using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle muteBtn;
    public Slider volume;
    // Start is called before the first frame update
    void Start()
    {

        muteBtn.isOn = PlayerPrefs.GetInt("Mute", 1) == 0;
        volume.value = PlayerPrefs.GetFloat("Volume", 1);
        if (!muteBtn.isOn)
            AudioListener.volume = volume.value;
        else
            AudioListener.volume = 0;
        //if (PlayerPrefs.GetInt("Mute", 1) != 0)
        //    AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        //else
        //    AudioListener.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void MuteSound()
    {
        if (muteBtn.isOn)
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
