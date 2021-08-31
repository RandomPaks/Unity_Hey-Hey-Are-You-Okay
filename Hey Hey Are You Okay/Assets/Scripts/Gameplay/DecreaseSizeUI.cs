using UnityEngine;

public class DecreaseSizeUI : MonoBehaviour
{
    RectTransform rect;
    CircleCollider2D collider;
    public float mult = 2;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        float newScale = ((100 * mult) - GameManager.Instance.progress) / (100 * mult);
        rect.localScale = new Vector3(newScale, newScale, 0);
        collider.radius = ((50 * mult) - GameManager.Instance.progress) /  mult;
    }
}
