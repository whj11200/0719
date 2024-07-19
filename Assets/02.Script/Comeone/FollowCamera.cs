using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform taget; // ���� �ٴ� ���
    [SerializeField]
    private float Height = 5.0f; // ī�޶� ����
    [SerializeField]
    private float distance = 7.0f; //Ÿ��
    [SerializeField]
    private float movedamping = 10f; // ī�޶� �̵� ȸ��
    [SerializeField]
    private float rotdamping = 15f; //  ī�޶� ȸ�� �̵�
    // ī�޶� �̵� ȸ���� ������ �ε巴�� ��ȭ �ϴ� ��
    [SerializeField]
    private Transform CamTr;// �ڽ� ��ġ
    [SerializeField]
    private float tagetOffset = 2.0f; // Ÿ�� ī�޶� ���� ��
    void Start()
    {
        CamTr = transform;
        taget = GameObject.FindWithTag("Player").transform;
    }

 
    // Update -> ���� �� ���� -> LateUpdate �� ���󰣴�.
    // FixedUpdate ��Ȯ�� �ð� Ÿ�ְ̹� ���� �������� �����ϴ� �Լ�
    // FIxedUpdate => Update => LateUpdate 
    void LateUpdate()
    {
        if (taget == null)
            return;// -�� �� ������ ī�޶� �÷��̾� �ڿ� �ֱ⶧���̴�
                   // Ÿ�� �����ǿ��� dlstance��ŭ �ڿ� ��ġ + Height ���� ��ŭ ���� ��ġ
        var Campos = taget.position - (taget.forward * distance)+ (taget.up * Height);
        CamTr.position = Vector3.Slerp(CamTr.position, Campos , Time.deltaTime * movedamping);
                //��� ���� (�ڱ��ڽ� ��ġ���� , Campos ���� , damping �ð� ��ŭ �ε巴�� �����δ�.
        CamTr.rotation = Quaternion.Slerp(CamTr.rotation,taget.rotation, Time.deltaTime * movedamping);
        // ȸ�������� CamTr��ġ���� Ÿ�� ȸ����
        CamTr.LookAt(taget.position +(taget.up * tagetOffset));
                   // Ÿ�� �����ǿ��� 2��ŭ ���� �ø�

    }
    // ��ȭ�鿡�� �����̳� ���� �׷��ִ� �Լ� �ݺ��Լ�
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(taget.position + (taget.up * tagetOffset), 0.1f);
    //                       // Ÿ�� ��ġ �� Ÿ���� ���� ��ġ�� �� Radius
    //   Gizmos.DrawLine(taget.position +(taget.up * tagetOffset),CamTr.position);
    //                 // Ÿ�� ��ġ��  �� ��ġ ����� Ÿ���� ���Ͱ� �׸��� ī�޶� ��ġ
    //}
}
