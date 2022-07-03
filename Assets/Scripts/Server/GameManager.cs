using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public ParticleSystem muzzleFlashPrefab;
    public ParticleSystem bloodPrefab;

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

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void PlayMuzzleFlash(Vector3 _pos, Quaternion _rot)
    {
        ParticleSystem muzzleFlash = Instantiate(muzzleFlashPrefab, _pos, _rot);
        muzzleFlash.transform.GetChild(0).gameObject.SetActive(true);
        muzzleFlash.Play();
        Destroy(muzzleFlash.gameObject, .3f);
    }

    public void PlayBlood(Vector3 _pos, Quaternion _rot)
    {
        ParticleSystem bloodObject = Instantiate(bloodPrefab, _pos, _rot);
        bloodObject.Play();
        Destroy(bloodObject.gameObject, .3f);
    }
}