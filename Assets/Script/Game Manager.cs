using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popUpPanel;
    public Button closeButton;

    void Start()
    {
        popUpPanel.SetActive(false);

        closeButton.onClick.AddListener(ClosePopUp);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Mengecek apakah scene berikutnya ada
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Tidak ada scene berikutnya, kembali ke menu utama.");
            LoadMainMenu();
        }
    }

    // Fungsi untuk pindah ke MainMenu (misalnya setelah game selesai)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowPopUp()
    {
        popUpPanel.SetActive(true);
    }

    public void ClosePopUp()
    {
        popUpPanel.SetActive(false);
    }
}
