using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_id, _health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();
        foreach (KeyValuePair<int, PlayerManager> player in GameManager.players)
        {
            player.Value.Respawn(_id);
        }
    }

    public static void GameInformation(Packet _packet)
    {
        UIManager.instance.team1Score.text = (_packet.ReadInt()).ToString();
        UIManager.instance.team2Score.text = (_packet.ReadInt()).ToString();
    }

    public static void PlayerActiveWeapon(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int _selectedWeapon = _packet.ReadInt();

        GameManager.players[_id].SetActiveWeapon(_id, _selectedWeapon);
    }

    public static void PlayerAmmoCount(Packet _packet)
    {
        int _id = _packet.ReadInt();
        //put ui manager in gamemanager.player
        UIManager.instance.SetPrimaryAmmo(_packet.ReadInt().ToString(), _packet.ReadInt().ToString());
        UIManager.instance.SetSecondaryAmmo(_packet.ReadInt().ToString(), _packet.ReadInt().ToString());
    }

    public static void MuzzleFlash(Packet _packet)
    {
        GameManager.instance.PlayMuzzleFlash(_packet.ReadVector3(), _packet.ReadQuaternion());
    }

    public static void PlayBlood(Packet _packet)
    {
        GameManager.instance.PlayBlood(_packet.ReadVector3(), _packet.ReadQuaternion());
    }

    public static void Reload(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _gun = _packet.ReadString();
        GameManager.players[_id].Reload(_gun);
    }
}