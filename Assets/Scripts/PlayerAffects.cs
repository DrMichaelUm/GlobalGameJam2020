using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAffects : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float timer = 0.55f;
    protected bool isKnocked;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    protected void KnockBack(GameObject eventListener, PlayerData opponentData)
    {
        isKnocked = true;
        eventListener.SetActive (false);
        // StopCoroutine(AddImpulseProcess());
        StartCoroutine (AddImpulseProcess(opponentData));
        StartCoroutine (KnockingImmuneTime());
    }

    private IEnumerator AddImpulseProcess(PlayerData opponentData)
    {
        Vector2 forceDirection = Random.insideUnitCircle.normalized;
        float speed = opponentData.Strength;

        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            rb.AddForce(forceDirection * speed, ForceMode2D.Force);
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
}
