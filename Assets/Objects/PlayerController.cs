using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public Rigidbody2D Body;
    public Animator PlayerAnimation;
    public bool Busy;
    public Animator InventoryAnimation;
    public GameObject InteractIcon;

    // Determine what input is being received
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            CheckInteraction();

        if(Input.GetKeyDown(KeyCode.E))
            ToggleInventory();

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if(!Busy)
        {
            PlayerAnimation.SetFloat("Speed", _movement.sqrMagnitude);
            PlayerAnimation.SetFloat("Horizontal", _movement.x);
            PlayerAnimation.SetFloat("Vertical", _movement.y);

            if (_movement != Vector2.zero)
            {
            PlayerAnimation.SetFloat("HorizontalIdle", _movement.x);
            PlayerAnimation.SetFloat("VerticalIdle", _movement.y);
            }
        }
        else
        {
            PlayerAnimation.SetFloat("Speed", 0.0f);
            PlayerAnimation.SetFloat("Horizontal", 0.0f);
            PlayerAnimation.SetFloat("Vertical", 0.0f);
        }      
    }

    public void OpenInteractIcon()
    {
        InteractIcon.SetActive(true);
    }
    public void CloseInteractIcon()
    {
        InteractIcon.SetActive(false);
    }

    private void ToggleInventory()
    {
        if(!InventoryAnimation.GetBool("IsOpen") && !Busy)
        {
            Busy = true;
            InventoryAnimation.SetBool("IsOpen", true);
        }
        else if (InventoryAnimation.GetBool("IsOpen") && Busy)
        {
            Busy = false;
            InventoryAnimation.SetBool("IsOpen", false);
        }
    }

    private void CheckInteraction()
    {
        if(!Busy)
        {
            // Player
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _interactRange, 0, Vector2.zero);

            // Mouse
            RaycastHit2D[] mouseHits = Physics2D.BoxCastAll(Input.mousePosition, _interactRange, 0, Vector2.zero);

            if(hits.Length > 0)
            {
                foreach(RaycastHit2D hit in hits)
                {
                    if(hit.transform.GetComponent<Interactable>() && !Busy)
                    {
                        hit.transform.GetComponent<Interactable>().Interact();
                        return;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!Busy)
            Body.MovePosition(Body.position + _movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }

    private Vector2 _movement;
    private Vector2 _interactRange = new Vector2(1f, 1f);
}
