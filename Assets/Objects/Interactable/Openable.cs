using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite Open;
    public Sprite Closed;
    private SpriteRenderer SpriteRender;
    public Animator DoorAnim;
    bool isOpen;

    public override void Interact()
    {
        if(DoorAnim.GetBool("IsOpen"))
            SpriteRender.sprite = Closed;
        else
            SpriteRender.sprite = Open;

        DoorAnim.SetBool("IsOpen", !DoorAnim.GetBool("IsOpen"));

        // Update sprite with accurate colliders 
        PolygonCollider2D collider = SpriteRender.GetComponent<PolygonCollider2D>();
        collider.TryUpdateShapeToAttachedSprite();
    }

    private void Start()
    {
        SpriteRender = GetComponent<SpriteRenderer>();
        SpriteRender.sprite = Closed;
    }
}
