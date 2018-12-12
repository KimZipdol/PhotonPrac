using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : PunBehaviour 
{
    public static GameObject LocalPlayerInstance;
    public CameraWork cameraWork;

    Animator animator;

    private void Awake()
    {
        if(photonView.isMine) //포톤뷰에서 내 클라이언트에서 조작하는 오브젝트를 찾아놓는다
        {
            Player.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(cameraWork != null)
        {
            if(photonView.isMine)
            {
                cameraWork.OnStartFollowing();
            }
        }
    }

    private void Update()
    {
        if(photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * 150.0f, 0);
        transform.Translate(0, 0, z * Time.deltaTime * 3.0f);

        animator.SetFloat("Speed", z);
    }
}
