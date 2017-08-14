using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{

    public int CardValue;
    public int State;

    public Sprite CardBack;
    public Sprite CardFace;
    public GameObject explosion;

    private LevelManager Manager;

    void Start()
    {
        State = 0;
        Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
    }

    public void TersCevir()
    {
        StartCoroutine(ArkaYuz());
    }

    IEnumerator ArkaYuz()
    {
        yield return new WaitForSeconds(1);
        State = 0;
        GetComponent<Image>().sprite = CardBack;
    }

    public void DuzCevir()
    {
        AudioManager AM = GameObject.FindObjectOfType<AudioManager>();
        if (AM != null)
            AM.CardTurn();

        State = 1;
        GetComponent<Image>().sprite = CardFace;
        Manager.Check(this);
    }

    public void Kaybol()
    {
        StartCoroutine(Patlama());
        
    }

    IEnumerator Patlama()
    {
        AudioManager AM = GameObject.FindObjectOfType<AudioManager>();
        if (AM != null)
            AM.Patla();
        Canvas c = FindObjectOfType<Canvas>();
        GameObject w = GameObject.Instantiate(explosion,c.transform);
        RectTransform rect = w.GetComponent<RectTransform>();
        rect.transform.position = this.transform.position;
        rect.localScale = new Vector2(40f, 40f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Kaybolus());
        yield return new WaitForSeconds(0.5f);
        DestroyObject(w);
    }

    IEnumerator Kaybolus()
    {
        while (this.transform.localScale != new Vector3(0f, 0f, 0f))
        {
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

}