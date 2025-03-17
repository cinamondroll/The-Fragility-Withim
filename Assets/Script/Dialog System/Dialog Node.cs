using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogNode", menuName = "Dialog/Node")]
public class DialogNode : ScriptableObject
{
    public DialogLine[] lines;
    public Choice[] choices;
    public string nextScene;

    [SerializeField]public float[] condition;
    [SerializeField]public float[] stat;
    [SerializeField]public Sprite[] AssetCard;


    public Choice nextNodeIndex(int i){
        if (i>=choices.Length)
        {
            return choices[0];
        }
        return choices[i];
    }
    
    public bool isChoiceNull(){
        return choices.Length==0;
    }

}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public DialogNode nextNode;

}
