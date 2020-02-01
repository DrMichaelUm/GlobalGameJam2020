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
    private PlayerData repeirerData;

    [SerializeField] private GameEvent OnDestroySkillCasted;
    [SerializeField] private GameEvent OnDestroyerTrapped;

    [SerializeField]
    private float damageBonusCofficient;
    private float startDamage;

    private Rigidbody2D rb;
    private Vector2 knockForceDirection;
    private bool isKnocked = false;
    private float timer = 0.55f;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();


    private void Start()
    {
        repairCastTriggerEventListener.SetActive(false);
        itemDestroyedEventListener.SetActive(false);
        startDamage = destroyerData.AttackPower;
        rb = GetComponent<Rigidbody2D>();
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
        if (collision.CompareTag("RepairWave") && !isKnocked)
            repairCastTriggerEventListener.SetActive(true);
        if (collision.CompareTag("Item"))
            itemDestroyedEventListener.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RepairWave"))
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

    public void OnTrapped(TrapData trapData)
    {
        StartCoroutine(OnTrappedCoroutine(trapData.StunTime));
    }

    private IEnumerator OnTrappedCoroutine(float stunTime)
    {
        OnDestroyerTrapped.Raise();

        //speed = 0

        yield return new WaitForSeconds(stunTime);

        //speed = normal;
    }

    private IEnumerator AddImpulseProcess()
    {
        Vector2 forceDirection = Random.insideUnitCircle.normalized;
        float speed = repeirerData.Strength;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            rb.AddForce(forceDirection*speed, ForceMode2D.Force);
            speed -= 5f;
            yield return waitForFixedUpdate;
        }
        timer = 0.55f;
    }

    private IEnumerator KnockingImmuneTime()
    {
        yield return new WaitForSeconds(3f);
        isKnocked = false;
    }
    public void KnockBack()
    {
        isKnocked = true;
        Debug.Log("KnockingBack!");
        repairCastTriggerEventListener.SetActive(false);
        StopCoroutine(AddImpulseProcess());
        StartCoroutine(AddImpulseProcess());
        StartCoroutine(KnockingImmuneTime());
    }

}
