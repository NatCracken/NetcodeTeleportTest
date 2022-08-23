using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using TMPro;
using UnityEngine;
using System.Text;

public class StatusDisplay : NetworkBehaviour
{
    public TMP_Text statusField;
    void Update()
    {
        StringBuilder builder = new();
        builder.Append("IsClient: " + IsClient);
        builder.Append(", IsHost: " + IsHost);
        builder.Append(", IsServer: " + IsServer);
        statusField.text = builder.ToString();
    }
}
