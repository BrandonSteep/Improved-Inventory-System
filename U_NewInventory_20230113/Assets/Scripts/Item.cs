using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{

    [Header("Only Gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = false;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType {
    Weapon,
    Key
}

public enum ActionType {
    Attack,
    Use
}
