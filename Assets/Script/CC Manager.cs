using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CCManager : MonoBehaviour
{
    [SerializeField] Button CH1;
    [SerializeField] Button CH2;
    [SerializeField] Button CH3;
    [SerializeField] Button Kembali;
    [SerializeField]private int[] Unlocked;

    void Start()
    {
        CH1.interactable=false;
        CH2.interactable=false;
        CH3.interactable=false;
    }
    void Update(){
        if(Unlocked[0]==1){
            CH1.interactable=true;
        }
        if(Unlocked[1]==1){
            CH2.interactable=true;
        }
        if(Unlocked[2]==1){
            CH3.interactable=true;
        }
    }

    public void loadCH(string gameObject)
    {
        SceneManager.LoadScene(gameObject);
        PlayerPrefs.SetFloat("x", 0f);
        PlayerPrefs.SetFloat("y", -1.3f);
        PlayerPrefs.SetFloat("anxStat", 80);

    }

}
