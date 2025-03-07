using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject popUpExit;
    //public Button closePopUpExit;
    //public Button openPopUpExit;

    public static GameManager Instance;

    void Start()
    {
        
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

    // void Awake()
    // {
    //     if (Instance==null)
    //     {
    //         Instance=this;
    //         DontDestroyOnLoad(gameObject);
    //     }else{
    //         Destroy(gameObject);
    //     }
    // }

    public void LoadScene(string scene){
        //Debug.Log("Tes");
        SceneManager.LoadScene(scene);
    }
}
