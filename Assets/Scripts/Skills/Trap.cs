using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour    
{
    [SerializeField] private TrapData trapData;

    [SerializeField] private GameEvent OnTrapSpawned;
    [SerializeField] private GameEvent OnTrapDeactivated;
    [SerializeField] private GameEvent OnDestroyerTrapped;

    [SerializeField] private ParticleSystem trapParticles;

    private float lifetime;
    private CircleCollider2D _circleCollider2D;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _circleCollider2D.enabled = true;
        OnTrapSpawned.Raise();
        //deactivate in 'lifetime' time after spawn
        StartCoroutine (Deactivate (lifetime));
    }

    private void Init()
    {
        lifetime = trapData.Lifetime;
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private IEnumerator Deactivate (float time)
    {
        yield return new WaitForSeconds (time);

        OnTrapDeactivated.Raise();

        DeactivateImmediately();
    }

    public void DeactivateImmediately()
    {
        _circleCollider2D.enabled = false;
        gameObject.SetActive (false);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Destroyer"))
        {
            OnDestroyerTrapped.Raise();
            StartCoroutine (Deactivate (trapData.StunTime));
            //DeactivateImmediately();
            trapParticles.Play();
        }
    }
}