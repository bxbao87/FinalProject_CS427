using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject[] cardSlots;

    private void DisplayCards()
	{
        for (int i = 0; i < cardManager.Cards.Count; ++i)
		{
            cardSlots[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = cardManager.Cards[i].CardName;
            cardSlots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = cardManager.Cards[i].CardBackground;
            cardSlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = cardManager.Cards[i].CardCharacter;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
