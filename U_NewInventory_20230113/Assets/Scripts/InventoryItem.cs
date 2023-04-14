using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private InventoryManager inventory;

    [Header("UI")]
    public Image image;
    public Text countText;

    [HideInInspector] public Transform parentBeforeDrag;
    [HideInInspector] public Transform parentAfterDrag;
    
    public int count = 1;
    public Item item;

    void Start(){
        inventory = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
    }
    
    public void InitializeItem(Item newItem){
        item = newItem;
        image.sprite =newItem.image;
        
        RefreshCount();
    }

    public void RefreshCount(){
        countText.text = count.ToString();
        bool textActive = item.stackable;
        countText.gameObject.SetActive(textActive);
    }

    // Drag & Drop
    #region Drag & Drop
    public void OnBeginDrag(PointerEventData eventData){
        parentAfterDrag = transform.parent;

        if(eventData.button == PointerEventData.InputButton.Right){
            // Debug.Log("Right Mouse Drag");
            SplitStack();
        }
        
        image.raycastTarget = false;
        transform.SetParent(transform.root);
        countText.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;

        transform.SetParent(parentAfterDrag);

        countText.raycastTarget = true;
    }
    #endregion

    // Split Stack Behaviour
    private void SplitStack(){
        if(item.stackable && count > 1){
            Debug.Log("Split Stack");

            int halfValue = count/2;
            int remainder = count%2;

            int firstHalf = halfValue;
            int secondHalf = halfValue;

            if(remainder == 1){
                firstHalf += 1;
            }

            inventory.SpawnNewItem(item, parentAfterDrag.GetComponent<InventorySlot>(), secondHalf);
            count = firstHalf;
            RefreshCount();
        }
    }
}
