using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public GameObject Uicard;
    public GameObject[] Deck;
    [SerializeField]private float[] condition;
    [SerializeField]private float[] stat;
    [SerializeField]private int[] NodeIndex;
    
    public GameObject panel;
    public Sprite[] AssetCard;
    private bool inChoice=false;
    HashSet<int> setUnik = new HashSet<int>();
    bool isClick=false;
    bool isChosed=false;
    private string cardName;
    [SerializeField]private float anxStat;
    Choice nextNode;
    public TextMeshProUGUI timer;
    [Header("UI Elements")]
    [SerializeField] TMP_Text speakerNametext;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject choicePanel;
    //[SerializeField] Button choiceButtonPrefab;
    [SerializeField] public Button progresButton;

    [Header("Protagonist")]
    [SerializeField] Image leftImage;
    [SerializeField] Image rightImage;
    //[SerializeField] Image centerImage;
    [SerializeField] bool deActiveLeftImage;
    [SerializeField] bool deActiveRightImage;
    //[SerializeField] bool deActiveCenterImage;

    [Header("Audio")]
    public AudioSource voiceAudioSource;
    public AudioSource effectAudioSource;

    [Header("Setting")]
    [SerializeField] float textSpeed = 0.05f;
    DialogNode currentNode;
    int currentLineIndex = 0;
    bool isTyping = false;
    public GameObject shuffleBtn;
    float time=10;
    
    void Awake()
    {
        anxStat=PlayerPrefs.GetFloat("anxStat");
       
        shapeVolume();
    }
    void Start()
    {
        timer.SetText("");
        dialogPanel.SetActive(false);
        //choicePanel.SetActive(false);

        //Logic progress button
        progresButton.onClick.AddListener(OnClickAdvance);

        //hide image
        if(leftImage != null && deActiveLeftImage) leftImage.color = new Color32(255, 255, 255, 0);
        if(rightImage != null && deActiveRightImage) rightImage.color = new Color32(255, 255, 255, 0);
        //if(centerImage != null && deActiveCenterImage) centerImage.color = new Color32(255, 255, 255, 0);
    }

    public void StartDialog(DialogNode startNode) 
    {
        dialogPanel.SetActive(true);
        currentNode = startNode;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        if (currentNode == null || currentNode.lines.Length == 0)
        { 
            //END DIALOG

            return;
        }
        if (currentLineIndex < currentNode.lines.Length)
        {
            DialogLine line = currentNode.lines[currentLineIndex];
            speakerNametext.text = line.speakerName;
            Image targetImage = GetTargetImage(line.targetImage);
            if(targetImage != null && line.charSprite != null)
            {
                targetImage.sprite = line.charSprite;
                targetImage.color = Color.white;
            }
            //PLay Audio

            //Start Corroutin
            StartCoroutine(AnimateAndType(line, targetImage));

        }
        else
        {
            DisplayChoice();
        }
    }

    private Image GetTargetImage(DialogTarget targetImage)
    {
        switch(targetImage)
        {
            case DialogTarget.RightImage: return rightImage;
            case DialogTarget.LeftImage: return leftImage;
            //case DialogTarget.CenterImage: return centerImage;
            default: return null;
        }
    }

    IEnumerator AnimateAndType (DialogLine line, Image targetImage)
    {
        if(line.animationType != Animation.None)
        {
            yield return new WaitForSeconds(line.durasi);
        }
        yield return StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";
        int visibleCharCount = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if(text[i] == '<')
            {
                int closingTagIndex = text.IndexOf('>', i);
                if (closingTagIndex != -1)
                {
                    dialogText.text += text.Substring(i, closingTagIndex - i + 1);
                    i = closingTagIndex;
                }
            }
            dialogText.text += text[i];
            visibleCharCount++;

            dialogText.maxVisibleCharacters = visibleCharCount;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void PlayAudio(DialogLine line)
    {
        if(voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }

        if(line.spokenText != null)
        {
            voiceAudioSource.clip = line.spokenText;
            voiceAudioSource.Play();
        }

        if (line.moorOrEffect != null)
        {
            effectAudioSource.clip = line.moorOrEffect;
            voiceAudioSource.Play();
        }
    }

    public void OnClickAdvance()
    {
        if(isTyping)
        {
            StopAllCoroutines();
            DialogLine currentLine = currentNode.lines[currentLineIndex];
            dialogText.text = currentLine.text;
            dialogText.maxVisibleCharacters = currentLine.text.Length;
            isTyping = false;
            if (voiceAudioSource.isPlaying)
            {
                voiceAudioSource.Stop();
            }
        }else if(inChoice){

        }
        else
        {
            currentLineIndex++;
            DisplayCurrentLine();
            
        }
    }
    public IEnumerator HideCard(String name, float effect, int nextNodeIndex)
    {   
        timer.SetText("");
        int chosedCArdIndex=0;
        cardName=name;
        shuffleBtn.SetActive(false);
        isChosed=true;
        for (int i = 0; i < Deck.Length; i++)
        {   
            if (Deck[i].name==name){
                chosedCArdIndex=i;
                continue;
            }
            StartCoroutine(Fade(Deck[i].name));
        yield return null;
        }
        nextNode=currentNode.nextNodeIndex(nextNodeIndex);
        this.anxStat-=effect;
        shapeVolume();
        Deck[chosedCArdIndex].GetComponent<CardScript>().rePosition();
    }


    IEnumerator Fade(String name){
        if (name=="")
        {
            
        }else{
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
           Deck[counter].GetComponent<CardScript>().setNextNode(i);
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
    if (name!="")
    {
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
        anxStat+=3;
        shapeVolume();
        time-=2;
        StartCoroutine(ShuffleCard());
        StartCoroutine(GetIn());
    }

    public void shapeVolume(){
        GameObject volume=GameObject.Find("Volume");
        float presentase=volume.GetComponent<Image>().fillAmount;
        if (anxStat<=100) presentase=anxStat/100;
        volume.GetComponent<Image>().fillAmount=presentase;
    }

    //Naya
    async void DisplayChoice()
    {   
        time=10;
        inChoice=true;
        choicePanel.SetActive(true);
        isClick=true;
        StartCoroutine(ShuffleCard());
        await Task.Delay(100);
        Uicard.SetActive(true);
        panel.SetActive(true);
        StartCoroutine(GetIn());

    }

    async void Update()
    {
         if (isChosed)
        {
        timer.text="";
        inChoice=false;
        isChosed=false;
        await Task.Delay(2000);
        StartCoroutine(Fade(cardName));
        await Task.Delay(500);
        panel.SetActive(false);
        Uicard.SetActive(false);
        shuffleBtn.SetActive(true);
        StartDialog(nextNode.nextNode);
        }

        if (inChoice)
        {
            if (time<0)
            {
                inChoice=false;
                StartCoroutine(HideCard("", 0f, 0));//parameter terakhir digunakan untuk indext next node diam
            }
            time-=Time.deltaTime;
            if (time<5)
            {
                timer.color=Color.red;
            }
            timer.text=time.ToString("F0");
        }
    }

    // IEnumerator Countdown(){
    //     float time=10;
    //     while (time>0){
            
    //     }
    // }
}

