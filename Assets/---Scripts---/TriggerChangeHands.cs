using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeHands : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            PartyManager.Instance.SwitchHandButton();
            AudioManager.Instance.PlaySound("SwitchHands");
        }
    }
}
