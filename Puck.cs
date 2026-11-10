using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [Header("Puck setting")]
    public int puckNumber;
    [HideInInspector]
    public bool isSorted = false;
    [HideInInspector]
    public bool isCarried = false;
}
