using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; // �폜�����܂ł̎��Ԑݒ�
    public bool isDelete; // �Ԃ������������e�ɂ��Ă������ǂ����̃t���O

    // Start is called before the first frame update
    void Start()
    {
        // ���ԍ��ŏ���
        Destroy(gameObject, deleteTime); // deleteTime������ɏ���
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �t���O�������Ă���Ώ�����
        if (isDelete)
        {
            Destroy(gameObject);
        }
    }
}
