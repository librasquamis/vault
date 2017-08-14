using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource BGM;
    public AudioClip cardTurn;
    public AudioClip walkAway;
    public AudioClip enterIn;
    public AudioClip patla;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        //StartMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CardTurn (){
        if (PlayerPrefs.GetInt("effect") == 1)
        {
            BGM.PlayOneShot(cardTurn);
        }
    }

    public void WalkAway()
    {
        if (PlayerPrefs.GetInt("effect") == 1)
        {
            BGM.PlayOneShot(walkAway);
        }
    }

    public void EnterIn()
    {
        if (PlayerPrefs.GetInt("effect") == 1)
        {
            BGM.PlayOneShot(enterIn);
        }
    }

    public void Patla()
    {
        if (PlayerPrefs.GetInt("effect") == 1)
        {
            BGM.PlayOneShot(patla,0.2f);
        }
    }

    public void StartMusic()
    {
        BGM.Play();
    }

    public void StopMusic()
    {
        BGM.Stop();
    }
}
