using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUpdate : MonoBehaviour
{
    private BoxCollider2D itemCollider;

    [SerializeField] private ItemData itemData;
    void Start()
    {
        itemCollider = GetComponent<BoxCollider2D>();
        itemCollider.size = new Vector2(itemData.ColliderX, itemData.ColliderY);
    }
}
