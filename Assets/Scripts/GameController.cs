using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // �Q�[���̏�ԊǗ��� ���ÓI�ϐ�

    public GameObject stageTitle; // �X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite; // �Q�[���N���A�̊G
    public Sprite gameOverSprite; // �Q�[���I�[�o�[�̊G

    public GameObject buttonPanel; // �{�^���p�l����UI�I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n�Ɠ����ɃQ�[���X�e�[�^�X��"playing"
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); // �������Ɏw�肵�����\�b�h(��)���A�������b��ɔ���
        buttonPanel.SetActive(false); // �I�u�W�F�N�g���\��
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���̏�Ԃ��N���A�܂��̓I�[�o�[�̎��A�{�^���𕜊���������
        if (gameState == "gameclear" || gameState == "gameover")
        {
            // �X�e�[�W�^�C�g���̕���
            stageTitle.SetActive(true);

            // �{�^���̕���
            buttonPanel.SetActive(true);

            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
            }
            else if (gameState == "gameover")
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
            }
        }
    }

    //�X�e�[�W�^�C�g�����\���ɂ��郁�\�b�h
    void InactiveImage()
    {
        stageTitle.SetActive(false); // �I�u�W�F�N�g���\��
    }
}
