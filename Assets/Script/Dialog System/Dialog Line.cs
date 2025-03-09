using UnityEngine;



[CreateAssetMenu(fileName = "NewDialogLine", menuName = "Dialog/Line")]
public class DialogLine : ScriptableObject 
{
    public string Pembicara;
    [TextArea(3,5)] public string text;
    public Sprite charSprite;

    public DialogTarget targetImage;

    [Header("Audio")]
    public AudioClip spokenText;
    public AudioClip moorOrEffect;

    [Header("Animation")]
    public float durasi = 1;
    public Animation animationType;

}

public enum DialogTarget
{
    LeftImage,
    RightImage,
    CenterImage
}

public enum Animation
{
    None,
    EnteringScane,
    ExitingScane,
    Jumping,
    Shaking,
    Scaling,
    Rotating
}