using UnityEngine;

public class TrapSkill : MonoBehaviour
{
    [SerializeField] private TrapData trapData;

    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private CircleCollider2D trapSkillCollider;
    [SerializeField] private Transform trapsHolder;

    [SerializeField] private ObjectsPool _objectsPool;

    private float _trapSkillRadius;
    
    private GameObject _aimTrapPrefab;
    private GameObject _aimTrap;
    // private Transform _aimsHolder;

    private Camera _mainCamera;

    private bool _aimed = false;

    private void Awake()
    {
        Init();

        _aimTrap = Instantiate (_aimTrapPrefab, Vector2.zero, Quaternion.identity, trapsHolder);
        _aimTrap.SetActive (false);
    }

    private void Init()
    {
        _aimTrapPrefab = trapData.AimTrap;
        _mainCamera = Camera.main;
        _trapSkillRadius = trapSkillCollider.radius;
        
        _objectsPool.parent = trapsHolder;
        _objectsPool.objList.Clear();
    }

    private void Aim()
    {
        if (_aimTrap.gameObject.activeSelf == false)
            _aimTrap.gameObject.SetActive (true);

        _aimTrap.transform.position = GetMousePosition();
    }

    //add it to 'OnRepairGunSkillCasted' event
    public void StopAim()
    {
        _aimTrap.SetActive (false);
        _aimed = false;
    }

    private void Skill()
    {
        SpawnTrap (_aimTrap.transform.position);
        StopAim();
    }

    private void SpawnTrap (Vector2 spawnPos)
    {
        var obj= _objectsPool.GetObject();
        obj.transform.position = spawnPos;

    }

    private Vector2 GetMousePosition()
    {
        Vector3 aimPos = _mainCamera.ScreenToWorldPoint (Input.mousePosition);
        aimPos.z = 0;

        return aimPos;
    }

    private void Update()
    {
        //hold aim in trapSkillRadius
        if (_aimed)
        {
            _aimTrap.transform.position = GetMousePosition();

            float distance = Vector2.Distance (_aimTrap.transform.position, transform.position);

            if (distance > _trapSkillRadius)
            {
                Vector3 fromOriginToObject =
                    _aimTrap.transform.localPosition - transform.localPosition; //~GreenPosition~ - *BlackCenter*
               
                fromOriginToObject *= _trapSkillRadius / distance;              //Multiply by trapSkillRadius //Divide by Distance

                _aimTrap.transform.localPosition =
                    transform.localPosition + fromOriginToObject; //*BlackCenter* + all that Math
            }
        }

        //first click - aim
        if (Input.GetMouseButtonDown (1) && _aimed == false)
        {
            Aim();
            _aimed = true;
        }

        //second click - spawn trap
        else if (Input.GetMouseButtonDown (1) && _aimed)
        {
            Skill();
            _aimed = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere (transform.position, _trapSkillRadius);
    }
}