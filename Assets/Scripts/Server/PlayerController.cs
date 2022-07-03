using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camTransform;
    private float nextTimeToFire = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            if (Constants.selectedWeapon == 0)
            {
                nextTimeToFire = Time.time + 1f / 10f;
                ClientSend.PlayerShoot(camTransform.forward);
                
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Constants.selectedWeapon == 0)
            {
                return;
            }
            ClientSend.PlayerShoot(camTransform.forward);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClientSend.StartReload(Constants.selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach(KeyValuePair<int, PlayerManager> p in GameManager.players)
            {
                if (p.Value.model.enabled)
                    p.Value.model.enabled = false;
                else
                    p.Value.model.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    /// <summary>Sends player input to the server.</summary>
    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.Alpha1),
            Input.GetKey(KeyCode.Alpha2)
        };

        ClientSend.PlayerMovement(_inputs);
    }
}