using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectButtonScript : MonoBehaviour
{
    [SerializeField] public Image objectIcon; 
    [SerializeField] public TMP_Text objectName;  

    public void setSprite(Sprite sprite)
    {
        if (objectIcon != null) objectIcon.sprite = sprite;
    }

    public void setText(string text)
    {
        if (objectName != null) objectName.text = text;
    }
}