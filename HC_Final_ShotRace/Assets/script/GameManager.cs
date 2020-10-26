using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.B1t.ShotRace
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        // 玩家離開遊戲室時, 把他帶回到遊戲場入口
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("選單");
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        public override void OnPlayerEnteredRoom(Player other)
        {

        }

        public override void OnPlayerLeftRoom(Player other)
        {

        }
    }
}
