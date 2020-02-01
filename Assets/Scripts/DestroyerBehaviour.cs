using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour, IPlayable
{
    [SerializeField]
    private GameObject repairGunEffectEventListener;
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
        repairGunEffectEventListener.SetActive(false);
        itemDestroyedEventListener.SetActive(false);
        destroyerData.AttackPower = destroyerData.StartAttackPower;
        startDamage = destroyerData.StartAttackPower;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Skill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyWave"))
            repairGunEffectEventListener.SetActive(true);
        if (collision.CompareTag("Item"))
            itemDestroyedEventListener.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyWave"))
            repairGunEffectEventListener.SetActive(false);
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
