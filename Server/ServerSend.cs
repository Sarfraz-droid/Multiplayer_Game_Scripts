using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class ServerSend
{
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }
    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }
    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }
    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }
    #region Packets
    public static void welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
            Debug.Log($"Welcome sent");
        }
    }
    public static void SpawnPlayer(int _toClient, Player _player)
    {

        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);
            _packet.Write(_player.transform.rotation);
            Debug.Log("Packet Written 1 " + _player.id + " " + _player.username + " " + _player.transform.rotation + " " + _player.transform.position);
            SendTCPData(_toClient, _packet);
        }
    }
    public static void PlayerPosition(Player player)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerPosition))
        {
            packet.Write(player.id);
            packet.Write(player.transform.position);

            SendUDPDataToAll(packet);
        }
    }

    public static void PlayerRotation(Player player)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerRotation))
        {
            packet.Write(player.id);
            packet.Write(player.transform.rotation);

            SendUDPDataToAll(player.id, packet);
        }
    }
    public static void PlayerDisconnected(int _playerid)
    {
        using (Packet packet = new Packet((int)ServerPackets.playerDisconnected))
        {
            packet.Write(_playerid);

            SendTCPDataToAll(packet);
        }
    }
    public static void PlayerHealth(Player player)
    {
        using(Packet packet = new Packet((int)ServerPackets.playerHealth))
        {
            packet.Write(player.id);
            packet.Write(player.health);

            SendTCPDataToAll(packet);
        }
    }

    public static void playerRespawned(Player player)
    {
        using(Packet packet = new Packet((int)ServerPackets.playerRespawned))
        {
            packet.Write(player.id);

            SendTCPDataToAll(packet);
        }
    }
    public static void Playerbool(Player player)
    {
        using(Packet packet = new Packet((int)ServerPackets.playeranim))
        {
            packet.Write(player.id);
            packet.Write(player.isMoving);
            packet.Write(player.isJumping);
            SendTCPDataToAll(packet);
        }
    }
    #endregion
}
