using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite Icon = null;
    
    [TextArea(3,10)]
    public string Description;

    public int BuyPrice;
    public int SellPrice;
    public int ItemAmount = 1;
}