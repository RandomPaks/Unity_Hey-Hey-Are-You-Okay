using UnityEngine;

public class DecreaseSizeUI : MonoBehaviour
{
    RectTransform rect;
    CircleCollider2D collider;
    public float mult = 1;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        float newScale = ((200 * mult) - GameManager.Instance.progress) / (200 * mult);
        rect.localScale = new Vector3(newScale, newScale, 0);
        collider.radius = ((100 * mult) - GameManager.Instance.progress) / (2 * mult);
    }
}
