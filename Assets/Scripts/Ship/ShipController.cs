using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShipController : MonoBehaviour
{
    // --------------------------------------- ENUMS -------------------------------------- //
    public enum eShip
    {
        DEFENSE,
        ATTACK,
        SUPPORT
    }

    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    // Dimensions
    [Header("Dimensions")]
    public float m_width = .1f;
    public float m_height = .1f;

    // Locomotion
    [Header("Locomotion")]
    [Header("Fly")]
    public float m_maxFlySpeed = 7;
    public float m_flyAcc = 1;
    
    public float m_flyAccDown = 1;
    public bool m_useMomentum = true;

    [Header("Change")]
    public AnimationCurve m_changeSpeed = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    public float m_changeDuration = 0.1f;
    public float m_changeCoolDownDuration = 1;

    //COMBAT
    [Header("Combat")]
    [Header("General")]
    public float AttackCooldown = 0.4f;
    
    [Header("Weapon - Shot")]
    public Vector2 ShotOffset;
    public Vector2 ShotHitbox;

    // --------------------------------- DEBUG IN EDITOR --------------------------------- //
    [Header("Debug")]
    public bool m_useDebugMode = false;

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // SHIP
    private ShipBHV ship;

    // LOCOMOTION: WALK
    private Vector2 m_flySpeed = new Vector2(0, 0);

    // LOCOMOTION: CHANGE
    private float m_changeTimer         = 0;
    private bool  m_changeFoward        = true;
    private bool  m_changeBackward      = false;
    private float m_changeCooldownTimer = 0;
    private eShip m_ship                = eShip.ATTACK;

    // COMBAT
    private bool isAttacking = false;
    private bool isChanging = false;

    // GENERAL
    private int m_nbLives = 1;

    private float m_collisionEpsilon;

    // -------------------------------- PUBLIC METHODS ------------------------------- //
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<ShipBHV>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0, vertical = 0;
        bool doAttack = false, doChange = false;

        if (m_nbLives > 0)
        {

            // get input
            horizontal       = InputMgr.GetAxis(1, InputMgr.eAxis.HORIZONTAL);    
            vertical         = InputMgr.GetAxis(1, InputMgr.eAxis.VERTICAL);      
            doAttack         = InputMgr.GetButton(1, InputMgr.eButton.ATTACK);        
            doChange         = InputMgr.GetButton(1, InputMgr.eButton.CHANGEB) || InputMgr.GetButton(1, InputMgr.eButton.CHANGEF);
            m_changeBackward = InputMgr.GetButton(1, InputMgr.eButton.CHANGEB);
            m_changeFoward   = InputMgr.GetButton(1, InputMgr.eButton.CHANGEF);

            // get attack input
            if (InputMgr.GetButton(1, InputMgr.eButton.ATTACK) && !isAttacking)
            {
                isAttacking = true;
                print("pew pew");
                ship.Fire1();
                isAttacking = false;
            }

            // change ships
            if (m_changeFoward && m_changeTimer==0)
            {
                m_changeCooldownTimer = m_changeCoolDownDuration;
                m_changeTimer = m_changeCoolDownDuration;

                if (m_ship == eShip.SUPPORT)
                    m_ship = eShip.DEFENSE;
                else
                    m_ship++;

                StartCoroutine(ChangeCooldown());
            }    
            if (m_changeBackward && m_changeTimer==0)
            {
                m_changeCooldownTimer = m_changeCoolDownDuration;
                m_changeTimer = m_changeCoolDownDuration;

                if (m_ship == eShip.DEFENSE)
                    m_ship = eShip.SUPPORT;
                else
                    m_ship--;

                StartCoroutine(ChangeCooldown());
            }
                
        }

        // update position
        UpdateTransform(horizontal, vertical);
    }

    // ======================================================================================
    private void UpdateTransform(float _inputHorizontal, float _inputVertical)
    {
        // update transform
        Vector3 initialPos = this.transform.position;

        bool isWallSnapped = CheckWalls();

        bool isFlying = UpdateFlight(_inputHorizontal, _inputVertical);

        UpdateCollisions(initialPos, this.transform.position);

        Vector3 finalPos = this.transform.position;
        Vector3 deltaPos = finalPos - initialPos;
    }

    // ======================================================================================
    private bool UpdateCollisions(Vector3 _startPos, Vector3 _endPos)
    {
        Vector3 finalPos = CheckCollision(_startPos, _endPos);

        if (_startPos.y == finalPos.y)
        {
            //m_gravSpeed = 0;
        }

        this.transform.position = finalPos;

        return true;
    }

    // ======================================================================================
    private Vector3 CheckCollision(Vector3 _startPos, Vector3 _endPos)
    {
        RaycastHit hitInfo;
        Vector3 direction = _endPos - _startPos;
        Vector3 finalEndPos = _endPos;

        finalEndPos.x = Mathf.Clamp(finalEndPos.x, SceneMgr.MinX + m_width / 2, SceneMgr.MaxX - m_width / 2);
        finalEndPos.y = Mathf.Clamp(finalEndPos.y, SceneMgr.MinY, SceneMgr.MaxY - m_height);
        return finalEndPos;
    }

    // ======================================================================================
    private bool CheckWalls()
    {
        Vector3 lWall = this.transform.position - (m_collisionEpsilon + m_width / 2) * Vector3.right;
        Vector3 rWall = this.transform.position + (m_collisionEpsilon + m_width / 2) * Vector3.right;

        if (lWall.x <= SceneMgr.MinX || rWall.x >= SceneMgr.MaxX)
            return true;

        return Physics.Raycast(this.transform.position, -(m_collisionEpsilon - m_width / 2) * Vector3.right) || Physics.Raycast(this.transform.position, -(m_collisionEpsilon - m_width / 2) * Vector3.right + (m_collisionEpsilon + m_width / 2) * Vector3.right);
    }

    // ======================================================================================
    private bool UpdateFlight(float _inputHorizontal, float _inputVertical)
    {
        bool animate = false;

        // fly
        Vector2 nextSpeed;

        if (!m_useMomentum)
        {
            nextSpeed.x = Mathf.Lerp(m_flySpeed.x, m_maxFlySpeed * _inputHorizontal, GameMgr.DeltaTime * m_flyAcc);
            nextSpeed.y = Mathf.Lerp(m_flySpeed.y, m_maxFlySpeed * _inputVertical, GameMgr.DeltaTime * m_flyAcc);
        }
        else
        {
            nextSpeed = m_flySpeed;
            if (_inputHorizontal != 0)
                nextSpeed.x += _inputHorizontal * m_flyAcc * GameMgr.DeltaTime;
            else
                nextSpeed.x = Mathf.Lerp(nextSpeed.x, 0, m_flyAccDown * GameMgr.DeltaTime);

            if (_inputVertical != 0)
                nextSpeed.y += _inputVertical * m_flyAcc * GameMgr.DeltaTime;
            else
                nextSpeed.y = Mathf.Lerp(nextSpeed.y, 0, m_flyAccDown * GameMgr.DeltaTime);

            nextSpeed.x = nextSpeed.x >= 0 ? Mathf.Clamp(nextSpeed.x, 0, m_maxFlySpeed) : Mathf.Clamp(nextSpeed.x, -m_maxFlySpeed, 0);
            nextSpeed.y = nextSpeed.y >= 0 ? Mathf.Clamp(nextSpeed.y, 0, m_maxFlySpeed) : Mathf.Clamp(nextSpeed.y, -m_maxFlySpeed, 0);
        }

        this.transform.position += Vector3.right * GameMgr.DeltaTime * nextSpeed.x + Vector3.up* GameMgr.DeltaTime * nextSpeed.y;

        // Flying Anim State
        float nextSpeedMagX = Mathf.Abs(nextSpeed.x);
        float prevSpeedMagX = Mathf.Abs(m_flySpeed.x);
        float nextSpeedMagY = Mathf.Abs(nextSpeed.y);
        float prevSpeedMagY = Mathf.Abs(m_flySpeed.y);

        bool isStartingFlight = nextSpeedMagX > prevSpeedMagX && nextSpeedMagY > prevSpeedMagY;

        m_flySpeed = nextSpeed;

        if (isStartingFlight)
        {
            animate = true;
        }

        return animate;
    }

    // ======================================================================================
    private IEnumerator ChangeCooldown ()
    {
        yield return new WaitForSeconds(m_changeCooldownTimer);
        m_changeTimer = 0;
        m_changeCooldownTimer = 0;
    }
}
