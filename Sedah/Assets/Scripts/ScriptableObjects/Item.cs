using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    general,
    special
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public ItemType itemType = ItemType.general;

    [Header("Buff")]
    public int lifeCount = 0;
    public int health = 0;
    public int attack = 0;
    public float attackRange = 0;
    public float movementSpeed = 0; 
}
