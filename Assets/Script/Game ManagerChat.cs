using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerChat : MonoBehaviour
{   
    public GameObject Uicard;
    public GameObject[] Deck;
    
    public GameObject panel;
    public Sprite[] AssetCard;
    HashSet<int> setUnik = new HashSet<int>();

    void Awake()
    {
        StartCoroutine(ShuffleCard());
    }
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
          GameObject.Find(name).SetActive(false);
    }

    public IEnumerator HideCard(String name)
    {
        Debug.Log(name+" Target");
        for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                Debug.Log(Deck[i].name+" Unless");
                continue;
            }
            StartCoroutine(GameObject.Find(Deck[i].name).GetComponent<CardScript>().Fade());
            HideUnless(Deck[i].name);
            yield return null;
        }
    }
    public IEnumerator ShuffleCard(){
        int counter=0;
        int a=0;
        while (setUnik.Count<Deck.Length)
        {   
            a=UnityEngine.Random.Range(0, Deck.Length);
            setUnik.Add(a);
        }
        Debug.Log(setUnik.Count);
       foreach (int i in setUnik)
       {
           Deck[counter].GetComponent<SpriteRenderer>().sprite=AssetCard[i];
           counter++;
       }
        yield return null;
    }

   
}
