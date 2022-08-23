using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class Teleport : NetworkBehaviour
{
    Rigidbody body;
    NetworkTransform nTrans;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        nTrans = GetComponent<NetworkTransform>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            StartCoroutine(WaitAndJumpTask());
        }
        else
        {
            Destroy(GetComponent<Oscilliate>());
            Destroy(GetComponent<Rigidbody>());
        }
    }


    public float yReflectAround = 5;
    public float afterTime = 10f;
    public float offset = 0;
    public JumpType jummpType;
    public enum JumpType
    {
        naive,
        disableInterp,
        teleport,
        setState,
        setStateNoGhostInterp,
    }

    public float disableInterpDelay = 0.5f;
    bool firstJump = true;
    private IEnumerator WaitAndJumpTask()
    {
        if (firstJump)
        {
            yield return new WaitForSeconds(offset);
            firstJump = false;
        }

        Vector3 pos = transform.position;
        pos.y -= yReflectAround;
        pos.y *= -1;
        pos.y += yReflectAround;

        if (jummpType == JumpType.disableInterp) ChangeInterpStateClientRPC(false);
        yield return new WaitForSeconds(disableInterpDelay);

        switch (jummpType)
        {
            default:
            case JumpType.naive:
            case JumpType.disableInterp: //brute force in demo ui
                transform.position = pos;
                break;
            case JumpType.teleport:
                nTrans.Teleport(pos, transform.rotation, transform.localScale);
                break;
            case JumpType.setState: //set state (true) in demo ui
                nTrans.SetState(pos, transform.rotation, transform.localScale, true);
                break;
            case JumpType.setStateNoGhostInterp: //set state (false) in demo ui
                nTrans.SetState(pos, transform.rotation, transform.localScale, false);
                break;
        }
        body.position = pos;

        yield return new WaitForSeconds(disableInterpDelay);
        if (jummpType == JumpType.disableInterp) ChangeInterpStateClientRPC(true);

        yield return new WaitForSeconds(afterTime - (disableInterpDelay + disableInterpDelay));//apply the disableDelay to all actors just to keep them in sync

        StartCoroutine(WaitAndJumpTask());
    }

    [ClientRpc]
    private void ChangeInterpStateClientRPC(bool newState)
    {
        nTrans.Interpolate = newState;
    }
}
