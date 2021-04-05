using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle : MonoBehaviour
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username);
        // TODO: send player into game
    }
    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion rotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, rotation);
    }

    public static void Playershoot(int _fromClient,Packet _packet)
    {
        Vector3 shootdirection = _packet.ReadVector3();

        Server.clients[_fromClient].player.Shoot(shootdirection);
    }

    public static void Speed(int _fromClient,Packet _packet)
    {
        int multiplier = _packet.ReadInt();

        Server.clients[_fromClient].player.Multiplier(multiplier);
    }
}
