using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이가 되었을때 배럴 색상이 랜덤하게 만들기
// 5번 배럴이 총알에 폭파 물리현상 만들기
public class BarrelCtrl : MonoBehaviour
{
    [SerializeField]
    private Texture[] textures;
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private int HitCount = 0;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private string BulletTag = "Bullet";
    private string E_BulletTag = "E_Bullet";
    [SerializeField]
    private GameObject ExplosionPrefad;
    private AudioClip clip;
    private AudioClip BoomCilp;
    private string CameraTag = "MainCamera";
 
   

    void Start()
    {
        ExplosionPrefad = Resources.Load("EffectBoom") as GameObject;
        rb = GetComponent<Rigidbody>();
        textures = Resources.LoadAll<Texture>("BarrelTextures");
        // textrues = Resources.LoadAll("")as <Texture>;
        mesh = GetComponent<MeshRenderer>();
        mesh.material.mainTexture = textures[Random.Range(0, textures.Length)];
        clip = Resources.Load("Sound/bullet_hit_metal_enemy_1") as AudioClip;
        BoomCilp = Resources.Load("Sound/grenade_exp2") as AudioClip;
       
      

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(BulletTag)||other.gameObject.CompareTag(E_BulletTag))
        {
           other.gameObject.SetActive(false);
            SoundManger.S_Instance.PlaySound(transform.position, clip);
            if (++HitCount == 5)
            {
                ExplosionBarrel();
                CamerSek.instance.TurnOn();
                
            }
            
            
        }
    }
    void ExplosionBarrel()
    {
        
        GameObject Effect = Instantiate(ExplosionPrefad,transform.position, Quaternion.identity);
        Destroy(Effect, 2f);
        Collider[] colls = Physics.OverlapSphere(transform.position, 20f);
        // 배럴 자기자신 위치에서 20변경에 배럴 레이어만 Cols 라는 배열에 담는다.
      
        foreach (Collider coll in colls)
        {
            Rigidbody rigidbody = coll.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
               
                SoundManger.S_Instance.PlaySound(transform.position, BoomCilp);
                rigidbody.mass = 1.0f;
                rigidbody.AddExplosionForce(500, transform.position, 10f, 1000f);
                Destroy(gameObject,2.0f);
               
                
            }
            Invoke("BerralMassOrginal",1f);

          
            // 리지드바디 클래스 폭파 함수는 AddExplosionForce(폭파력,위치, 반경, 위로 솟구치는 힘)을 의미한다.
        }
    }
    void BerralMassOrginal()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 100.0f);
        // 배럴 자기자신 위치에서 20변경에 배럴 레이어만 Cols 라는 배열에 담는다.

        foreach (Collider coll in colls)
        {
            Rigidbody rigidbody = coll.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                
                rigidbody.mass = 60f;
                

            }
           


           
        }

    }
}
