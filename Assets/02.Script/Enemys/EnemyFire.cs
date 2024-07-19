using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private AudioClip firecilp;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playertr;
    [SerializeField] private Transform EnmeyTr;
    [SerializeField] private Transform FirePos;
    [SerializeField] private string Player = "Player";
    private readonly int hashFire = Animator.StringToHash("Fire");

    private float newxtFire = 0.0f; // ���� �ð��� �߻��� �ð� ���� ����
    private readonly float fireRate = 0.1f;// �Ѿ� �߻� ����
    private readonly float damping = 10.0f; // �÷��̾ ���� ȸ���� �ӵ�
    [SerializeField]
    public MeshRenderer muzzleflash;
    public bool isFire = false;
    [Header("Reload")]
    [SerializeField]
    private readonly float reloadTime = 2.0f; // ������ �ð�
    [SerializeField]
    private readonly int maxBullet = 10; // 10�� �϶� ������ �ϱ�����  Max��
    [SerializeField]
    private int curBullet = 0; // ���� �Ѿ� ��
    [SerializeField]
    private bool isReload = false;// ������ ���� 
    [SerializeField]
    private WaitForSeconds reloadWs; // ��ŸƮ �ڷ�ƾ���� �ð� ���� ����
    [SerializeField]
    private AudioClip reloadCilp; // ������ ���� 
    private EnemyAi enemyai;
    private readonly int hashReload = Animator.StringToHash("Reload");
    void Start()
    {
        firecilp = Resources.Load ("Sound/p_shot_gun_1 1") as AudioClip;
        muzzleflash = transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        animator = GetComponent<Animator> ();
        playertr = GameObject.FindWithTag(Player).transform;
        EnmeyTr = transform;
        FirePos = transform.GetChild(3).GetChild(0).GetChild(0).transform;
        curBullet = maxBullet;
        reloadWs = new WaitForSeconds (reloadTime);
        enemyai = GetComponent<EnemyAi> ();
        reloadCilp = Resources.Load("Sound/E_Reload") as AudioClip;
        muzzleflash.enabled = false;
        
    }

   
    void Update()
    {
        if (isFire && !isReload)
        {
            if (Time.time >= newxtFire)
            {
                Fire();
                
                newxtFire = Time.time + fireRate + Random.Range(0.0f, 0.3f);
            }
            Quaternion rot = Quaternion.LookRotation(playertr.position - EnmeyTr.position);
            EnmeyTr.rotation = Quaternion.Slerp(EnmeyTr.rotation, rot, damping * Time.deltaTime);
        }
    }
    
    void Fire()
    {
        
        animator.SetTrigger(hashFire);
        SoundManger.S_Instance.PlaySound(FirePos.transform.position, firecilp);
        
        var E_bullet = ObjectPullingManger.pullingManger.CreatEnBulletPool();
        if (E_bullet != null)
        {
            E_bullet.transform.position = FirePos.position;
            E_bullet.transform.rotation = FirePos.rotation;
            E_bullet.SetActive(true);
        }
       
        isReload = (--curBullet % maxBullet) == 0;
        if (isReload)
        {
            StartCoroutine(Reloading());
        }
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator Reloading()
    {
        animator.SetTrigger(hashReload);
        SoundManger.S_Instance.PlaySound(transform.position,reloadCilp);
    
        yield return reloadWs;
        curBullet = maxBullet;
        isReload = false;
    }
    IEnumerator ShowMuzzleFlash()
    {
        muzzleflash.enabled = true;
        muzzleflash.transform.localScale = Vector3.one * Random.Range(1.5f, 2.3f);
        Quaternion rot = Quaternion.Euler(0f, 0f, Random.RandomRange(0f, 360f));
        muzzleflash.transform.localRotation = rot;
       
        yield return new WaitForSeconds(0.05f);
        muzzleflash.enabled = false;
    }
}
