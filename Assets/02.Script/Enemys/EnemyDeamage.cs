using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeamage : MonoBehaviour
{
    [SerializeField]
    private readonly string BulletTag = "Bullet";
    [SerializeField]
    public GameObject BloodEffect;
    [SerializeField]
    private float Hp = 100;
    void Start()
    {
        BloodEffect = Resources.Load("Effects/BulletImpactFleshSmallEffect") as GameObject;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(BulletTag))
        {
            other.gameObject.SetActive(false);
            ShowBloodEffect(other);
            Hp -= other.gameObject.GetComponent<Bullet>().Damage;
            Hp = Mathf.Clamp(Hp, 0f, 100f);
            if (Hp <= 0f)
            {
                Die();
            }
        }
    }

    private void ShowBloodEffect(Collision other)
    {
        
        // ���� ��ġ Collision ����ü�ȿ� Contacts��� �迭�� �ִ�.
        Vector3 pos = other.contacts[0].point; //  ��ġ
        Vector3 _nomal = other.contacts[0].normal; // ����
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _nomal);
        GameObject blood = Instantiate(BloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
    void Die()
    {
       
        GetComponent<EnemyAi>().state = EnemyAi.State.DIE;
    }
}
