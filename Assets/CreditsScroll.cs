using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsText; 
    public float scrollSpeed = 100f;

    private Vector2 startPos;
    private float endY;

    private void Start()
    {
        startPos = creditsText.anchoredPosition;
        endY = creditsText.rect.height + 1400f; 
    }

    private void Update()
    {
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (creditsText.anchoredPosition.y >= endY)
        {
            creditsText.anchoredPosition = startPos;
        }
    }
}
