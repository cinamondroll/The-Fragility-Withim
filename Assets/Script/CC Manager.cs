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

    void Start()
    {
        
    }

    public void loadCH(string gameObject)
    {
        SceneManager.LoadScene(gameObject);
    }
}
