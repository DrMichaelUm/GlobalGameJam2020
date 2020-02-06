using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class GunSkill : MonoBehaviour, ISkillable
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    [SerializeField] private float maxSkillDistance;
    [SerializeField] private ParticleSystem ps;

    private BoxCollider2D gunSkillCollider;
    private float startGunSkillColliderHeight;
    private float startGunSkillColliderOffsetY;

    [SerializeField] private Light2D gunShapeLight;
    [SerializeField] private LayerMask mask;

    [Space] [SerializeField] private UnityEvent OnShooted;
    [SerializeField] private UnityEvent OnStoppedShooting;

    private float hitDistance;
    private float hitToMaxDistance;

    private float startMaxLifeTime;
    private float startMinLifeTime;

    private Vector2[] gunLightPathPositions; //positions of path points on start

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

        gunSkillCollider = GetComponent<BoxCollider2D>();
        startGunSkillColliderHeight = gunSkillCollider.size.y;
        startGunSkillColliderOffsetY = gunSkillCollider.offset.y;
    }

    private void Update()
    {
        if (Input.GetMouseButton (0))
        {
            Skill();
        }

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

    private void CalculateHitDistance (RaycastHit2D hit)
    {
        if (hit)
            hitDistance = Vector2.Distance (hit.point, transform.position);
        else
            hitDistance = maxSkillDistance;
    }

    private void CalculateHitToMaxDistance()
    {
        hitToMaxDistance = hitDistance / maxSkillDistance;
    }

    private void ChangeSkillParticleRange (RaycastHit2D hit)
    {
        var psLifeTime = ps.main;

        if (hit)
        {
            float minLifeTime = startMinLifeTime * hitDistance / (maxSkillDistance);
            float maxLifeTime = startMaxLifeTime * hitDistance / (maxSkillDistance);

            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (minLifeTime, maxLifeTime);
            
            //Change light by proportion
            ChangeLightRange (hitToMaxDistance);
        }
        else
        {
            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (startMaxLifeTime, startMinLifeTime);

            //Don't change range of light
            ChangeLightRange (1);
        }
    }

    /// <summary>
    /// Going throw all path points and change their Y position by 'coef'
    /// </summary>
    /// <param name="coef"></param>
    private void ChangeLightRange (float coef)
    {
        for (int i = 1; i < gunShapeLight.shapePath.Length; i++)
        {
            Vector3 newPos = new Vector2 (gunShapeLight.shapePath[i].x, gunLightPathPositions[i].y * coef);
            gunShapeLight.shapePath[i] = newPos;
        }
    }

    private void ChangeGunColliderDistance()
    {
        float hitGunColliderHeight = startGunSkillColliderHeight * hitToMaxDistance;
        float deltaHeight = Mathf.Abs (hitGunColliderHeight - gunSkillCollider.size.y * hitToMaxDistance);

        gunSkillCollider.size = new Vector2 (gunSkillCollider.size.x, hitGunColliderHeight);

        gunSkillCollider.offset = new Vector2 (gunSkillCollider.offset.x,
                                               startGunSkillColliderOffsetY * hitToMaxDistance + deltaHeight / 2);
    }
    
    public void Skill()
    {
        RaycastHit2D hit = GetRaycastHit (transform.up);
        CalculateHitDistance (hit);
        CalculateHitToMaxDistance();

        ChangeGunColliderDistance();
        ChangeSkillParticleRange (hit);
        OnRepairGunSkillCasted.Raise();
        OnShooted.Invoke();
    }
}