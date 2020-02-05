using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairerAffectBehaviour : PlayerAffects
{
    [SerializeField] private GameObject destroySkillCastEventListener;
    [SerializeField] private PlayerData destroyerData;
    void Start()
    {
        destroySkillCastEventListener.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyWave") && !isKnocked)
            destroySkillCastEventListener.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyWave"))
            destroySkillCastEventListener.SetActive(false);
    }

    public void KnockBack()
    {
        KnockBack(destroySkillCastEventListener, destroyerData);
    }
}
