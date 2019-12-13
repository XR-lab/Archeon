using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DangerFire : MonoBehaviour
{
    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Vibration hapticAction;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Fire"))
        {
            Pulse(1, 150, 75, inputSource);
        }
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }
}
