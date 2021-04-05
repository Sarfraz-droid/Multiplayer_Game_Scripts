using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public GameObject localPlayerPrefab;
    public GameObject PlayerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying objects");
            Destroy(this);
        }
    }
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(PlayerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
    public void externalanim(int _id, bool isMoving,bool isJumping)
    { 

        if(_id != Client.instance.myId)
        {
            Animator anim = players[_id].GetComponentInChildren<Animator>();

            if(isMoving)
            {
                anim.SetBool("isMoving",true);
            }else
            {
                anim.SetBool("isMoving", false);
            }

            if (isJumping)
            {
                anim.SetBool("isJumping", true);
            }
            else
                anim.SetBool("isJumping", false);
        }
    }
}
