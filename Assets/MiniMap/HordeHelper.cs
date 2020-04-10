using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeHelper : MonoBehaviour
{
    public static HordeHelper Instance;

    public GameObject LockedHorde { get; set; }
    public GameObject SelectedHorde { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
}
