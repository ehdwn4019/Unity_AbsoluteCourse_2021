using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fire : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;

    private ParticleSystem muzzleFlash;

    private PhotonView pv;
    private bool isMouseClick => Input.GetMouseButtonDown(0);

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine && isMouseClick)
        {
            FireBulle();
            pv.RPC("FireBulle", RpcTarget.Others, null);
        }
    }

    [PunRPC]
    void FireBulle()
    {
        if(!muzzleFlash.isPlaying) muzzleFlash.Play(true);

        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
