using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using System.Runtime.InteropServices;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text speakerNametext;
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
        dialogPanel.SetActive(false);
        choicePanel.SetActive(false);
        //Logic progress button

        //hide image
        if(leftImage != null && deActiveLeftImage) leftImage.color = new Color32(255, 255, 255, 0);
        if(rightImage != null && deActiveRightImage) rightImage.color = new Color32(255, 255, 255, 0);
        if(centerImage != null && deActiveCenterImage) centerImage.color = new Color32(255, 255, 255, 0);
    }

    public void StartDialog(DialogNode startNode) 
    {
        dialogPanel.SetActive(true);
        currentNode = startNode;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        if (currentNode == null || currentNode.lines.Length == 0)
        { 
            //END DIALOG

            return;
        }
        if (currentLineIndex < currentNode.lines.Length)
        {
            DialogLine line = currentNode.lines[currentLineIndex];
            speakerNametext.text = line.speakerName;
            Image targetImage = GetTargetImage(line.targetImage);
            if(targetImage != null && line.charSprite != null)
            {
                targetImage.sprite = line.charSprite;
                targetImage.color = Color.white;
            }
            //PLay Audio

            //Start Corroutin
            StartCoroutine(AnimateAndType(line, targetImage));

        }
        else
        {

        }
    }

    private Image GetTargetImage(DialogTarget targetImage)
    {
        switch(targetImage)
        {
            case DialogTarget.RightImage: return rightImage;
            case DialogTarget.LeftImage: return leftImage;
            case DialogTarget.CenterImage: return centerImage;
            default: return null;
        }
    }

    IEnumerator AnimateAndType (DialogLine line, Image targetImage)
    {
        if(line.animationType != Animation.None)
        {
            yield return new WaitForSeconds(line.durasi);
        }
        yield return StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";
        int visibleCharCount = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if(text[i] == '<')
            {
                int closingTagIndex = text.IndexOf('>', i);
                if (closingTagIndex != -1)
                {
                    dialogText.text += text.Substring(i, closingTagIndex - i + 1);
                    i = closingTagIndex;
                }
            }
            dialogText.text += text[i];
            visibleCharCount++;

            dialogText.maxVisibleCharacters = visibleCharCount;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void PlayAudio(DialogLine line)
    {
        if(voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }

        if(line.spokenText != null)
        {
            voiceAudioSource.clip = line.spokenText;
            voiceAudioSource.Play();
        }

        if (line.moorOrEffect != null)
        {
            effectAudioSource.clip = line.moorOrEffect;
            voiceAudioSource.Play();
        }
    }
}
