using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popUpExit;
    public Button closePopUpExit;
    public Button openPopUpExit;

    public static GameManager Instance;

    // void Start()
    // {
    //     popUpExit.SetActive(false);
    //     openPopUpExit.onClick.AddListener(ShowPopUp);
    //     closePopUpExit.onClick.AddListener(ClosePopUp);
    // }

    // public void LoadNextScene()
    // {
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     int nextSceneIndex = currentSceneIndex + 1;

    //     // Mengecek apakah scene berikutnya ada
    //     if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    //     {
    //         SceneManager.LoadScene(nextSceneIndex);
    //     }
    //     else
    //     {
    //         Debug.Log("Tidak ada scene berikutnya, kembali ke menu utama.");
    //         LoadMainMenu();
    //     }
    // }

    // // Fungsi untuk pindah ke MainMenu (misalnya setelah game selesai)
    // public void LoadMainMenu()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }

    // public void ShowPopUp()
    // {
    //     popUpExit.SetActive(true);
    // }

    // public void ClosePopUp()
    // {
    //     Debug.Log("Pop-up sedang ditutup");
    //     popUpExit.SetActive(false);
    // }
    
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

    void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    public void LoadScene(string scene){
        Debug.Log("Tes");
        SceneManager.LoadScene(scene);
    }
}
