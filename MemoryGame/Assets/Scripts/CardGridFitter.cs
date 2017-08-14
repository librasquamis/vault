using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridFitter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var cardCount = PlayerPrefs.GetInt("cardCount");
        var grid = this.gameObject.GetComponent<GridLayoutGroup>();
        if (cardCount <=3)
        {
            grid.constraintCount = 2;
        }
        else if (cardCount ==4)
        {
            grid.constraintCount = 3;
        }
        else if (cardCount <= 6)
        {
            grid.constraintCount = 3;
            grid.spacing = new Vector2(5f, 5f);
        }
        else if (cardCount <= 8)
        {
            grid.constraintCount = 4;
            grid.spacing = new Vector2(2f, 2f);
        }

        grid.cellSize = new Vector2(Screen.width / 4.2f, Screen.height / 4.2f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
