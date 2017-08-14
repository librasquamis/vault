using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWindow : MonoBehaviour {

    public int LevelNo;
    private GUITexture Overlay;
    private RoomManager RoomManager;
	// Use this for initialization
	void Start () {
        Overlay = GameObject.FindObjectOfType<GUITexture>();
        RoomManager = GameObject.FindObjectOfType<RoomManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    int[] cardCountArray = { 2,3,3,4,4,6,6,6,8,8, 8, 8 };
                           //1 2 3 4 5 6 7 8 9 10 11 12

    public void GoToLevel()
    {
        StartCoroutine(RoomManager.LevelInfoFadeIn());
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("cardCount", cardCountArray[LevelNo - 1]);
        PlayerPrefs.SetInt("level", LevelNo);
        SceneManager.LoadScene("Room1");
    }
}