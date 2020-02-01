using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 53)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private float itemHP;
    [SerializeField]
    private int itemCost;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private Sprite damagedItemSprite;
    [SerializeField]
    private Sprite destroyedItemSprite;

    public float Hp
    {
        get
        {
            return itemHP;
        }
    }

    public int Cost
    {
        get
        {
            return itemCost;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return itemSprite;
        }
    }

    public Sprite DamagedSprite
    {
        get
        {
            return damagedItemSprite;
        }
    }

    public Sprite DestroyedSprite
    {
        get
        {
            return destroyedItemSprite;
        }
    }

}
