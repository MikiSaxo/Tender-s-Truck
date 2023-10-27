using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowBeat : MonoBehaviour
{
    private void Start()
    {
    }
    public void Grow()
    {
        //transform.localScale = transform.localScale * 1.1f;
        transform.DOPunchScale(Vector3.one * .1f, .2f, 5);
    }
}
