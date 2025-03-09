using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogNode", menuName = "Dialog/Node")]
public class DialogNode : ScriptableObject
{
    public DialogLine[] lines;
    public Choice[] choices;
    public string nextScene;

}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public DialogNode nextNode;

}
