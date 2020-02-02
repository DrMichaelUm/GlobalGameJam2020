using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject itemDestroyListener;
    //[SerializeField]
    public GameObject itemRepairListener;

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
    private BoxCollider2D collider;

    private void Start()
    {
        itemDestroyListener.SetActive(false);
        itemRepairListener.SetActive(false);
        spRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        spRenderer.sprite = itemData.Sprite;
        collider.size = new Vector2(itemData.ColliderX, itemData.ColliderY);
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
                scoreCounter.Score += itemCost;
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
                scoreCounter.Score -= itemCost;
                spRenderer.sprite = itemData.DamagedSprite;
                OnItemRepaired.Raise();
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
