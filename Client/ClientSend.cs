using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);
    }
    public static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.udp.SendData(packet);
    }
    #region Packets
    public static void WelcomeRecieved()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.instance.myId);
            packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(packet);
        }
    }
    public static void Playermovment(bool[] input)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(input.Length);
            foreach(bool _input in input)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager2.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }
    public static void PlayerShoot(Vector3 _facing)
    {
        using(Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerRunning(int mult)
    {
        using(Packet _packet = new Packet((int)ClientPackets.playerRunning))
        {
            _packet.Write(mult);
            SendTCPData(_packet);
        }
    }
    public static void Playerbools(bool Ismoving,bool isJumping)
    {
        using(Packet _packet = new Packet((int)ClientPackets.playerbools))
        {
            _packet.Write(Ismoving);
            _packet.Write(isJumping);
            SendUDPData(_packet);
        }
    }
    #endregion

}
