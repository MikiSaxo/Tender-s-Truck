using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowCube : MonoBehaviour
{
    private void Start()
    {
    }
    public void Grow()
    {
        //transform.localScale = transform.localScale * 1.1f;
        transform.DOPunchScale(Vector3.one * .11f, .1f, 5);
    }
}
