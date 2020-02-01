using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject itemDestroyListener;
    [SerializeField]
    private GameObject itemRepairListener;

    [SerializeField]
    private GameEvent OnItemDestroyed;
    [SerializeField]
    private GameEvent OnItemRepaired;

    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private ScoreCounter scoreCounter;
    //[SerializeField]
    //private PlayerData destroyerData;
    //[SerializeField]
    //private PlayerData repayerData;

    private float itemHp;
    private int itemCost;
    private bool destroyed;
    private bool damaged;

    private SpriteRenderer spRenderer;

    private void Start()
    {
        itemDestroyListener.SetActive(false);
        itemRepairListener.SetActive(false);
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = itemData.Sprite;
        itemHp = itemData.Hp;
        itemCost = itemData.Cost;
        destroyed = false;
        damaged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Item in zone!");
        if (collision.CompareTag("DestroyWave"))
        itemDestroyListener.SetActive(true);
        //TODO прописать условие для тэга воздействия пушки ремонтника
        //itemRepairListener.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Item out of zone");
        if (collision.CompareTag("DestroyWave"))
            itemDestroyListener.SetActive(false);
        //TODO прописать условие для тэга воздействия пушки ремонтника
        //itemRepairListener.SetActive(true);

    }

    private void TakeDamage(float damage)
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
                scoreCounter.Score += itemCost;

                OnItemDestroyed.Raise();
            }
            else
            {
                damaged = true;
                spRenderer.sprite = itemData.DamagedSprite;
            }
        }
    }

    private void TakeRepairment(float repairPower)
    {
        if (damaged || destroyed)
        {
            itemHp += repairPower;
            destroyed = false;
            if (itemHp >= itemData.Hp)
            {
                //TODO запустить анимацию, частицы и звук исцеления
                itemHp = itemData.Hp;
                damaged = false;
                spRenderer.sprite = itemData.Sprite;
                scoreCounter.Score -= itemCost;

                OnItemRepaired.Raise();
            }
            else
            {
                spRenderer.sprite = itemData.DamagedSprite;
            }
        }
    }

    public void TakeDamage(PlayerData destroyerData)
    {
        TakeDamage(destroyerData.AttackPower);
    }

    public void TakeRepairment(PlayerData repayerData)
    {
        TakeRepairment(repayerData.AttackPower);
    }
}
