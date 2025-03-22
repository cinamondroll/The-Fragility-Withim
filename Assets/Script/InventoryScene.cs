using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Threading.Tasks;

public class InventoryScene : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Button saveButton;
    [SerializeField] String Menu = "Start Screen";
    [SerializeField] GameObject UnvailableText;
    void Start()
    {
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(Menu);
    }
    
    public async void UnvButtonFun ()
    {
        await Unvailable();
    }
    
    async Task Unvailable(){
        UnvailableText.SetActive(true);
        await Task.Delay(2000);
        UnvailableText.SetActive(false);
    }
    
}
