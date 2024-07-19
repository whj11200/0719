using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPullingManger : MonoBehaviour
{
    public static ObjectPullingManger pullingManger;
    [SerializeField]
    
    private GameObject bullet; // 
    private GameObject E_bullet;
    public int maxPool = 10;// ������Ʈ Ǯ�� �� ����
    // List � �ڷ����� ���� ���� �� ���� �� �迭�� ���� ��°�
    public List<GameObject> bulletPoolList;
    public List <GameObject> E_bulletPoolList;
    [Header("EnemyObjectPool")]
    public GameObject EnemyPrefad;
    public List<GameObject> EnemyPoolList;
    public List<Transform> SpwanPointsList;

    void Start()
    {
        var spwanPoint = GameObject.Find("SpwanPoints");
        if (spwanPoint != null)
        {
            spwanPoint.GetComponentsInChildren<Transform>(SpwanPointsList);
            SpwanPointsList.RemoveAt(0);
            
        }
        if(SpwanPointsList.Count > 0)
        {
            StartCoroutine(CreateEnemyPull());
        }
    }
    private void Awake() // Start,Awake,OnEnable �ٰ��� �������ڸ��� ȣ�� 
    {
        if (pullingManger == null)
        {
            pullingManger = this;
        }
        else if (pullingManger != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        bullet = Resources.Load("Bullet") as GameObject;
        E_bullet = Resources.Load("EnBullt") as GameObject;
        EnemyPrefad = Resources.Load("Enemy") as GameObject;
        CreateBulletPool();
        CrateBulletPoolEnemy();
        CreatEnemyPull();
    }

    private void CreatEnemyPull()
    {
        GameObject EnemyGroup = new GameObject("EnemyGroup");
        for (int i = 0; i < maxPool; i++)
        {
            var enemyObj = Instantiate(EnemyPrefad, EnemyGroup.transform);
            enemyObj.name = $"{(i + 1).ToString()} ��";
            enemyObj.SetActive(false);
            EnemyPoolList.Add(enemyObj);
        }
    }

    void CreateBulletPool()
    {
        // ���� ������Ʈ ����
        GameObject playerBulletGroup = new GameObject("PlayerBulletGroup");
        for( int i = 0; i< 10; i++ )
        {
            var _bullet = Instantiate(bullet,playerBulletGroup.transform);
            _bullet.name = $"{(i+1).ToString()}��";
            _bullet.SetActive(false);
            bulletPoolList.Add(_bullet);
        }
    }
    void CrateBulletPoolEnemy()
    {
        GameObject EnemyBulletGroup = new GameObject("EnemyGroup");
        for (int i = 0; i < 10; i++)
        {
            var _bullet2 = Instantiate(E_bullet, EnemyBulletGroup.transform);
            _bullet2.name = $"{(i + 1).ToString()}��";
            _bullet2.SetActive(false);
            E_bulletPoolList.Add(_bullet2);
        }
    }
  
    public GameObject CreatEnBulletPool()
    {
        for (int i = 0; i< E_bulletPoolList.Count; i ++ )
        {
            if ( E_bulletPoolList[i].activeSelf == false)
            {
                return E_bulletPoolList[i];
            }
        }
        return null;
    }
    public GameObject GetBulletPool()
    {
        for(int i = 0; i< bulletPoolList.Count; i++ )
        {
            // ��Ȱ�� �Ǿ��ٸ� activeSelf�� Ȱ��ȭ ��Ȱ�� ���θ� �˷���
            if (bulletPoolList[i].activeSelf==false)
            {
                return bulletPoolList[i];
            }
        }
        return null;
    }
    IEnumerator CreateEnemyPull()
    {
        while (!GameManger.Ginstance.isGameOver)
        {
            yield return new WaitForSeconds(3f);
            if (GameManger.Ginstance.isGameOver)
                yield break;
            // ������ ���ᰡ �Ǹ� �ڷ�ƾ�� ���� �ؼ� ���� ��ƾ�� ���� ���� �ʤ���

            foreach (GameObject _enemy in EnemyPoolList)
            {           // Ȱ��ȭ�ϴ��� ���ϴ��� �Ǻ��ϴ� ����
                if (_enemy.activeSelf == false)
                {
                    int idx = Random.Range(0,SpwanPointsList.Count);
                    _enemy.transform.position = SpwanPointsList[idx].position;
                    _enemy.transform.rotation = SpwanPointsList[idx].rotation;
                    _enemy.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
