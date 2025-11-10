using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equip,
    Consume,
    ETC
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/newItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;         // 아이템 이름
    public string description;      // 아이템 설명
    public int stuck;               // 아이템 재고
    public ItemType type;           // 아이템 타입
}
