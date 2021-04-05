using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject Playerprefab;

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

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        Server.Start(50, 26950);

    }
    private void OnApplicationQuit()
    {
        Server.Stop();
    }
    public Player InstantiatePlayer()
    {
        Debug.Log("Player spawned");
        return Instantiate(Playerprefab, new Vector3(0f,0.5f,0f), Quaternion.identity).GetComponent<Player>();
    }
}
