using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using System;
using Fusion.Sockets;



public class PlayerStat : NetworkBehaviour
{
   //[Networked(OnChanged = nameof()] public NetworkString<_32> PlayerName { get; set; }
    [SerializeField] TMP_Text playerNameLabel;

    [Networked] public float Health { get; set; }


    private void Start()

    {
        if (this.HasInputAuthority)
        {
           //PlayerName = FusionManager.instance._playername;
        }
       
    }
    /*protected static void UpdatePlayerName(Changed<PlayerStat> changed)
    {
        changed.Behaviour.playerNameLabel.text = changed.Behaviour.PlayerName.ToStrng();
    }*/
}
