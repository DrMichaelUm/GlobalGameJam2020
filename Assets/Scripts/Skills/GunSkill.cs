using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class GunSkill : MonoBehaviour, ISkillable
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    [SerializeField] private float maxSkillDistance;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Light2D gunShapeLight;
    [SerializeField] private LayerMask mask;

    [Space] [SerializeField] private UnityEvent OnShooted;
    [SerializeField] private UnityEvent OnStoppedShooting;

    private float hitDistance;

    private float startMaxLifeTime;
    private float startMinLifeTime;

    private Vector2[] gunLightPathPositions;                        //positions of path points on start

    public bool hitted = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        startMaxLifeTime = ps.main.startLifetime.constantMax;
        startMinLifeTime = ps.main.startLifetime.constantMin;

        gunLightPathPositions = new Vector2[gunShapeLight.shapePath.Length];

        for (int i = 0; i < gunLightPathPositions.Length; i++)
            gunLightPathPositions[i] = gunShapeLight.shapePath[i];
    }

    private void Update()
    {
        if (Input.GetMouseButton (0))
            Skill();

        else if (Input.GetMouseButtonUp (0))
        {
            OnStoppedShooting.Invoke();
        }
    }

    private RaycastHit2D GetRaycastHit (Vector2 direction)
    {
        Vector2 startPoint = new Vector2 (transform.position.x, transform.position.y);
        Debug.DrawRay (startPoint, direction * maxSkillDistance, Color.red);

        return Physics2D.Raycast (startPoint, direction, maxSkillDistance, mask);
    }

    private void ChangeSkillRange (RaycastHit2D hit)
    {
        var psLifeTime = ps.main;

        if (hit)
        {
            //Debug.Log("Point: " + hit.point + " " + hit.collider.name);
            float distance = Vector2.Distance (hit.point, transform.position);
            //Debug.Log("LifeTime:" +startMinLifeTime+" "+startMaxLifeTime);
            float minLifeTime = startMinLifeTime * distance / (maxSkillDistance);
            float maxLifeTime = startMaxLifeTime * distance / (maxSkillDistance);
            // Debug.Log("LifeTimer:" + minLifeTime+" "+maxLifeTime);

            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (minLifeTime, maxLifeTime);

            hitDistance = distance;
            
            //proportion of hit distance to max skill distance
            float hitToMaxDistance = hitDistance / maxSkillDistance;
            //Change light by proportion
            ChangeLightRange (hitToMaxDistance);
        }
        else
        {
            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (startMaxLifeTime, startMinLifeTime);
            
            //reset 'hitDistance' meaning that ray is casting nothing 
            hitDistance = 0;
            //Don't change range of light
            ChangeLightRange (1);
        }
    }

    /// <summary>
    /// Going throw all path points and change their Y position by 'coef'
    /// </summary>
    /// <param name="coef"></param>
    private void ChangeLightRange(float coef)
    {
        for (int i = 1; i < gunShapeLight.shapePath.Length; i++)
        {
            Vector3 newPos = new Vector2 (gunShapeLight.shapePath[i].x, gunLightPathPositions[i].y * coef);
            gunShapeLight.shapePath[i] = newPos;
        }
    }

    public void GunRaycastHit (RaycastHit2D hit)
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
        RaycastHit2D hit = GetRaycastHit (transform.up);
        GunRaycastHit (hit);
        ChangeSkillRange (hit);
        OnRepairGunSkillCasted.Raise();
        OnShooted.Invoke();
    }
}