using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int ID;
    public string Name;
    public int Value;

    public InventoryItem(int ID, string Name, int Value)
    {
        this.ID = ID;
        this.Name = Name;
        this.Value = Value;
    }
}
