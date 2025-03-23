using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DialogManager : MonoBehaviour
{
    public GameObject Uicard;
    public GameObject[] Deck;
    public GameObject panel;
    private bool inChoice = false;
    HashSet<int> setUnik = new HashSet<int>();
    bool isChosed = false;
    private string cardName;
    [SerializeField] private float anxStat;
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
    public AudioClip voiceAudioSource;
    public AudioClip effectAudioSource;
    [SerializeField] float textSpeed = 0.05f;
    [SerializeField] AudioClip audioClick;
    [SerializeField] AudioClip audioTimer;
    [SerializeField] AudioClip shuffleAudio;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioType;

    [Header("Setting")]

    //[SerializeField] string NextSceneDialog;
    DialogNode currentNode;
    int currentLineIndex = 0;
    bool isTyping = false;
    public GameObject shuffleBtn;
    float time = 10;
    bool isTimeOver = false;
    [SerializeField] private String NextScene;
    [SerializeField] private String chatWith;
    //[SerializeField] GameObject PopUpGameOver;



    String imageBefore = "";
    bool isOver = false;

    void Awake()
    {
        anxStat = PlayerPrefs.GetFloat("anxStat");

        shapeVolume();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer.SetText("");
        dialogPanel.SetActive(false);
        //choicePanel.SetActive(false);

        //Logic progress button
        progresButton.onClick.AddListener(clicking);

        //hide image
        if (leftImage != null && deActiveLeftImage) leftImage.color = new Color32(255, 255, 255, 0);
        if (rightImage != null && deActiveRightImage) rightImage.color = new Color32(255, 255, 255, 0);
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
        if (currentNode.lines.Length == currentLineIndex && currentNode.isChoiceNull())
        {
            if (currentNode.nextNode == null)
            {
                PlayerPrefs.SetFloat("anxStat", anxStat);
                EndDialog();
                PlayerPrefs.SetString("sceneBefore", "chat");
                if (SceneTransitionManager.instance != null) SceneTransitionManager.instance.LoadSceneWithFade(currentNode.nextScene);

                //SceneManager.LoadScene(currentNode.nextScene);
            }
            else
            {
                currentNode = currentNode.nextNode;
                currentLineIndex = 0;
                DisplayCurrentLine();
            }
            return;
        }
        else if (currentLineIndex < currentNode.lines.Length || currentNode.isChoiceNull())
        {
            DialogLine line = currentNode.lines[currentLineIndex];
            speakerNametext.text = line.speakerName;
            if (imageBefore == "right")
            {
                rightImage.color = Color.gray;
            }
            else if (imageBefore == "left")
            {
                leftImage.color = Color.gray;
            }
            Image targetImage = GetTargetImage(line.targetImage);
            if (targetImage != null && line.charSprite != null)
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
        switch (targetImage)
        {
            case DialogTarget.RightImage:
                imageBefore = "right";
                return rightImage;

            case DialogTarget.LeftImage:
                imageBefore = "left";
                return leftImage;
            //case DialogTarget.CenterImage: return centerImage;
            default: return null;
        }
    }

    IEnumerator AnimateAndType(DialogLine line, Image targetImage)
    {
        if (line.animationType != Animation.None)
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
            if (i % 3 == 0) PlayAudio();

            if (text[i] == '<')
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

    void PlayAudio()
    {
        if (audioType.isPlaying)
        {
            audioType.Stop();
        }

        audioType.clip = effectAudioSource;
        audioType.Play();

    }

    public void clicking()
    {
        audioSource.clip = audioClick;
        audioSource.Play();
        if (isTyping)
        {
            StopAllCoroutines();
            DialogLine currentLine = currentNode.lines[currentLineIndex];
            dialogText.text = currentLine.text;
            dialogText.maxVisibleCharacters = currentLine.text.Length;
            isTyping = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            currentLineIndex++;
            DisplayCurrentLine();

        }
        if (inChoice)
        {
            progresButton.interactable = false;
        }
    }
    public async void HideCard(String name, float effect, int nextNodeIndex)
    {
        shapeVolume();
        isChosed = true;
        timer.SetText("");
        int chosedCArdIndex = 0;
        cardName = name;
        shuffleBtn.SetActive(false);
        for (int i = 0; i < Deck.Length; i++)
        {
            if (Deck[i].name == name)
            {
                chosedCArdIndex = i;
                continue;
            }
            Fade(Deck[i].name);
            await Task.Yield();
        }
        nextNode = currentNode.nextNodeIndex(nextNodeIndex);

        if (name != "")
        {
            Deck[chosedCArdIndex].GetComponent<CardScript>().rePosition();
            shapeVolume();
        }
        else
        {
            anxStat += 5;
            shapeVolume();
        }
    }


    async void Fade(String name)
    {
        if (name == "")
        {

        }
        else if (name != "")
        {
            float t = 0;
            while (t < 0.2f)
            {

                t += Time.deltaTime;
                SpriteRenderer spriteRenderer = GameObject.Find(name).GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
                Color fadeColor = spriteRenderer.color;
                fadeColor.a = Mathf.Lerp(1f, 0, t / 0.2f);
                spriteRenderer.color = fadeColor;
                await Task.Yield();
            }
            GameObject.Find(name).SetActive(false);
        }

    }

    public async Task ShuffleCard()
    {
        audioSource.clip = shuffleAudio;
        audioSource.Play();
        setUnik.Clear();
        int counter = 0;
        int a = 0;
        while (setUnik.Count < Deck.Length)
        {
            a = UnityEngine.Random.Range(0, 12);
            if (anxStat > currentNode.condition[a]) continue;
            setUnik.Add(a);
        }

        foreach (int i in setUnik)
        {
            Deck[counter].GetComponent<SpriteRenderer>().sprite = currentNode.AssetCard[i];
            Deck[counter].GetComponent<CardScript>().setCond(currentNode.condition[i]);
            Deck[counter].GetComponent<CardScript>().setStat(currentNode.stat[i]);
            Deck[counter].GetComponent<CardScript>().setNextNode(i);
            counter++;
        }
        await Task.Yield();
    }

    async Task GetIn()
    {
        foreach (var card in Deck)
        {
            card.GetComponent<CardScript>().setAnx(anxStat);
            card.SetActive(true);
            SpriteRenderer spriteRenderer = GameObject.Find(card.name).GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 0);
            FadeIn(card.name);
            await Task.Yield();
        }
    }

    async void FadeIn(String name)
    {
        if (name != "")
        {
            float t = 0;
            while (t < 0.5f)
            {
                t += Time.deltaTime;
                SpriteRenderer spriteRenderer = GameObject.Find(name).GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
                Color fadeColor = spriteRenderer.color;
                fadeColor.a = Mathf.Lerp(0f, 1, t / 0.5f);
                spriteRenderer.color = fadeColor;
                await Task.Yield();
            }
        }
    }

    public async Task Reshuffle()
    {
        time -= 2;
        anxStat += 3;
        await ShuffleCard();
        await GetIn();
        shapeVolume();
    }

    public async void shapeVolume()
    {
        float time = 0;
        while (time < 1f)
        {
            GameObject volume = GameObject.Find("Volume");
            float presentase = volume.GetComponent<Image>().fillAmount;
            float target = anxStat / 100;
            volume.GetComponent<Image>().fillAmount = presentase;
            float temp = Mathf.Lerp(presentase, target, time / 1f);
            volume.GetComponent<Image>().fillAmount = temp;
            time += Time.deltaTime;
            await Task.Yield();
            if (anxStat < 60)
            {
                volume.GetComponent<Image>().color = Color.green;
            }
            else if (anxStat < 80)
            {
                volume.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                volume.GetComponent<Image>().color = Color.red;
            }
        }

    }

    //Naya
    async void DisplayChoice()
    {
        shuffleBtn.GetComponent<Shuffle>().isAvail = true;
        timer.color = Color.black;
        time = 10;
        inChoice = true;
        choicePanel.SetActive(true);
        await ShuffleCard();
        Uicard.SetActive(true);
        panel.SetActive(true);
        await GetIn();

    }

    async void Update()
    {
        if (isChosed)
        {
            timer.text = "";
            inChoice = false;
            isChosed = false;
            int a = 2000;
            if (isTimeOver) a = 0;
            await Task.Delay(a);
            Fade(cardName);
            await Task.Delay(500);
            panel.SetActive(false);
            Uicard.SetActive(false);
            shuffleBtn.SetActive(true);
            StartDialog(nextNode.nextNode);
            progresButton.interactable = true;
            isTimeOver = false;
        }

        if (inChoice)
        {

            if (time < 0)
            {
                inChoice = false;
                HideCard("", 0f, 12);
                isTimeOver = true;//parameter terakhir digunakan untuk indext next node diam
            }
            time -= Time.deltaTime;
            if (time < 5)
            {
                timer.color = Color.red;
            }
            if (time <= 2)
            {
                shuffleBtn.GetComponent<Shuffle>().isAvail = false;
            }
            timer.text = time.ToString("F0");
            if (Mathf.Abs(time - Mathf.Round(time)) < 0.01f)
            {
                audioSource.clip = audioTimer;
                audioSource.Play();
            }
        }
        if (anxStat >= 100 && !isOver)
        {
            timer.SetText("");
            isOver = true;
            GameOver();
        }
    }
    //END DIALOG
    void EndDialog()
    {
        dialogText.text = "";
        speakerNametext.text = "";
        choicePanel.SetActive(false);
        panel.SetActive(false);

    }

    async void GameOver()
    {
        inChoice = false;
        PlayerPrefs.SetFloat("anxStat", anxStat);
        GameObject.Find("chatCore").SetActive(false);
        dialogText.text = "";
        speakerNametext.text = "";
        await Task.Delay(1000);
        choicePanel.SetActive(false);
        panel.SetActive(false);
        //PopUpGameOver.SetActive(true);
        PlayerPrefs.SetInt(chatWith, 0);
        SceneTransitionManager.instance.LoadSceneWithFade("Ending");
    }
    public void Restart()
    {
        PlayerPrefs.SetFloat("anxStat", 80);
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.SetInt(chatWith, 0);

        SceneTransitionManager.instance.LoadSceneWithFade(NextScene);
        //SceneManager.LoadScene("SC Chapter 1");
    }
    public void Menu()
    {
        SceneTransitionManager.instance.LoadSceneWithFade("Start Screen");
        //SceneManager.LoadScene("Start Screen");
    }

    public void setAnxStat(float stat)
    {
        anxStat -= stat;
    }
}

