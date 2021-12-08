using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PhotonManager>();

            return instance;
        }
    }

    private static PhotonManager instance;
    
    public Transform[] spawnPositions;
    public GameObject playerPrefab;


    private void Start()
    {
        SpawnPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonManager.Start() - Master Clinet");
        }
    }

    private void SpawnPlayer()
    {
        // 로컬 플레이어 수 관리 그리고, 마스터 클라이언트가 일반 클라이언트에 공유되지 않는 것에 대한 이슈를 해결하려면, 
        // 공은 마스터 클라이언트일 떄만 생성하고, 각 클라이언트들은(로컬)에서 움직임을 마스터에게 공유하는 방식임. 
        // 실제 예제 프로젝트에서 각 포톤뷰 아이디가 어떻게 관리되는 지도 확인해봐야 함.
        var localPlayerIndex = 0;
        Debug.Log("localPlayerIndex : " + localPlayerIndex);
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        Debug.Log("spawnPosition : " + spawnPosition);
        // Param : position, rotation -> 우리가 로그인 후 메타버스에서 캐릭터가 초기화되는 위치를 넣어줘야 함. 
        Debug.Log("name : " + playerPrefab.name
            + "\n positon : " + spawnPosition.position
            + "\n rotation : " + spawnPosition.rotation);
        PhotonNetwork.Instantiate(LoginManager.LoginUserNickname, spawnPosition.position, Quaternion.identity); 
        // PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation)
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Photonmanager.cs");
        SceneManager.LoadScene("Lobby");
    }

}