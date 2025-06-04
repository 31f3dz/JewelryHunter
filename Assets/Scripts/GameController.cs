using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // �Q�[���̏�ԊǗ��� ���ÓI�ϐ�

    public GameObject stageTitle; // �X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite; // �Q�[���N���A�̊G
    public Sprite gameOverSprite; // �Q�[���I�[�o�[�̊G

    public GameObject buttonPanel; // �{�^���p�l����UI�I�u�W�F�N�g
    public GameObject restartButton; // ���X�^�[�g�{�^��
    public GameObject nextButton; // �l�N�X�g�{�^��

    TimeController timeCnt; // TimeController�X�N���v�g���擾����

    public TextMeshProUGUI timeText; // TimeText�I�u�W�F�N�g�������Ă���TextMeshPro�R���|�[�l���g

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n�Ɠ����ɃQ�[���X�e�[�^�X��"playing"
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); // �������Ɏw�肵�����\�b�h(��)���A�������b��ɔ���
        buttonPanel.SetActive(false); // �I�u�W�F�N�g���\��

        // TimeController�R���|�[�l���g�̏����擾
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "playing")
        {
            // �J�E���g�_�E���̕ϐ���UI�ɘA��
            // timeCnt��currentTime(float�^)���܂�int�Ɍ^�ϊ����Ă���AToString()�ŕ�����ɕϊ����AtimeText(TextMeshPro)��text���ɑ��
            timeText.text = ((int)timeCnt.currentTime).ToString();

            // ����currentTime��0�ɂȂ�����Q�[���I�[�o�[
            if (timeCnt.currentTime <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player"); // �v���C���[��T��
                // �T���o�����v���C���[�������Ă���PlayerController�R���|�[�l���g��GameOver���\�b�h�𔭓�
                player.GetComponent<PlayerController>().GameOver();
            }
        }
        // �Q�[���̏�Ԃ��N���A�܂��̓I�[�o�[�̎��A�{�^���𕜊���������
        else if (gameState == "gameclear" || gameState == "gameover")
        {
            // �X�e�[�W�^�C�g���̕���
            stageTitle.SetActive(true);

            // �{�^���̕���
            buttonPanel.SetActive(true);

            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;

                // restartButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false �� �{�^���@�\���~
                restartButton.GetComponent<Button>().interactable = false;
            }
            else if (gameState == "gameover")
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;

                // nextButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false �� �{�^���@�\���~
                nextButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    //�X�e�[�W�^�C�g�����\���ɂ��郁�\�b�h
    void InactiveImage()
    {
        stageTitle.SetActive(false); // �I�u�W�F�N�g���\��
    }
}
