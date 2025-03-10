using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    bool isClick=false;

    void Awake()
    {
        
    }
    void Start()
    {
       
    }
    async Task Update()
    {
     if (Input.GetMouseButtonDown(0)&&!isClick)
     {
        isClick=true;
        StartCoroutine(ShuffleCard());
        await Task.Delay(100);
        Uicard.SetActive(true);
        panel.SetActive(true);
        StartCoroutine(GetIn());
     }   
    }
   

    public IEnumerator HideCard(String name)
    {
        for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                continue;
            }
            StartCoroutine(Fade(Deck[i].name));
        yield return null;
        }
    }

    IEnumerator Fade(String name){
        float t=0;
        while(t<0.2f){
                t+=Time.deltaTime;
                SpriteRenderer spriteRenderer = GameObject.Find(name).GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
                Color fadeColor=spriteRenderer.color;
                fadeColor.a = Mathf.Lerp(1f, 0, t/0.2f);
                spriteRenderer.color = fadeColor;
                yield return null;
            }
        GameObject.Find(name).SetActive(false);
        
    }



    public IEnumerator ShuffleCard(){
        if (setUnik.Count>=Deck.Length)
        {
            setUnik.Clear();
        }
        int counter=0;
        int a=0;
        while (setUnik.Count<Deck.Length)
        {   
            a=UnityEngine.Random.Range(0, Deck.Length);
            setUnik.Add(a);
        }
      
       foreach (int i in setUnik)
       {
           Deck[counter].GetComponent<SpriteRenderer>().sprite=AssetCard[i];
           counter++;
       }
        yield return null;
    }

    IEnumerator GetIn(){
        foreach (var card in Deck){
                StartCoroutine(FadeIn(card.name));
                yield return null;
            }
        }

    IEnumerator FadeIn(String name){
    float t=0;
    while(t<0.5f){     
        t+=Time.deltaTime;
        SpriteRenderer spriteRenderer = GameObject.Find(name).GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
        Color fadeColor=spriteRenderer.color;
        fadeColor.a = Mathf.Lerp(0f, 1, t/0.5f);
        spriteRenderer.color = fadeColor;
        yield return null;
    }
}

    public void Reshuffle(){
        StartCoroutine(ShuffleCard());
        StartCoroutine(GetIn());
    }

    
}