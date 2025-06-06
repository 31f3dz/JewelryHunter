using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float leftLimit; // �J�����̍��̈ړ��̌��E
    public float rightLimit; // �J�����̉E�̈ړ��̌��E
    public float topLimit; // �J�����̏�̈ړ��̌��E
    public float bottomLimit; // �J�����̉��̈ړ��̌��E

    public bool isForceScrollX; // X�����X�N���[���t���O
    public float forceScrollSpeedX = 0.5f; // �X�N���[���X�s�[�h

    public bool isForceScrollY; // Y�����X�N���[���t���O
    public float forceScrollSpeedY = 0.5f; // �X�N���[���X�s�[�h

    public GameObject subScreen; // �T�u�X�N���[��

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float currentX = player.transform.position.x; // �v���C���[��X���W���擾
            float currentY = player.transform.position.y; // �v���C���[��Y���W���擾

            // X�������X�N���[���Ȃ�΁AforceScrollSpeedX�̕����������ŉ��Z����Ă���
            if (isForceScrollX) currentX = transform.position.x + (forceScrollSpeedX * Time.deltaTime);

            // Y�������X�N���[���Ȃ�΁AforceScrollSpeedY�̕����������ŉ��Z����Ă���
            if (isForceScrollY) currentY = transform.position.y + (forceScrollSpeedY * Time.deltaTime);

            // ���~�b�g�Ŏ~�܂�
            if (currentX < leftLimit)
            {
                currentX = leftLimit;
            }
            else if (currentX > rightLimit)
            {
                currentX = rightLimit;
            }

            if (currentY < bottomLimit)
            {
                currentY = bottomLimit;
            }
            else if (currentY > topLimit)
            {
                currentY = topLimit;
            }

            // X��Y�̓v���C���[�Ɠ����AZ���͋�������ۂ�
            transform.position = new Vector3(currentX, currentY, transform.position.z);

            // �T�u�X�N���[���̓J�������݂������ŘA��������
            if (subScreen != null)
            {
                subScreen.transform.position = new Vector3(currentX / 2.0f, subScreen.transform.position.y, subScreen.transform.position.z);
            }
        }
    }
}
