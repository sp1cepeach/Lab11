using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    List<InventoryItem> inventoryItems;

    void Start()
    {
        inventoryItems = new List<InventoryItem>();

        PopulateInventory(6);
        LinearSearchByName("ItemName3");
        LinearSearchByName("ItemName7");
        inventoryItems.Sort((item1, item2) => item1.ID.CompareTo(item2.ID));
        BinarySearchByID(4);
        BinarySearchByID(8);
        QuickSortByValue();
    }

    private void PopulateInventory(int n)
    {
        inventoryItems.Clear();

        for (int i = 0; i < n; i++)
        {
            int ID = RandomUniqueId(n);
            string Name = "ItemName" + ID;
            int Value = Random.Range(1, 101);

            inventoryItems.Add(new InventoryItem(ID, Name, Value));
        }

        PrintInventory();
    }

    InventoryItem LinearSearchByName(string name)
    {
        InventoryItem foundItemOrNull = null;
        
        foreach (InventoryItem item in inventoryItems)
        {
            if (item.Name == name)
            {
                foundItemOrNull = item;
                break;
            }
        }

        if (foundItemOrNull == null)
            Debug.Log(name + " was NOT found by name in the inventory");
        else
            Debug.Log(name + " was found by name in the inventory");

        return foundItemOrNull;
    }

    InventoryItem BinarySearchByID(int id)
    {
        InventoryItem foundItemOrNull = null;

        int left = 0;
        int right = inventoryItems.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (inventoryItems[mid].ID == id)
            {
                foundItemOrNull = inventoryItems[mid]; // Return the index if the target is found
                break;
            }
            else if (inventoryItems[mid].ID < id)
            {
                left = mid + 1; // Narrow the search to the upper half
            }
            else
            {
                right = mid - 1; // Narrow the search to the lower half
            }
        }

        if (foundItemOrNull == null)
            Debug.Log(id + " was NOT found by id in the inventory");
        else
            Debug.Log(id + " was found by id in the inventory");

        return foundItemOrNull;
    }

    void QuickSortByValue()
    {
        QuickSortByValue(0, inventoryItems.Count - 1);
        PrintInventory();
    }

    private void QuickSortByValue(int first, int last)
    {
        if (first < last)
        {
            int pivot = PartitionByValue(first, last);

            QuickSortByValue(first, pivot - 1);
            QuickSortByValue(pivot + 1, last);
        }
    }

    private int PartitionByValue(int first, int last)
    {
        int pivot = last;
        int smaller = first;
        int larger = last - 1;

        while (smaller <= larger) {
            if (inventoryItems[smaller].Value < inventoryItems[pivot].Value)
            {
                smaller++;
            } else
            {
                InventoryItem temporary = inventoryItems[smaller];
                inventoryItems[smaller] = inventoryItems[larger];
                inventoryItems[larger] = temporary;
                larger--;
            }
        }

        InventoryItem temporaryNext = inventoryItems[smaller];
        inventoryItems[smaller] = inventoryItems[pivot];
        inventoryItems[pivot] = temporaryNext;
        pivot = smaller;

        return pivot;
    }

    private int RandomUniqueId(int maxId)
    {
        int ID = 0;
        bool isIdUnique = false;

        while (!isIdUnique)
        {
            ID = Random.Range(1, maxId + 1);
            isIdUnique = true;

            foreach (InventoryItem item in inventoryItems)
            {
                if (ID == item.ID)
                {
                    isIdUnique = false;
                    break;
                }
            }
        }

        return ID;
    }

    private void PrintInventory()
    {
        string output = "Inventory:\n";

        foreach (InventoryItem item in inventoryItems)
        {
            output += 
                "\tID: " + item.ID +
                "\tName: " + item.Name +
                "\tValue: " + item.Value + "\n";
        }

        Debug.Log(output);
    }
}
