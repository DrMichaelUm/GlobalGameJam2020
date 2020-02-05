using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyerBehaviour : PlayerAffects, IPlayable
{
    [SerializeField] private GameObject repairCastTriggerEventListener;
    [SerializeField] private GameObject itemDestroyedEventListener;

    [SerializeField] private PlayerData destroyerData;
    [SerializeField] private PlayerData repeirerData;

    [SerializeField] private GameEvent OnDestroySkillCasted;
    [SerializeField] private GameEvent OnDestroyerTrapped;

    [SerializeField] private float damageBonusCofficient;
    
    private float startDamage = 20;

    private PlayerMovement _playerMovement;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        destroyerData.AttackPower = startDamage;
        
        repairCastTriggerEventListener.SetActive (false);
        itemDestroyedEventListener.SetActive (false);
        startDamage = destroyerData.AttackPower;
        rb = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            Skill();
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag ("RepairWave") && !isKnocked)
            repairCastTriggerEventListener.SetActive (true);

        if (collision.CompareTag ("Item"))
            itemDestroyedEventListener.SetActive (true);
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag ("RepairWave"))
            repairCastTriggerEventListener.SetActive (false);

        if (collision.CompareTag ("Item"))
            itemDestroyedEventListener.SetActive (false);
    }

    public void Skill()
    {
        //TODO Партикл и звук волны
        Debug.Log ("Boom!");
        OnDestroySkillCasted.Raise();
    }

    public void ChangeDamage (ScoreCounter scoreCounter)
    {
        float damageBoost = scoreCounter.Score * damageBonusCofficient;
        destroyerData.AttackPower = startDamage + damageBoost;
    }

    public void OnTrapped (TrapData trapData)
    {
        StartCoroutine (OnTrappedCoroutine (trapData.StunTime));
    }

    private IEnumerator OnTrappedCoroutine (float stunTime)
    {
        float normalSpeed = _playerMovement.MoveSpeed;

        //OnDestroyerTrapped.Raise();

        //freeze the player
        _playerMovement.MoveSpeed = 0;

        yield return new WaitForSeconds (stunTime);

        //reset the player speed
        _playerMovement.MoveSpeed = normalSpeed;
    }

    public void KnockBack()
    {
        KnockBack(repairCastTriggerEventListener, repeirerData);
    }
}