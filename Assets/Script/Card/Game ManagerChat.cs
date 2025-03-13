using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerChat : MonoBehaviour
{   
    public GameObject Uicard;
    public GameObject[] Deck;
    [SerializeField]private float[] condition;
    [SerializeField]private float[] stat;
    
    public GameObject panel;
    public Sprite[] AssetCard;
    HashSet<int> setUnik = new HashSet<int>();
    bool isClick=false;
    bool isChosed=false;
    private string cardName;
    [SerializeField]private float anxStat;


    void Awake()
    {
        anxStat=PlayerPrefs.GetFloat("anxStat");
       
        shapeVolume();
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
     if (isChosed)
     {
        isChosed=false;
        await Task.Delay(2000);
        StartCoroutine(Fade(cardName));
        await Task.Delay(500);
        panel.SetActive(false);
        Uicard.SetActive(false);
     }
    }   

    public IEnumerator HideCard(String name, float effect)
    {   
       
        cardName=name;
        GameObject.Find("Shuffle").SetActive(false);
        isChosed=true;
        for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                continue;
            }
            StartCoroutine(Fade(Deck[i].name));
        yield return null;
         this.anxStat-=effect;
        shapeVolume();
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
        if (setUnik.Count>=Deck.Length){
            setUnik.Clear();
        }
        int counter=0;
        int a=0;
        while (setUnik.Count<Deck.Length){   
            a=UnityEngine.Random.Range(0, Deck.Length);
            setUnik.Add(a);
        }
      
       foreach (int i in setUnik)
       {
           Deck[counter].GetComponent<SpriteRenderer>().sprite=AssetCard[i];
           Deck[counter].GetComponent<CardScript>().setAnx(anxStat);
           Deck[counter].GetComponent<CardScript>().setCond(condition[i]);
           Deck[counter].GetComponent<CardScript>().setStat(stat[i]);
           counter++;
       }
        yield return null;
    }

    IEnumerator GetIn(){
        foreach (var card in Deck){
            card.GetComponent<CardScript>().setAnx(anxStat);
            card.SetActive(true);
                SpriteRenderer spriteRenderer = GameObject.Find(card.name).GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 0);
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

    void FixedUpdate()
    {
         if(Input.GetKey(KeyCode.Escape)){
            PlayerPrefs.SetFloat("anxStat", anxStat);
            if (anxStat<0){
                anxStat=0;
            }
            if (anxStat>100){
            anxStat=100;
            }
            SceneManager.LoadScene("SC Chapter 1");
        }
    }

    public void Reshuffle(){
        StartCoroutine(ShuffleCard());
        StartCoroutine(GetIn());
    }

    public void shapeVolume(){
        GameObject volume=GameObject.Find("Volume");
        float presentase=volume.GetComponent<Image>().fillAmount;
        if (anxStat<=100) presentase=anxStat/100;
        volume.GetComponent<Image>().fillAmount=presentase;
    }

    
}