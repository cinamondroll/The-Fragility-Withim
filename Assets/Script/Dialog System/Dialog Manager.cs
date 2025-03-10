using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text speakerName;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] Button choiceButtonPrefab;
    [SerializeField] Button progresButton;

    [Header("Protagonist")]
    [SerializeField] Image leftImage;
    [SerializeField] Image rightImage;
    [SerializeField] Image centerImage;
    [SerializeField] bool deActiveLeftImage;
    [SerializeField] bool deActiveRightImage;
    [SerializeField] bool deActiveCenterImage;

    [Header("Audio")]
    public AudioSource voiceAudioSource;
    public AudioSource effectAudioSource;

    [Header("Setting")]
    [SerializeField] float textSpeed = 0.05f;
    DialogNode currentNode;
    int currentLineIndex = 0;
    bool isTyping = false;
    
    void Start()
    {
        
    }

    
}
