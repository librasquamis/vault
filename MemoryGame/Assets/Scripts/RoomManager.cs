using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    public Button[] windowBtn;
    public Sprite windowOn;
    public AudioManager audioManager;
    public Text SwitchRoomTxt;
    private static GUITexture Overlay;

    static AudioManager AM;

    private void Awake()
    {

    }
    void Start()
    {
        if (Overlay == null)
        {
            Overlay = GameObject.FindObjectOfType<GUITexture>();
            Overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
            Overlay.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("effect"))
                PlayerPrefs.SetInt("effect",1);
            if (!PlayerPrefs.HasKey("music"))
                PlayerPrefs.SetInt("music", 1);

        }
        if (AM == null) 
            AM = GameObject.Instantiate(audioManager);
        
        var musicChoice = PlayerPrefs.GetInt("music");
        if (musicChoice == 0)
            AM.StopMusic();
        else
            AM.StartMusic();

        StartCoroutine(LevelInfoFadeOut());

        int lastUnlockedLevel = 1;
        lastUnlockedLevel = PlayerPrefs.GetInt("lastUnlockedLevel");
        if (lastUnlockedLevel == 0) lastUnlockedLevel = 1;
        for (int i = 0; i < 12; i++)
        {
            Button w = windowBtn[i];
            w.GetComponent<LevelWindow>().LevelNo = i + 1;
            var yazi = w.GetComponentInChildren<Text>();
            yazi.text = (i+1).ToString();
            if (i < lastUnlockedLevel)
            {
                w.image.sprite = windowOn;
            }
        }
    }

    public IEnumerator LevelInfoFadeIn()
    {
       
        AM.EnterIn();

        Overlay.gameObject.SetActive(true);
        Overlay.color = Color.clear;
        var overtxt = Overlay.GetComponentInChildren<GUIText>();
        overtxt.text = "Entering room...";
        overtxt.color = Color.clear;
        float progress = 0f;
        while (progress < 1f)
        {
            Overlay.color = Color.Lerp(Color.clear, Color.black, progress);
            overtxt.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += 3 * Time.deltaTime;
            yield return null;
        }
        overtxt.color = Color.white;
        Overlay.color = Color.black;
    }

    public IEnumerator LevelInfoFadeOut()
    {

        Overlay.gameObject.SetActive(true);
        Overlay.color = Color.black;
        var overtxt = Overlay.GetComponentInChildren<GUIText>();
        overtxt.text = "Walking away...";
        overtxt.color = Color.white;
        float progress = 0f;
        while (progress < 1f)
        {
            Overlay.color = Color.Lerp(Color.black, Color.clear, progress);
            overtxt.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += 3 * Time.deltaTime;
            yield return null;
        }
        overtxt.color = Color.clear;
        Overlay.color = Color.clear;
        Overlay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishGame()
    {

    }
}
