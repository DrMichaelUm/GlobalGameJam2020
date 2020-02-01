using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour, IPlayable
{
    [SerializeField]
    private GameObject repairCastTriggerEventListener;
    [SerializeField]
    private GameObject itemDestroyedEventListener;

    [SerializeField]
    private PlayerData destroyerData;

    [SerializeField]
    private GameEvent OnDestroySkillCasted;

    [SerializeField]
    private float damageBonusCofficient;
    private float startDamage;


    private void Start()
    {
        repairCastTriggerEventListener.SetActive(false);
        itemDestroyedEventListener.SetActive(false);
        startDamage = destroyerData.AttackPower;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO прописать условие для тэга коллайдера области урона ремонтника
        repairCastTriggerEventListener.SetActive(true);
        if (collision.CompareTag("Item"))
            itemDestroyedEventListener.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO прописать условие для тэга коллайдера области урона ремонтника
        repairCastTriggerEventListener.SetActive(false);
        if (collision.CompareTag("Item"))
            itemDestroyedEventListener.SetActive(false);
    }
    public void Skill()
    {
        //TODO Партикл и звук волны
        Debug.Log("Boom!");
        OnDestroySkillCasted.Raise();
    }

    public void ChangeDamage(ScoreCounter scoreCounter)
    {
        float damageBoost = scoreCounter.Score * damageBonusCofficient;
        destroyerData.AttackPower = startDamage + damageBoost;
    }

}
