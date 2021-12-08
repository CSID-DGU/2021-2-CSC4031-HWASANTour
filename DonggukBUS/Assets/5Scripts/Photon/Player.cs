using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player : MonoBehaviourPun
{

    [SerializeField] public float speed;
    public bool IsMasterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    public GameObject playera;
    public TextMesh playerName;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer spriteRenderer;



    private void Start()
    {
        if (photonView.IsMine)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            playerName.text = LoginManager.LoginUserNickname;
            Debug.Log("Update() - PhotonView is not mine");
            return;
        }
        else
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            transform.position += input.normalized * speed * Time.deltaTime;
            playerName.text = "³ª";
            playerName.color = new Color(255, 10, 10);
            //playerRigidbody.MovePosition(transform.position);
        }

        //  var distance = input * speed * Time.deltaTime;
        //  var targetPosition = transform.position + Vector3.up * distance;

        //  playerRigidbody.MovePosition(targetPosition);
    }

}