using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] DialogManager manager;
    [SerializeField] DialogNode startNode;
    void Start()
    {
        Invoke("StartThis", 1f);
    }
    void StartThis()
    {
        manager.StartDialog(startNode);
    }

}
