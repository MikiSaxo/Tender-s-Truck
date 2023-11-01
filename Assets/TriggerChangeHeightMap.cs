using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeHeightMap : MonoBehaviour
{
    enum SideHeight
    {
        Minus = -1,
        Plus = 1
    }

    [SerializeField] private SideHeight _whichSide;
    [SerializeField] private MapHeight _mapHeight;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            _mapHeight.ChangeHeight((int)_whichSide);
            AudioManager.Instance.PlaySound("ChangeHeight");
        }
    }
}
