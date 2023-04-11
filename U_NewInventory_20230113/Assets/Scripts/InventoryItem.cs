using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;
    public Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Item item;
    
    public void InitializeItem(Item newItem){
        item = newItem;
        image.sprite =newItem.image;
        // count = Random.Range(1, 4);
        RefreshCount();
    }

    public void RefreshCount(){
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    // Drag and drop
    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;

        parentAfterDrag = transform.parent;
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
}
