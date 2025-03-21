using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPopUp;
    [SerializeField] GameObject GoodEndingPopUp;
    [SerializeField] GameObject BadEndingPopUp;

    void Start()
    {
        FinalStatsAnx();
    }

    void FinalStatsAnx()
    {
        if (PlayerPrefs.GetFloat("anxStat") >= 100)
        {
            GameOverPopUp.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("anxStat") < 100 && PlayerPrefs.GetFloat("anxStat") > 50)
        {
            BadEndingPopUp.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("anxStat") <= 50)
        {
            GoodEndingPopUp.SetActive(true);
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneTransitionManager.instance.LoadSceneWithFade(sceneToLoad);
    }
}
