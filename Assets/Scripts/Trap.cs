using System;
using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour, IPlayable
{
   [SerializeField] private TrapData trapData;

   [SerializeField] private GameEvent OnTrapSpawned;
   [SerializeField] private GameEvent OnTrapDeactivated;
   [SerializeField] private GameEvent OnDestroyerTrapped;

   private float lifetime;
   
   private void Awake()
   {
      Init();
   }

   private void OnEnable()
   {
      //deactivate in 'lifetime' time after spawn
      OnTrapSpawned.Raise();
      StartCoroutine (Deactivate(lifetime));
   }

   private void Init()
   {
      lifetime = trapData.Lifetime;
   }

   private IEnumerator Deactivate(float time)
   {
      yield return new WaitForSeconds (time);
      
      OnTrapDeactivated.Raise();
      
      DeactivateImmediately();
   }

   public void DeactivateImmediately()
   {
      gameObject.SetActive (false);
   }

   //set to 'OnDestroyerTrapped'
   public void Skill()
   {
      //TODO EFFECT
      Debug.Log ("TRAPPED");
   }

   private void OnTriggerEnter2D (Collider2D other)
   {
      if (other.CompareTag ("Destroyer"))
         OnDestroyerTrapped.Raise();
   }
}
