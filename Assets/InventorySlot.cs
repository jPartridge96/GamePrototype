using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject dropPrefab;
    Item item;

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        dropPrefab.GetComponent<ItemPickup>().item = item;
        dropPrefab.GetComponent<SpriteRenderer>().sprite = item.Icon;
        dropPrefab.name = item.Name;

        Instantiate(dropPrefab, GameObject.Find("Player").transform.position, Quaternion.identity);

        Debug.Log("Dropping item " + dropPrefab.name);
        Inventory.instance.Remove(item);
        
    }
}
