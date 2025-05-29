using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // ���E�̃L�[�̒l���i�[
    Rigidbody2D rbody; // Rigidbody2D�̏����������߂̔}��
    public float speed = 3.0f; // �����X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        // Player�ɂ��Ă���Rigidbody2D�R���|�[�l���g��ϐ�rbody�ɏh�����B�Ȍ�ARigidbody2D�R���|�[�l���g�̋@�\��rbody�Ƃ����ϐ���ʂ��ăv���O���������犈�p�ł���
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���E�̃L�[�������ꂽ�ꍇ�A�ǂ���̒l�������̂���axisH�Ɋi�[
        // ����Horizontal�̏ꍇ�F���������̃L�[�����������ꂽ�ꍇ�A���Ȃ�-1�A�E�Ȃ�1�A����������Ă��Ȃ��̂ł���Ώ��0��Ԃ����\�b�h
        axisH = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // velocity��2���̕����f�[�^�iVector2�j����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}
