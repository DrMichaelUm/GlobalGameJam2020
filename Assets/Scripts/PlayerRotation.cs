﻿using UnityEngine;

public class PlayerRotation : MonoBehaviour, IRotatible
{
    void Update()
    {
        RotateTo (GetDirection());
    }

    public float GetDirection()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotation;
    }

    public void RotateTo (float rotation)
    {
        transform.rotation = Quaternion.Euler (0f, 0f, rotation);
    }
}
