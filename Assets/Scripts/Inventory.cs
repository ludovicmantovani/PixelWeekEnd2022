using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance
    {
        get;
        set;
    }

    public static List<WorkOfArt> worksOfArt;

    public static void SetWorksOfArt(List<WorkOfArt> workList)
    {
        worksOfArt = new List<WorkOfArt>();
        foreach (WorkOfArt item in workList)
        {
            worksOfArt.Add(item);
        }
    }


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }
}
