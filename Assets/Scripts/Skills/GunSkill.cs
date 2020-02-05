using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunSkill : MonoBehaviour, ISkillable
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    [SerializeField] private UnityEvent OnShooted;
    [SerializeField] private UnityEvent OnStoppedShooting;

    [SerializeField] private float maxSkillDistance;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private LayerMask mask;
    private float startMaxLifeTime;
    private float startMinLifeTime;
    public bool hitted = false;
    private void Start()
    {
        startMaxLifeTime = ps.main.startLifetime.constantMax;
        startMinLifeTime = ps.main.startLifetime.constantMin;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Skill();
        }

        else if (Input.GetMouseButtonUp(0))
            OnStoppedShooting.Invoke();
    }

    private RaycastHit2D GetRaycastHit (Vector2 direction)
    {
        Vector2 startPoint = new Vector2(transform.position.x, transform.position.y);
        Debug.DrawRay(startPoint, direction*maxSkillDistance, Color.red);
        return Physics2D.Raycast(startPoint, direction, maxSkillDistance, mask);
    }

    private void ChangeSkillRange(RaycastHit2D hit)
    {
        var psLifeTime = ps.main;
        if (hit)
        {
            //Debug.Log("Point: " + hit.point + " " + hit.collider.name);
            float distance = Vector2.Distance(hit.point, transform.position);
            //Debug.Log("LifeTime:" +startMinLifeTime+" "+startMaxLifeTime);
            float minLifeTime = startMinLifeTime * distance / (maxSkillDistance);
            float maxLifeTime = startMaxLifeTime * distance / (maxSkillDistance);
            // Debug.Log("LifeTimer:" + minLifeTime+" "+maxLifeTime);


            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve(minLifeTime, maxLifeTime);
        }
        else
        {
            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve(startMaxLifeTime, startMinLifeTime);
        }
    }

    public void GunRaycastHit(RaycastHit2D hit)
    {
        //ItemBehaviour itemBehaviour = null;
        //if (hit)
        //{

        //    if (hit.collider.CompareTag("Item"))
        //    {
        //        itemBehaviour = hit.collider.GetComponent<ItemBehaviour>();
        //        itemBehaviour.itemRepairListener.SetActive(true);
        //        hitted = true;
        //    }
        //    else if (hitted)
        //    {
        //        Debug.Log("TurnOff!!");
        //        itemBehaviour.itemRepairListener.SetActive(false);
        //        hitted = false;
        //    }
        //}
        //else if (hitted)
        //{
        //    if (itemBehaviour != null)
        //    itemBehaviour.itemRepairListener.SetActive(false);
        //    hitted = false;
        //}
    }

    public void Skill()
    {
        RaycastHit2D hit = GetRaycastHit(transform.up);
        GunRaycastHit(hit);
        ChangeSkillRange(hit);
        OnRepairGunSkillCasted.Raise();
        OnShooted.Invoke();
    }
}
