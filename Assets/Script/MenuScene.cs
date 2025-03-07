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
    [SerializeField] Button exitButton;
    [SerializeField] Button yaButton;
    [SerializeField] Button tidakButton;
    [SerializeField] String Chapter1 = "SC Chapter 1";
    public GameObject PopUpExit;

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

    }

    public void startGame()
    {
        SceneManager.LoadScene(Chapter1);
    }
    public void muatGame()
    {

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
