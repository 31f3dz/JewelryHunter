using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // ���E�̃L�[�̒l���i�[
    Rigidbody2D rbody; // Rigidbody2D�̏����������߂̔}��
    Animator animator; // Animator�̏����������߂̔}��
    public float speed = 3.0f; // �����X�s�[�h
    bool isJump; // �W�����v�����ǂ���
    bool onGround; // �n�ʔ���
    public LayerMask groundLayer; // �n�ʔ���̑Ώۂ̃��C���[�����������߂Ă���
    public float jump = 9.0f; // �W�����v��

    // Start is called before the first frame update
    void Start()
    {
        // Player�ɂ��Ă���Rigidbody2D�R���|�[�l���g��ϐ�rbody�ɏh�����B�Ȍ�ARigidbody2D�R���|�[�l���g�̋@�\��rbody�Ƃ����ϐ���ʂ��ăv���O���������犈�p�ł���
        rbody = GetComponent<Rigidbody2D>();

        // Player�ɂ��Ă���Animator�R���|�[�l���g��ϐ�animator�ɏh����
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != "playing")
        {
            return; // Update�̏����������I��
        }

        // ���E�̃L�[�������ꂽ�ꍇ�A�ǂ���̒l�������̂���axisH�Ɋi�[
        // ����Horizontal�̏ꍇ�F���������̃L�[�����������ꂽ�ꍇ�A���Ȃ�-1�A�E�Ȃ�1�A����������Ă��Ȃ��̂ł���Ώ��0��Ԃ����\�b�h
        axisH = Input.GetAxisRaw("Horizontal");

        // ����axisH�����̐��Ȃ�E����
        if (axisH > 0)
        {
            // this.gameObject.GetComponent<Transform>().localScale��MonoBehaviour���ȗ�
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("run", true); // �S�����Ă���R���g���[���[�̃p�����[�^��ς���
        }
        // �łȂ���΁A����axisH�����̐��Ȃ獶����
        else if (axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("run", true); // �S�����Ă���R���g���[���[�̃p�����[�^��ς���
        }
        else
        {
            animator.SetBool("run", false); // �S�����Ă���R���g���[���[�̃p�����[�^��ς���
        }

        // �����W�����v�{�^���������ꂽ��
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (GameController.gameState != "playing")
        {
            return; // Update�̏����������I��
        }

        // �n�ʂɂ��邩�ǂ������T�[�N���L���X�g���g���Ĕ���
        onGround = Physics2D.CircleCast(
            transform.position, // Player�̊�_
            0.2f, // ���a
            Vector2.down, // �w�肵���_����ǂ̕����Ƀ`�F�b�N��L�΂��� new Vector2(0, -1)
            0.0f, // �w�肵���_����ǂ̂��炢�`�F�b�N�̋�����L�΂���
            groundLayer // �w�肵�����C���[
        );

        // velocity��2���̕����f�[�^�iVector2�j����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        // �W�����v���t���O����������
        if (isJump)
        {
            // Rigidbody2D��AddForce���\�b�h�ɂ���ď�ɉ����o��
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            isJump = false;
        }
    }

    public void Jump()
    {
        // �n�ʔ��肪false�Ȃ�W�����v�t���O�͗��ĂȂ�
        if (onGround)
        {
            isJump = true;
            animator.SetTrigger("jump"); // �W�����v�A�j���̂��߂̃g���K�[����
        }
    }

    // �����ƂԂ������甭�����郁�\�b�h
    // �Ԃ����������Collider��������collision�ɓ����
    // �����Collider�����Ă��Ȃ��ƈӖ����Ȃ�
    // �������Collider��isTrigger�ł��邱��
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }

        if (collision.gameObject.tag == "Item") // Item�ɐG�ꂽ��
        {
            ItemData itemdata = collision.gameObject.GetComponent<ItemData>(); // �Ԃ�����Item�̃X�N���v�g���擾
            GameController.stageScore += itemdata.value; // �Ԃ�����Item�̃X�N���v�g�ɋL����Ă���value�̒l��stageScore�ɉ��Z
            Destroy(collision.gameObject); // ����̖{�̂�����
        }
    }

    public void Goal()
    {
        GameController.gameState = "gameclear";
        animator.SetBool("gameClear", true); // PlayerClear�A�j����ON
        PlayerStop(); // �������~�߂�
    }

    public void GameOver()
    {
        GameController.gameState = "gameover";
        animator.SetBool("gameOver", true); // PlayerOver�A�j����ON
        PlayerStop(); // �������~�߂�

        // �v���C���[����ɒ��ˏグ��
        rbody.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
        // �����蔻����J�b�g
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // �v���C���[�̓������~
    public void PlayerStop()
    {
        // ���x��0�ɂ��Ď~�߂�
        rbody.velocity = new Vector2(0, 0);
    }
}
