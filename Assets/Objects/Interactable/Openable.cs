using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite Open;
    public Sprite Closed;
    private SpriteRenderer SpriteRender;
    bool isOpen;

    public override void Interact()
    {
        if(isOpen)
            SpriteRender.sprite = Closed;
        else
            SpriteRender.sprite = Open;

        isOpen = !isOpen;

        PolygonCollider2D collider = SpriteRender.GetComponent<PolygonCollider2D>();
        collider.TryUpdateShapeToAttachedSprite();
    }

    private void Start()
    {
        SpriteRender = GetComponent<SpriteRenderer>();
        SpriteRender.sprite = Closed;
    }
}
