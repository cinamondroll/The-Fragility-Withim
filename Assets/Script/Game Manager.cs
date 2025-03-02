using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [Header("Pengaturan Halaman Menu")]
    public string Halaman_Menu;
    public string[] Halaman_Permainan;
    public string Halaman_Hasil;

    public void PindahHalaman(string halamanTujuan)
    {
        SceneManager.LoadScene(halamanTujuan);
    }

    public void Open_Popup(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void Close_Popup(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void Keluar_Aplikasi()
    {
        Application.Quit();
    }
}

