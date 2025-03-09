using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerChat : MonoBehaviour
{   
    public GameObject Uicard;
    public GameObject[] Deck;
    
    public GameObject panel;
    void Start()
    {
        
    }
    void Update()
    {
     if (Input.GetMouseButtonDown(0))
     {
         Uicard.SetActive(true);
        panel.SetActive(true);
     }   
    }
    public void HideUnless(String name){
         for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                continue;
            }
            GameObject.Find(Deck[i].name).SetActive(false);
        }
    }

    IEnumerator HideCard(String name)
    {
        for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                continue;
            }
            GameObject.Find(Deck[i].name).SetActive(false);
            yield return null;
        }
    }

   
}
