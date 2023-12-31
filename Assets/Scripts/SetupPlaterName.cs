using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupPlaterName : MonoBehaviour
{

    [SerializeField] GameObject canves;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] Button enterButton;

    public void SubmitName()
    {
        canves.SetActive(false);
       // FusionManager.instance.ConnetToRunner(nameInputField.text);
    }

    public void ActivateButton()
    {
        enterButton.interactable = true;
    }
}
