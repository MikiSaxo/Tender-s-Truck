using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchType : MonoBehaviour
{
    public ElementType Type => _type;
    
    [SerializeField] private ElementType _type;
}
