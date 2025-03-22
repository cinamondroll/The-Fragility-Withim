using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MenuScene : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button muatButton;
    [SerializeField] Button lemariButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button yaButton;
    [SerializeField] Button tidakButton;
    [SerializeField] String ChapterChoice = "Chapter Choice";
    [SerializeField] String Inventory = "Inventory";
    public GameObject PopUpExit;
    public GameObject PopUpTentang;
    private AudioSource clickaudio;
    public Button mute;
    public Sprite volume;
    public Sprite volume2;
    public AudioSource song;
    bool isMute=false;

    void Awake()
    {
        clickaudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(startGame);
        }
        if (muatButton != null)
        {
            muatButton.onClick.AddListener(muatGame);
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(exitGame);
        }
        if (lemariButton != null)
        {
            lemariButton.onClick.AddListener(inventory);
        }

    }

    public async void startGame()
    {
        clickaudio.Play();
        await Task.Delay(200);
        SceneTransitionManager.instance.LoadSceneWithFade("Chapter Choice");
        //SceneManager.LoadScene(ChapterChoice);
    }
    public void muatGame()
    {

    }
    public void inventory()
    {
        SceneTransitionManager.instance.LoadSceneWithFade(Inventory);
        //SceneManager.LoadScene(Inventory);
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void Open_Popup(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void Close_Popup(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // void Mute(){
    //     if(isMute)

    // }
}
