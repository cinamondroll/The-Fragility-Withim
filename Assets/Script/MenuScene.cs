using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        if(startButton != null)
        {
            startButton.onClick.AddListener(startGame);
        }
        if(muatButton != null)
        {
            muatButton.onClick.AddListener(muatGame);
        }
        if(exitButton != null)
        {
            exitButton.onClick.AddListener(exitGame);
        }
        if(lemariButton != null)
        {
            lemariButton.onClick.AddListener(inventory);
        }

    }

    public void startGame()
    {
        SceneManager.LoadScene(ChapterChoice);
    }
    public void muatGame()
    {

    }
    public void inventory()
    {
        SceneManager.LoadScene(Inventory);
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

}
