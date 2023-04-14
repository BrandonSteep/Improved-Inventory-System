using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start(){
        ChangeSelectedSlot(0);
    }

    private void Update(){
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 8){
                ChangeSelectedSlot(number -1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue){
        if(selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item){
        if(item.stackable){
            for (int i = 0; i < inventorySlots.Length; i++){
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.maxStackedItems && itemInSlot.item.stackable == true){
                    
                    itemInSlot.count ++;
                    itemInSlot.RefreshCount();
                    return true;
                }
                else if (itemInSlot == null){
                    SpawnNewItem(item, slot);
                    return true;
                }
            }
        }
        else{
            for (int i = 0; i < inventorySlots.Length; i++){
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null){
                    SpawnNewItem(item, slot);
                    return true;
                }
            }
        }
        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void SpawnNewItem(Item item, InventorySlot slot, int count){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.count = count;
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null){
            Item item = itemInSlot.item;
            if(use == true){
                itemInSlot.count --;
                if(itemInSlot.count <= 0){
                    Destroy(itemInSlot.gameObject);
                }
                else{
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }
        
        return null;
    }
}
