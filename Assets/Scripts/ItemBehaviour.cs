using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private GameObject itemListener;

    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private DestroyedItemCounter destroyedItemCounter;

    private float itemHp;
    private int itemCost;
    private bool destroyed;
    private bool damaged;

    private SpriteRenderer spRenderer;

    private void Start()
    {
        itemListener = GetComponentInChildren<GameEventListener>().gameObject;
        itemListener.SetActive(false);
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = itemData.Sprite;
        itemHp = itemData.Hp;
        itemCost = itemData.Cost;
        destroyed = false;
        damaged = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO прописать условие для тэга волны
        itemListener.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //TODO прописать условие для тэга волны
        itemListener.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (!destroyed)
        {
            itemHp -= damage;
            if (itemHp <= 0)
            {
                itemHp = 0;
                destroyed = true;
                damaged = false;
                //TODO запустить анимацию, частицы и звук уничтожения

                spRenderer.sprite = itemData.DestroyedSprite;
                destroyedItemCounter.DeItemNum++;
            }
            else
            {
                damaged = true;
                spRenderer.sprite = itemData.DamagedSprite;
            }
        }
    }

    public void TakeRepairment(float repairPower)
    {
        if (damaged || destroyed)
        {
            itemHp += repairPower;
            destroyed = false;
            if (itemHp >= itemData.Hp)
            {
                itemHp = itemData.Hp;
                damaged = false;
                spRenderer.sprite = itemData.Sprite;
            }
            else
            {
                spRenderer.sprite = itemData.DamagedSprite;
            }
        }
    }
}
