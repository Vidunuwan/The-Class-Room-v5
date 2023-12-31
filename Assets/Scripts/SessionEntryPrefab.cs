using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SessionEntryPrefab : MonoBehaviour
{
    public TMP_Text sessionName, playerCount;
    public Button joinButton;

    private void Awake()
    {
        joinButton.onClick.AddListener(JoinSession);
    }

    private void Start()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }

    private void JoinSession()
    {
        FusionManager.instance.ConnetToSession(sessionName.text);
    }
}
