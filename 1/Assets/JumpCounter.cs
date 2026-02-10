using TMPro;
using UnityEngine;

public class JumpCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI jumpCounter;
    private int jumpCount = 0;

    public void AddJump()
    {
        jumpCount++;
        jumpCounter.text = "Прыжков сделано: " + jumpCount;
    }
}