using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class PrologScript : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject dialogPanel;
    //[SerializeField] public Button progresButton;
    [Header("Setting")]
    [SerializeField] float textSpeed = 0.05f;
    DialogNode currentNode;
    int currentLineIndex = 0;
    bool isTyping = false;
    float time=10;
    private bool inChoice=false;

    void Start()
    {
        dialogPanel.SetActive(false);
        //progresButton.onClick.AddListener(clicking);
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
        if (currentNode==null || currentNode.lines.Length == currentLineIndex && currentNode.isChoiceNull())
        { 
            if (currentNode.nextNode == null)
            {
                EndDialog();
                SceneManager.LoadScene(currentNode.nextScene);            
            } else 
            {
                currentNode = currentNode.nextNode;
                currentLineIndex = 0;
                DisplayCurrentLine();
            }
            return;
        }



        if (currentLineIndex < currentNode.lines.Length || currentNode.isChoiceNull())
        {
            Debug.Log(currentLineIndex);
            DialogLine line = currentNode.lines[currentLineIndex];

            //PLay Audio
             //Start Corroutin
            StartCoroutine(AnimateAndType(line));
        }
    }

        IEnumerator AnimateAndType (DialogLine line)
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

    public void clicking()
    {
        if(isTyping)
        {
            StopAllCoroutines();
            DialogLine currentLine = currentNode.lines[currentLineIndex];
            dialogText.text = currentLine.text;
            dialogText.maxVisibleCharacters = currentLine.text.Length;
            isTyping = false;
            // if (voiceAudioSource.isPlaying)
            // {
            //     voiceAudioSource.Stop();
            // }
        }
        else
        {
            currentLineIndex++;
            DisplayCurrentLine();
            
        }
        // if(inChoice){
        //     progresButton.interactable=false;
        // }
    }

    void EndDialog()
    {
        dialogText.text = "";

        Debug.Log("End Dialog");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            clicking();
        }
    }
}
