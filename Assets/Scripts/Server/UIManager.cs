using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipInput;
    public TextMeshProUGUI team1Score;
    public TextMeshProUGUI team2Score;
    public TextMeshProUGUI primaryAmmo;
    public TextMeshProUGUI secondaryAmmo;
    public TextMeshProUGUI healthNum;
    public Camera startCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SetPrimaryAmmo(string _ammoCount, string _maxAmmoCount)
    {
        primaryAmmo.SetText(_ammoCount + " / " + _maxAmmoCount);
    }

    public void SetSecondaryAmmo(string _ammoCount, string _maxAmmoCount)
    {
        secondaryAmmo.SetText(_ammoCount + " / " + _maxAmmoCount);
    }

    public void ChangeAmmoUI()
    {
        primaryAmmo.gameObject.SetActive(!primaryAmmo.gameObject.activeSelf);
        secondaryAmmo.gameObject.SetActive(!secondaryAmmo.gameObject.activeSelf);
    }

    public void SetHealth(string _health)
    {
        healthNum.SetText(_health);
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        ipInput.interactable = false;
        startCamera.enabled = false;
        team1Score.gameObject.SetActive(true);
        team2Score.gameObject.SetActive(true);
        primaryAmmo.gameObject.SetActive(true);
        healthNum.gameObject.SetActive(true);
        Client.instance.ConnectToServer();
    }
}