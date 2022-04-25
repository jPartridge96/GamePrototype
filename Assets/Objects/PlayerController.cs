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

    void Start()
    {
        InteractIcon.SetActive(false);
    }

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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] playerHits = Physics2D.BoxCastAll(transform.position, _playerInteractRange, 0, Vector2.zero);    // Player in range
            RaycastHit2D[] mouseHits = Physics2D.BoxCastAll(mousePosition, _mouseInteractRange, 0, Vector2.zero);     // Mouse in range

            if(playerHits.Length > 0 && mouseHits.Length > 0)
            {
                // Checks each entity player is near whether if mouse is in range
                foreach(RaycastHit2D pHit in playerHits)
                {
                    foreach(RaycastHit2D mHit in mouseHits)
                    {
                        if(pHit.transform.GetComponent<Interactable>() && mHit.transform.GetComponent<Interactable>() && !Busy)
                        {
                            // Will only interact if mouse is hovering over Interactable
                            mHit.transform.GetComponent<Interactable>().Interact();
                            return;
                        }
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
    private Vector2 _playerInteractRange = new Vector2(1f, 1f);
    private Vector2 _mouseInteractRange = new Vector2(0.1f, 0.1f);
}
