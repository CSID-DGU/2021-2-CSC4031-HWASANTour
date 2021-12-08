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
        // ���� �÷��̾� �� ���� �׸���, ������ Ŭ���̾�Ʈ�� �Ϲ� Ŭ���̾�Ʈ�� �������� �ʴ� �Ϳ� ���� �̽��� �ذ��Ϸ���, 
        // ���� ������ Ŭ���̾�Ʈ�� ���� �����ϰ�, �� Ŭ���̾�Ʈ����(����)���� �������� �����Ϳ��� �����ϴ� �����. 
        // ���� ���� ������Ʈ���� �� ����� ���̵� ��� �����Ǵ� ���� Ȯ���غ��� ��.
        var localPlayerIndex = 0;
        Debug.Log("localPlayerIndex : " + localPlayerIndex);
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        Debug.Log("spawnPosition : " + spawnPosition);
        // Param : position, rotation -> �츮�� �α��� �� ��Ÿ�������� ĳ���Ͱ� �ʱ�ȭ�Ǵ� ��ġ�� �־���� ��. 
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