using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    
    private void Awake(){
        Deselect();
    }

    public void Select(){
        image.color = selectedColor;
    }

    public void Deselect(){
        image.color = notSelectedColor;
    }

    // Drag and Drop
    public void OnDrop(PointerEventData eventData){
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if(transform.childCount != 0){
            Debug.Log($"Swapping Items");
            transform.GetChild(0).SetParent(inventoryItem.parentAfterDrag);
        }
        inventoryItem.parentAfterDrag = transform;
    }
}
