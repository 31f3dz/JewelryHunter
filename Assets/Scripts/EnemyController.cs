using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f; // �ړ��X�s�[�h
    public bool isToRight; // true = �E�����Afalse = ������
    int groundContactCount; // �n�ʂɂǂ̂��炢�ڐG������

    public LayerMask groundLayer; // �n�ʔ���̑Ώۃ��C���[

    // Start is called before the first frame update
    void Start()
    {
        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool onGround = Physics2D.CircleCast(
            transform.position, // �Z���T�[�̔����ʒu
            0.5f, // �~�̔��a
            Vector2.down, // �ǂ��̕��p�Ɍ����邩
            0, // �Z���T�[���΂�����
            groundLayer // �����Ώۂ̃��C���[
        );

        if (onGround)
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();

            if (isToRight)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }
        }
    }

    // �����E���̐؂�ւ����\�b�h
    void Turn()
    {
        isToRight = !isToRight;

        if (isToRight) transform.localScale = new Vector2(-1, 1);
        else transform.localScale = new Vector2(1, 1);
    }

    // Ground�^�O�ȊO�̂��̂ƂԂ������甽�]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            Turn(); // �����ƂԂ������甽�]
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount++; // �V�����n�ʂƐڐG������1�J�E���g�v���X
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount--; // �����̒n�ʂ���E�o������1�J�E���g�}�C�i�X

            if (groundContactCount <= 0) // �}�C�i�X�������ʁA�V���ɐڐG����n�ʂ��Ȃ��Ƃ��̓J�E���g��0���R�ۂɂ���
            {
                groundContactCount = 0; // �O�̂��߃J�E���g�𖾊m��0�ɖ߂�
                Turn(); // �R�ۂ��Ǝv����̂Ŕ��]
            }
        }
    }
}
