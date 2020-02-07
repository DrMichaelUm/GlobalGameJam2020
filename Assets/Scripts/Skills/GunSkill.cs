using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class GunSkill : MonoBehaviour, ISkillable
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    [SerializeField] private float maxSkillDistance;
    [SerializeField] private ParticleSystem ps;

    private BoxCollider2D _gunSkillCollider;
    private float _startGunSkillColliderHeight;
    private float _offsetToHeightGunSkillCollider;

    [SerializeField] private Light2D gunShapeLight;
    [SerializeField] private LayerMask mask;

    [Space] 
    
    [SerializeField] private UnityEvent OnShooted;
    [SerializeField] private UnityEvent OnStoppedShooting;

    private float _hitDistance;
    private float _hitToMaxDistance;

    private float _startMaxLifeTime;
    private float _startMinLifeTime;

    private Vector2[] _gunLightPathPositions; //positions of path points on start

    public bool hitted = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _startMaxLifeTime = ps.main.startLifetime.constantMax;
        _startMinLifeTime = ps.main.startLifetime.constantMin;

        _gunLightPathPositions = new Vector2[gunShapeLight.shapePath.Length];

        for (int i = 0; i < _gunLightPathPositions.Length; i++)
            _gunLightPathPositions[i] = gunShapeLight.shapePath[i];

        _gunSkillCollider = GetComponent<BoxCollider2D>();
        //gunSkillCollider.size = new Vector2 (gunSkillCollider.size.x, maxSkillDistance);

        _startGunSkillColliderHeight = _gunSkillCollider.size.y;
        _offsetToHeightGunSkillCollider = _gunSkillCollider.offset.y / _startGunSkillColliderHeight;

        //ChangeGunColliderDistance();
        //Skill();
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
            _hitDistance = Vector2.Distance (hit.point, transform.position);
        else
            _hitDistance = maxSkillDistance;
    }

    private void CalculateHitToMaxDistance()
    {
        _hitToMaxDistance = _hitDistance / maxSkillDistance;
    }

    private void ChangeSkillParticleRange (RaycastHit2D hit)
    {
        var psLifeTime = ps.main;

        if (hit)
        {
            float minLifeTime = _startMinLifeTime * _hitDistance / (maxSkillDistance);
            float maxLifeTime = _startMaxLifeTime * _hitDistance / (maxSkillDistance);

            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (minLifeTime, maxLifeTime);

            //Change light by proportion
            ChangeLightRange (_hitToMaxDistance * .5f);
        }
        else
        {
            psLifeTime.startLifetime = new ParticleSystem.MinMaxCurve (_startMaxLifeTime, _startMinLifeTime);

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
            Vector3 newPos = new Vector2 (gunShapeLight.shapePath[i].x, _gunLightPathPositions[i].y * coef);
            gunShapeLight.shapePath[i] = newPos;
        }
    }

    private void ChangeGunColliderDistance()
    {
        float hitGunColliderHeight = _startGunSkillColliderHeight * _hitToMaxDistance;

        _gunSkillCollider.size = new Vector2 (_gunSkillCollider.size.x, hitGunColliderHeight);

        _gunSkillCollider.offset = _gunSkillCollider.size * _offsetToHeightGunSkillCollider;
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