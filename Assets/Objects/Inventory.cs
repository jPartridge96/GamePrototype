using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int Capacity = 12;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(items.Count >= Capacity)
        {
            Debug.Log("Not enough space.");
            return false;
        }

        Item copyItem = Instantiate(item);
        foreach(Item i in items)
        {
            if(copyItem.Name.Equals(i.Name))
            {
                i.ItemAmount++;
                onItemChangedCallback.Invoke();
                break;
            }
        }
        items.Add(item);

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        if(onItemChangedCallback != null)
            items.Remove(item);
        onItemChangedCallback.Invoke();
    }
}
