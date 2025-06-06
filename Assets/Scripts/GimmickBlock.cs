using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; // �����������m����
    public bool isDelete = false; // ������ɍ폜���邩�ǂ����̃t���O
    public GameObject deadObj; // ���S�Ɋւ��铖���蔻��
    bool isFell = false; // �����ɂ��邩�ǂ����̃t���O
    float fadeTime = 0.5f; // ���ł���ۂ̃t�F�[�h�A�E�g����
    Rigidbody2D rbody; // Rigidbody2D�R���|�[�l���g���̎擾
    GameObject player; // Player���̎擾
    float distance; // �u���b�N�ƃv���C���[�̋���

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= length)
            {
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);
                }
            }
        }

        if (isFell)
        {
            // �����ɂ���
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color; // ������m�F
            col.a = fadeTime; // �����x��0�b�Ɍ������Ă���fadeTime�ƃ����N
            GetComponent<SpriteRenderer>().color = col; // ���H����Color�������ɖ߂�

            if (fadeTime <= 0.0f)
            {
                Destroy(gameObject); // �w�肵���I�u�W�F�N�g���q�G�����L�[�������
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // GimmickBlock�𒆐S�ɁAlength�̔��a�̉~��`��
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
