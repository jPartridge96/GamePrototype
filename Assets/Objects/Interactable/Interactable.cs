using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().OpenInteractIcon();
    }

    private void OnCollisionExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().CloseInteractIcon();
    }
}
