using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAffect : MonoBehaviour
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
    private bool isScoreChanged = false;

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
        if(collision.CompareTag("RepairWave"))
        itemRepairListener.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Item out of zone");
        if (collision.CompareTag("DestroyWave"))
            itemDestroyListener.SetActive(false);
        if (collision.CompareTag("RepairWave"))
            itemRepairListener.SetActive(false);

    }

    private void TakeDamage(float damage)
    {
        if (!destroyed)
        {
            itemHp -= damage;
            if (itemHp <= 0)
            {
                itemHp = 0;
                damaged = true;
                destroyed = true;

                //TODO запустить анимацию, частицы и звук уничтожения
                ChangeScoreByItemCost(true);
                spRenderer.sprite = itemData.DestroyedSprite;
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
        if (destroyed || damaged)
        {
            itemHp += repairPower;

            if (itemHp >= itemData.Hp)
            {
                //TODO запустить анимацию, частицы и звук исцеления
                itemHp = itemData.Hp;
                damaged = false;
                spRenderer.sprite = itemData.Sprite;
            }
            else if (itemHp >= itemData.Hp / 2)
            {
                destroyed = false;
                ChangeScoreByItemCost(false);
                spRenderer.sprite = itemData.DamagedSprite;
                OnItemRepaired.Raise();
            }
        }
    }

    private void ChangeScoreByItemCost(bool mode)
    {
        if (mode)
        {
                scoreCounter.Score += itemCost;
                isScoreChanged = false;
        }
        else
        {
            if (!isScoreChanged)
            {
                scoreCounter.Score -= itemCost;
                isScoreChanged = true;
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
