using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            // 첫번째 충돌 지점의 정보, 모든 충돌 지점의 정보는 GetContacts로 가져오기 
            ContactPoint cp = collision.GetContact(0); 

            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            Destroy(spark, 0.5f);

            Destroy(collision.gameObject);
        }
    }
}
