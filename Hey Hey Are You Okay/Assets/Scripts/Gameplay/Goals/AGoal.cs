using UnityEngine;

public abstract class AGoal : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D other);
    public abstract void OnTriggerExit2D(Collider2D other);
}
