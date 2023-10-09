using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croquette : MonoBehaviour
{
    public ElementType Type => _type;
    
    [SerializeField] private ElementType _type;
}
