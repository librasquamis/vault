using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public Sprite[] CardFaces;
    public Sprite CardBack;
    public GameObject Card;
    public GameObject CardList;
    public Image FakeCard;
    public Text PairsLeft;
    private static GUITexture Overlay;
    private static GUIText OverlayTxt;
    private int CardCount;
    private GameObject[] cards;
    private List<CardController> openedCards;
    private List<CardController> foundCards;
    private int PairLeft;
    public GameObject TimeLimit;


    private void Awake()
    {
        if (Overlay == null)
        {
            Overlay = GameObject.FindObjectOfType<GUITexture>();
            OverlayTxt = GameObject.FindObjectOfType<GUIText>();
        }
        StartCoroutine(LevelInfoFadeOut());
        StartCoroutine(FadeTextToFullAlpha(1f,OverlayTxt));
    }
    void Start()
    {
        Settings settingWindow = GameObject.FindObjectOfType<Settings>();
        settingWindow.gameObject.SetActive(false);

        openedCards = new List<CardController>();
        foundCards = new List<CardController>();
        CardCount = PlayerPrefs.GetInt("cardCount") * 2;
        PairLeft = CardCount / 2;
        PairsLeft.text = "Ghosts";
        cards = new GameObject[CardCount];
        for (int i = 0; i < PairLeft; i++)
        {
            GameObject c = (GameObject)Instantiate(Card);
            var ctrl = c.GetComponent<CardController>();

            ctrl.CardValue = i % CardFaces.Length;
            ctrl.CardFace = CardFaces[ctrl.CardValue];
            ctrl.CardBack = CardBack;
            c.GetComponent<Image>().sprite = CardBack;
            cards[i] = c;
            var pair = (GameObject)Instantiate(c);
            cards[i + PairLeft] = pair;
        }

        new System.Random().Shuffle(cards);

        foreach (GameObject item in cards)
        {
            item.transform.SetParent(CardList.transform);
            var rect = item.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1);
        }
    }

    public void Replay()
    {
        foreach (var item in cards)
        {
            Destroy(item);
        }
        Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Check(CardController card)
    {
        if (!foundCards.Contains(card) && !openedCards.Contains(card))
            openedCards.Add(card);
        if (openedCards.Count == 2)
        {
            if (openedCards[0].CardValue == openedCards[1].CardValue)
            {
                PairLeft--;
                if (PairLeft == 0) StartCoroutine(FinishLevel());
                foundCards.Add(openedCards[0]);
                foundCards.Add(openedCards[1]);
                openedCards[0].Kaybol();
                openedCards[1].Kaybol();
                openedCards.Clear();
            }
            else
            {
                openedCards[0].TersCevir();
                openedCards[1].TersCevir();
                openedCards.Clear();
            }
        }
    }

    public IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(LevelFadeOut());
        StartCoroutine(FadeTextIn(2, OverlayTxt));
        StartCoroutine(GoToMansion());
    }

    public IEnumerator GoToMansion()
    {
        yield return new WaitForSeconds(2);
        int curLevel = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("lastUnlockedLevel", curLevel + 1);
        SceneManager.LoadScene("Levels");
    }

    public Sprite GetCardBack()
    {
        return CardBack;
    }

    public Sprite GetCardFace(int i)
    {
        return CardFaces[i];
    }

    public IEnumerator LevelInfoFadeOut()
    {
        Overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        float progress = 0f;
        Overlay.color = Color.black;
        while (progress < 1f)
        {
            Overlay.color = Color.Lerp(Color.black, Color.clear, progress);
            progress +=   Time.deltaTime;
            yield return null;
        }
        Overlay.color = Color.clear;
        Overlay.gameObject.SetActive(false);
    }

    public IEnumerator LevelFadeOut()
    {
        AudioManager AM = GameObject.FindObjectOfType<AudioManager>();
        if (AM != null)
            AM.WalkAway();

        OverlayTxt.color = Color.white;
        Overlay.gameObject.SetActive(true);
        Overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        float progress = 0f;
        Overlay.color = Color.clear;
        while (progress < 1f)
        {
            Overlay.color = Color.Lerp(Color.clear, Color.black, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        Overlay.color = Color.black;
       
    }

    public IEnumerator FadeTextToFullAlpha(float t, GUIText i)
    {
        i.text = "Entering room...";
        i.color = Color.white;
        float progress = 0f;
        while (progress < 1f)
        {
            i.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator FadeTextIn(float t, GUIText i)
    {
        i.text = "Walking away...";
        i.color = Color.clear;
        float progress = 0f;
        while (progress < 1f)
        {
            i.color = Color.Lerp(Color.clear, Color.white, progress);
            progress += Time.deltaTime;
            yield return null;
        }
    }
}
static class RandomExtensions
{
    public static void Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
