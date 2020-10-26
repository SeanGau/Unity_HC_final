using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Com.B1t.ShotRace
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public GameObject firstCanvas;
        public GameObject controlPanel;
        public GameObject progressLabel;
        // 遊戲版本的編碼, 可讓 Photon Server 做同款遊戲不同版本的區隔.
        string gameVersion = "1";
        [Tooltip("遊戲室玩家人數上限. 當遊戲室玩家人數已滿額, 新玩家只能新開遊戲室來進行遊戲.")]
        public byte maxPlayersPerRoom = 4;

        void Awake()
        {
            // 確保所有連線的玩家均載入相同的遊戲場景
            PhotonNetwork.AutomaticallySyncScene = true;
            firstCanvas.SetActive(true);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKey && firstCanvas.activeSelf)
            {
                firstCanvas.SetActive(false);
            }
        }
        // 與 Photon Cloud 連線
        public void Connect()
        {
            controlPanel.SetActive(false);
            progressLabel.SetActive(true);
            // 檢查是否與 Photon Cloud 連線
            if (PhotonNetwork.IsConnected)
            {
                // 已連線, 嚐試隨機加入一個遊戲室
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // 未連線, 嚐試與 Photon Cloud 連線
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN 呼叫 OnConnectedToMaster(), 已連上 Photon Cloud.");

            // 確認已連上 Photon Cloud
            // 隨機加入一個遊戲室
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN 呼叫 OnDisconnected() {0}.", cause);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN 呼叫 OnJoinRandomFailed(), 隨機加入遊戲室失敗.");

            // 隨機加入遊戲室失敗. 可能原因是 1. 沒有遊戲室 或 2. 有的都滿了.    
            // 好吧, 我們自己開一個遊戲室.
            PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("PUN 呼叫 OnJoinedRoom(), 已成功進入遊戲室中.");
            SceneManager.LoadScene("遊戲場景");
        }
    }
}