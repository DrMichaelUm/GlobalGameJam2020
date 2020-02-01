using UnityEngine;

public class TrapSkill : MonoBehaviour
{
    [SerializeField] private TrapData trapData;

    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private Transform trapsHolder;

    private GameObject aimTrapPrefab;
    private GameObject aimTrap;
    private Transform aimsHolder;

    private Camera mainCamera;

   [SerializeField] private bool _aimed = false;

    private void Awake()
    {
        Init();

        aimTrap = Instantiate (aimTrapPrefab, Vector2.zero, Quaternion.identity, aimsHolder);
        aimTrap.SetActive (false);
    }

    private void Init()
    {
        aimTrapPrefab = trapData.AimTrap;
        mainCamera = Camera.main;
    }

    private void Aim()
    {
        if (aimTrap.gameObject.activeSelf == false)
            aimTrap.gameObject.SetActive (true);

        aimTrap.transform.position = GetMousePosition();
    }

    //add it to 'OnRepairGunSkillCasted' event
    public void StopAim()
    {
        aimTrap.SetActive (false);
    }

    private void Skill()
    {
        SpawnTrap (GetMousePosition());
        StopAim();
    }

    private void SpawnTrap (Vector2 spawnPos)
    {
        Instantiate (trapPrefab, spawnPos, Quaternion.identity, trapsHolder);
    }

    private Vector2 GetMousePosition()
    {
        Vector3 aimPos = mainCamera.ScreenToWorldPoint (Input.mousePosition);
        aimPos.z = 0;

        return aimPos;
    }

    private void Update()
    {
        if (_aimed)
            aimTrap.transform.position = GetMousePosition();

        if (Input.GetMouseButtonDown (1) && _aimed == false)
        {
            Aim();
            _aimed = true;
        }

        else if (Input.GetMouseButtonDown (1) && _aimed)
        {
            Skill();
            _aimed = false;
        }
    }
}