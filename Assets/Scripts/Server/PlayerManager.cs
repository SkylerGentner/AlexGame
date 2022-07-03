using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float maxHealth = 100f;
    public float health;
    public MeshRenderer model;
    public Gun primary;
    public Gun secondary;
    public GameObject playerObj;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    public void SetHealth(int _id, float _health)
    {
        GameManager.players[_id].health = _health;
        if (id == _id)
        {
            UIManager.instance.SetHealth(GameManager.players[_id].health.ToString());
        }
        if (GameManager.players[_id].health <= 0f)
        {
            Die(_id);
        }
    }

    public void Die(int _id)
    {
        GameManager.players[_id].model.enabled = false;
    }

    public void Respawn(int _id)
    {
        SetHealth(_id, maxHealth);
        GameManager.players[_id].model.enabled = true; 
    }

    public void SetActiveWeapon(int _id, int _selectedWeapon)
    {
        if(_selectedWeapon == 0)
        {
            primary.model.SetActive(true);
            secondary.model.SetActive(false);
            Constants.selectedWeapon = 0;
            UIManager.instance.ChangeAmmoUI();
        }
        if (_selectedWeapon == 1)
        {
            primary.model.SetActive(false);
            secondary.model.SetActive(true);
            Constants.selectedWeapon = 1;
            UIManager.instance.ChangeAmmoUI();
        }
    }
    public void Reload(string _gun)
    {
        if (primary.model.name.Equals(_gun))
        {
            primary.reloadAnimation.Play();
        }
        else if (secondary.model.name.Equals(_gun))
        {
            secondary.reloadAnimation.Play();
        }
    }
}