using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public GameObject settingWindow;
    LevelManager Manager;
    AudioManager AM;
    public Button SoundBtn;
    public Button EffectBtn;

    public Sprite SoundOn;
    public Sprite SoundOff;
    public Sprite EffectOn;
    public Sprite EffectOff;

    void Start () {

    }

    void BtnDefaults()
    {
        AM = GameObject.FindObjectOfType<AudioManager>();
        var musicChoice = PlayerPrefs.GetInt("music");
        if (musicChoice == 0)
        {
            AM.StopMusic();
            SoundBtn.image.sprite = SoundOff;
        }
        else
        {
            AM.StartMusic();
            SoundBtn.image.sprite = SoundOn;
        }
        musicChoice = PlayerPrefs.GetInt("effect");
        if (musicChoice == 0)
        {
            PlayerPrefs.SetInt("effect", 0);
            EffectBtn.image.sprite = EffectOff;
        }
        else
        {
            PlayerPrefs.SetInt("effect", 1);
            EffectBtn.image.sprite = EffectOn;
        }
    }

    public void ShowSettings()
    {
        gameObject.SetActive(true);
        BtnDefaults();
    }

    public void HideSettings()
    {
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Replay()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
        Manager.Replay();
    }

    public void SoundToggle()
    {
        AM = GameObject.FindObjectOfType<AudioManager>();
        var musicChoice = PlayerPrefs.GetInt("music");
        if (musicChoice == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            AM.StopMusic();
            SoundBtn.image.sprite = SoundOff;
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            AM.StartMusic();
            SoundBtn.image.sprite = SoundOn;
        }
    }

    public void EffectToggle()
    {
        AM = GameObject.FindObjectOfType<AudioManager>();
        var musicChoice = PlayerPrefs.GetInt("effect");
        if (musicChoice == 1)
        {
            PlayerPrefs.SetInt("effect", 0);
            EffectBtn.image.sprite = EffectOff;
        }
        else
        {
            PlayerPrefs.SetInt("effect", 1);
            EffectBtn.image.sprite = EffectOn;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
