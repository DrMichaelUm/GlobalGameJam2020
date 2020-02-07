﻿using System.Collections;
using UnityEngine;

public class DestroyerAffect : PlayerAffects
{
    [SerializeField] private GameObject repairCastTriggerEventListener;
    [SerializeField] private GameObject itemDestroyedEventListener;

    [SerializeField] private PlayerData destroyerData;
    [SerializeField] private PlayerData repeirerData;

    [SerializeField] private GameEvent OnDestroyerTrapped;

    [SerializeField] private float damageBonusCofficient;
    
    private float startDamage = 20;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        destroyerData.AttackPower = startDamage;
        
        repairCastTriggerEventListener.SetActive (false);
        itemDestroyedEventListener.SetActive (false);
        startDamage = destroyerData.AttackPower;
        rb = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();
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