using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InventoryScene : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Button saveButton;
     [SerializeField] String Menu = "Start Screen";
    void Start()
    {
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(Menu);
    }
    public void SaveInven()
    {

    }
}
