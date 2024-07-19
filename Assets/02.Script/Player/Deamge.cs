using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class Deamge : MonoBehaviour
{
    private readonly string E_BulletTag = "E_Bullet";
    public GameObject Blood;


    void Start()
    {

        Blood = Resources.Load("Effects/BulletImpactFleshSmallEffect") as GameObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(E_BulletTag))
        {
            collision.gameObject.SetActive(false);

                // 맞은 위치 Collision 구조체안에 Contacts라는 배열이 있다.
                Vector3 pos = collision.contacts[0].point; //  위치
                Vector3 _nomal = collision.contacts[0].normal; // 방향
                Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _nomal);
                GameObject blood = Instantiate(Blood, pos, rot);
                Debug.Log(Blood);
                Destroy(blood, 1.0f);
            
        }

    }
}
