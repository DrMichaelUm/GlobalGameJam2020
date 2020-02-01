using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Counter", menuName = "Item Counter", order = 54)]
public class DestroyedItemCounter : ScriptableObject
{
    [SerializeField]
    private int destroedItemNumber;

    public int DeItemNum
    {
        get
        {
            return destroedItemNumber;
        }

        set
        {
            destroedItemNumber = value;
        }
    }

}
