using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //Unity�������ړ�������R���|�[�l���g������
    private Rigidbody myRigidbody;
    //�O�����̑��x
    private float velocityZ = 16f;//�菇�ɕK�v�Ȃ��́i�p�����[�^�j
    //�������̑��x
    private float velocityX = 10f;
    //������̑��x
    private float velocityY = 10f;
    //���E�̈ړ��ł���͈�
    private float movableRange = 3.4f;
    //����������������W��
    private float coefficient = 0.99f;
    //�Q�[���I���̔���
    private bool isEnd = false;
    //�Q�[���I�����ɕ\������e�L�X�g
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g
    private GameObject scoreText;
    //���_
    private int score = 0;
    //���{�^�������̔���
    private bool isLButtonDown = false;
    //�E�{�^�������̔���
    private bool isRButtonDown = false;
    //�W�����v�{�^�������̔���
    private bool isJButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n ???
        //Animator�N���X�́uSetFloat�v�֐�*�́A�������ɗ^����ꂽ�p�����[�^�ɑ������̒l��������֐��B�܂��������̃p�����[�^���A�j���Đ��̏����Ɏg����
        this.myAnimator.SetFloat("Speed", 1);//Lesson�W�ŉ���B

        //Rigidvidy�R���|�[�l���g���v�擾
        this.myRigidbody = GetComponent<Rigidbody>();

        //�V�[������stateText�I�u�W�F�N�g���擾
        this.stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {

        //�Q�[���I���Ȃ�Unity�����̓�������������
        if (this.isEnd) 
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //Unity�����ɑ��x��^����i�ǉ��j
        //this.myRigidbody.velocity = new Vector3(0,0,this.velocityZ);

        //�������̓��͂ɂ�鑬�x�i�ǉ��j???�@Update�֐��̒��ɓ���闝�R��?�@���L�uif�v�uelse if�v�ȊO�A�����Ȃ��Ƃ��̐ݒ�
        float inputVelocityX = 0;
        //������̓��͂ɂ�鑬�x�i�ǉ��j
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������i�ǉ��j
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //�������ւ̑��x�����i�ǉ��j
            inputVelocityX = -this.velocityX;
        }

        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //�E�����ւ̑��x�����i�ǉ��j
            inputVelocityX = this.velocityX;

        }

        //�W�����v���Ă��Ȃ����ɃX�y�[�X�������ꂽ��W�����v����i�ǉ��j
        if ((Input.GetKey(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)//???0.5�̒l��?
        {
            //�W�����v�A�j�����Đ��i�ǉ��j
            this.myAnimator.SetBool("Jump", true);//�A�j���̃W�����v�̃`�F�b�N������
            //������ւ̑��x�����i�ǉ��j
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂�Y���̑��x�����i�ǉ��j
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����i�ǉ��j???
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))//�u�O�v�̓A�j���[�^�[��Base���C�����Ƃ�Ƃ����Ӗ�
        {
            this.myAnimator.SetBool("Jump", false);//�A�j���̃W�����v�̃`�F�b�N���O��
        }


        //Unity�����ɑ��x��^����i�ύX�j
        //this.myRigidbody.velocity = new Vector3(inputVelocityX, 0, velocityZ);

        //Unity�����ɑ��x��^����i�ĕύX�j
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);

    }

    private void OnTriggerEnter(Collider other)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") 
        {
            this.isEnd = true;

            //stateText��GAME OVER��\��
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //�S�[���n�_�ɓ��B���ꍇ�i�ǉ��j
        if (other.gameObject.tag == "GoalTag") 
        {
            this.isEnd = true;

            //stateText��CLEAR��\��
            this.stateText.GetComponent<Text>().text = "CLEAR!";

        }

        //�R�C���ɏՓ˂����ꍇ
        if (other.gameObject.tag == "CoinTag") 
        {
            //�X�R�A�����Z
            this.score += 10;

            //scoreText�Ɋl�������_����\��
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //�p�[�e�B�N�����Đ�
            GetComponent<ParticleSystem>().Play();

            //�ڐG���R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
    }

    //�W�����v�{�^�����������ꍇ�̏���
    public void GetMyJumpButtonDown() 
    {
        this.isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̏���
    public void GetMyJumpButtonUp() 
    {
        this.isJButtonDown = false;
    }

    //���{�^���������������ꍇ�̏���
    public void GetMyLeftButtonDown() 
    {
        this.isLButtonDown = true;
    }

    //���{�^���𗣂����ꍇ�̏���
    public void GetMyLeftButtonUp() 
    {
        this.isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏���
    public void GetMyRightButtonDown() 
    {
        this.isRButtonDown = true;
    }

    //�E�{�^���𗣂����ꍇ�̏���
    public void GetMyRightButtonUp() 
    {
        this.isRButtonDown = false;
    }

}