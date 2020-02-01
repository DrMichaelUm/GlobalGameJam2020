using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ObjectsPool", fileName = "New Pool", order = 57)]
public class ObjectsPool : ScriptableObject
{
    [SerializeField] private GameObject prefab;

    [HideInInspector] public Transform parent;

    [HideInInspector] public List<GameObject> objList = new List<GameObject> ();

    public GameObject GetObject()
    {
        GameObject newObj = FindInactive (objList); //ищем доступный для переиспользования объект

        if (newObj == null) //если таких нет
        {
            newObj = Instantiate (prefab);
            // objList.Add(newObj);
        }

        newObj.transform.SetParent (parent);
        objList.Add (newObj);

        return newObj;
    }

    private GameObject FindInactive (List<GameObject> list) //ищем доступный для переиспользования объект
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                list[i].SetActive (true);

                return list[i];
            }
        }

        return null;
    }
}