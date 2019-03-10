using UnityEngine;
#if !UNITY_WEBGL
using XInputDotNetPure; // Required in C#
#endif

public class InputMgr : MonoBehaviour
{

    public PlayerSO gambiarra; //FIX ME
    // --------------------------------------- ENUMS ------------------------------------- //
    public enum eAxis
    {
        HORIZONTAL,
        VERTICAL
    }

    public enum eButton
    {
        SPECIAL,
        CANCEL,
        ATTACK,
        CHANGEF,
        CHANGEB,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        PAUSE
    }

    public enum eXBoxButton
    {
        A,
        B,
        X,
        Y,
        DPAD_LEFT,
        DPAD_RIGHT,
        DPAD_UP,
        DPAD_DOWN,
        START,
        OPTIONS,
        BUMPR_LEFT,
        TRIGG_LEFT,
        STICK_LEFT,
        BUMPR_RIGHT,
        TRIGG_RIGHT,
        STICK_RIGHT,
    }
    
    // ---------------------------------- PUBLIC ATTRIBUTES ------------------------------ //
 
    public eXBoxButton m_changeButtonF;
    public eXBoxButton m_changeButtonB;

    public eXBoxButton m_shootButton;

    public eXBoxButton m_specialButton;
    public eXBoxButton m_cancelButton;
    public eXBoxButton m_pauseButton;

    public eXBoxButton m_leftButton;
    public eXBoxButton m_rightButton;

    public float m_triggMinRatio = .3f;

    [Header("Debug")]
    public bool m_debugMode;

    //public string m_shoot   = "Space";
    //public string m_changeF = "X";
    //public string m_changeB = "C";
    //public string m_cancel  = "M";
    //public string m_special = "Z";
    //public string m_pause   = "P";


    public KeyCode m_shootKey = KeyCode.Space;
    public KeyCode m_changeFKey = KeyCode.E;
    public KeyCode m_changeBKey = KeyCode.Q;
    public KeyCode m_cancelKey = KeyCode.C;
    public KeyCode m_specialKey = KeyCode.LeftShift;
    public KeyCode m_pauseKey = KeyCode.P;

    public static KeyCode m_shoot = KeyCode.Space;
    public static KeyCode m_changeF = KeyCode.E;
    public static KeyCode m_changeB = KeyCode.Q;
    public static KeyCode m_cancel = KeyCode.C;
    public static KeyCode m_special = KeyCode.LeftShift;
    public static KeyCode m_pause = KeyCode.P;

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //

    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private static InputMgr m_manager = null;
    private static int      Count;
    private ButtonState    state;
    private ButtonState    old_state;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - InputMgr : input manager must be unique!");
        m_manager = this;
        Count = 0;
        m_shoot = m_shootKey;
        m_changeF = m_changeFKey;
        m_changeB = m_changeBKey;
        m_cancel = m_cancelKey;
        m_special = m_specialKey;
        m_pause = m_pauseKey;
        m_manager.state = ButtonState.Released;
        m_manager.old_state = ButtonState.Released;
        if (!GamePad.GetState(PlayerIndex.One).IsConnected)
            m_manager.m_debugMode = true;
        else
            m_manager.m_debugMode = false;
    }
    
    // ======================================================================================
    // TODO: Explain: what is the '_player' parameter?
    // Player is the paired controller. If _player > 1, it means that there is more than one controller connected
    public static bool GetButton(int _player, eButton _button)
    {
        if (_player > 1 || _player <= 0)
            return false;


        if (_player == 1 && m_manager.m_debugMode)
        {
            return GetDebugButton(_button);
        }

#if UNITY_WEBGL
        return GetDebugButton(_button);
#else
        GamePadState gamePadState = GamePad.GetState( (PlayerIndex) (_player - 1) );

        switch (_button)
        {
            case eButton.ATTACK:
                return GetButton(gamePadState, m_manager.m_shootButton);
            case eButton.CHANGEF:
                return GetButton(gamePadState, m_manager.m_changeButtonF);
            case eButton.CHANGEB:
                return GetButton(gamePadState, m_manager.m_changeButtonB);
            case eButton.SPECIAL:
                return GetButton(gamePadState, m_manager.m_specialButton);
            case eButton.CANCEL:
                return GetButton(gamePadState, m_manager.m_cancelButton);
            case eButton.PAUSE:
                return GetButton(gamePadState, m_manager.m_pauseButton);
            case eButton.LEFT:
                return GetButton(gamePadState, m_manager.m_leftButton);
            case eButton.RIGHT:
                return GetButton(gamePadState, m_manager.m_rightButton);
        }

        return false;
#endif
    }

    public static bool GetKeyDown(int _player, eButton _button)
    {
        if (_player > 1 || _player <= 0)
            return false;


        if (_player == 1 && m_manager.m_debugMode)
        {
            return GetDebugKeyDown(_button);
        }

#if UNITY_WEBGL
        return GetDebugKeyDown(_button);
#else
        GamePadState gamePadState = GamePad.GetState((PlayerIndex)(_player - 1));

        //GAMBIARRA EXTREMA ARRUMAR DEPOIS URGENTEMENTE
        if(!GetButton(gamePadState, m_manager.m_shootButton))
        {
            if(!GetButton(gamePadState, m_manager.m_changeButtonF))
            {
                if (!GetButton(gamePadState, m_manager.m_changeButtonB))
                {
                    if (!GetButton(gamePadState, m_manager.m_specialButton))
                    {
                        if (!GetButton(gamePadState, m_manager.m_cancelButton))
                        {
                            if (!GetButton(gamePadState, m_manager.m_pauseButton))
                            {
                                if (!GetButton(gamePadState, m_manager.m_leftButton))
                                {
                                    if (!GetButton(gamePadState, m_manager.m_rightButton))
                                    {
                                        m_manager.state = ButtonState.Released;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (m_manager.state == ButtonState.Released)
            m_manager.old_state = ButtonState.Released;
        
        switch (_button)
        {
            case eButton.ATTACK:
                if (GetButton(gamePadState, m_manager.m_shootButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.CHANGEF:
                if (GetButton(gamePadState, m_manager.m_changeButtonF))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.CHANGEB:
                if (GetButton(gamePadState, m_manager.m_changeButtonB))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.SPECIAL:
                if (GetButton(gamePadState, m_manager.m_specialButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.CANCEL:
                if (GetButton(gamePadState, m_manager.m_cancelButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.PAUSE:
                if (GetButton(gamePadState, m_manager.m_pauseButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.LEFT:
                if (GetButton(gamePadState, m_manager.m_leftButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            case eButton.RIGHT:
                if (GetButton(gamePadState, m_manager.m_rightButton))
                {
                    m_manager.state = ButtonState.Pressed;
                    if (m_manager.old_state == ButtonState.Released)
                    {
                        m_manager.old_state = m_manager.state;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
        }
        
        return false;
#endif
    }

    // ======================================================================================
    public static float GetAxis(int _player, eAxis _axis)
    {
        if (_player > 1 || _player <= 0)
            return 0f;

        if (_player == 1 && m_manager.m_debugMode)
        {
            return GetDebugAxis(_axis);
        }

#if UNITY_WEBGL
        return GetDebugAxis(_axis);
#else

        GamePadState gamePadState = GamePad.GetState( (PlayerIndex) (_player - 1) );

        switch (_axis)
        {

            case eAxis.HORIZONTAL:
                return gamePadState.ThumbSticks.Left.X;
            case eAxis.VERTICAL:
                return gamePadState.ThumbSticks.Left.Y;
        }

        return 0f;
#endif
    }


    // ======================================================================================
    // PRIVATE METHODS
    // ======================================================================================
#if !UNITY_WEBGL
    private static bool GetButton(GamePadState _gamePadState, eXBoxButton _xboxButton)
    {
        switch (_xboxButton)
        {
            // TRIGGERS AS BUTTONS
            case eXBoxButton.TRIGG_LEFT:
                return _gamePadState.Triggers.Left > m_manager.m_triggMinRatio;
            case eXBoxButton.TRIGG_RIGHT:
                return _gamePadState.Triggers.Right > m_manager.m_triggMinRatio;

            // BUTTONS
            case eXBoxButton.A:
                return _gamePadState.Buttons.A == ButtonState.Pressed;
            case eXBoxButton.B:
                return _gamePadState.Buttons.B == ButtonState.Pressed;
            case eXBoxButton.X:
                return _gamePadState.Buttons.X == ButtonState.Pressed;
            case eXBoxButton.Y:
                return _gamePadState.Buttons.Y == ButtonState.Pressed;
            case eXBoxButton.BUMPR_LEFT:
                return _gamePadState.Buttons.LeftShoulder == ButtonState.Pressed;
            case eXBoxButton.BUMPR_RIGHT:
                return _gamePadState.Buttons.RightShoulder == ButtonState.Pressed;
            case eXBoxButton.STICK_LEFT:
                return _gamePadState.Buttons.LeftStick == ButtonState.Pressed;
            case eXBoxButton.STICK_RIGHT:
                return _gamePadState.Buttons.RightStick == ButtonState.Pressed;
            case eXBoxButton.START:
                return _gamePadState.Buttons.Start == ButtonState.Pressed;
            case eXBoxButton.OPTIONS:
                return _gamePadState.Buttons.Guide == ButtonState.Pressed;
            case eXBoxButton.DPAD_UP:
                return _gamePadState.DPad.Up == ButtonState.Pressed;
            case eXBoxButton.DPAD_DOWN:
                return _gamePadState.DPad.Down == ButtonState.Pressed;
            case eXBoxButton.DPAD_LEFT:
                return _gamePadState.DPad.Left == ButtonState.Pressed;
            case eXBoxButton.DPAD_RIGHT:
                return _gamePadState.DPad.Right == ButtonState.Pressed;
        }

        return false;
    }
#endif

    //// ======================================================================================

    public static bool GetDebugButton(eButton _button)
    {
        switch (_button)
        {
            case eButton.ATTACK:
                return Input.GetKey(m_shoot);
            case eButton.SPECIAL:
                return Input.GetKey(m_special) || Input.GetKey(KeyCode.LeftShift);
            case eButton.CHANGEB:
                return Input.GetKey(m_changeB);
            case eButton.CHANGEF:
                return Input.GetKey(m_changeF);
            case eButton.PAUSE:
                return Input.GetKey(m_pause);
            case eButton.CANCEL:
                return Input.GetKey(m_cancel);
            case eButton.LEFT:
                return Input.GetKey(KeyCode.A);
            case eButton.RIGHT:
                return Input.GetKey(KeyCode.D);
            case eButton.UP:
                return Input.GetKey(KeyCode.W);
            case eButton.DOWN:
                return Input.GetKey(KeyCode.S);
        }

        return false;
    }

    public static bool GetDebugKeyDown(eButton _button)
    {
        switch (_button)
        {
            case eButton.ATTACK:
                return Input.GetKeyDown(m_shoot);
            case eButton.SPECIAL:
                return Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(m_special);
            case eButton.CHANGEB:
                return Input.GetKeyDown(m_changeB);
            case eButton.CHANGEF:
                return Input.GetKeyDown(m_changeF);
            case eButton.PAUSE:
                return Input.GetKeyDown(m_pause);
            case eButton.CANCEL:
                return Input.GetKeyDown(m_cancel);
            case eButton.LEFT:
                return Input.GetKeyDown(KeyCode.A);
            case eButton.RIGHT:
                return Input.GetKeyDown(KeyCode.D);
            case eButton.UP:
                return Input.GetKeyDown(KeyCode.W);
            case eButton.DOWN:
                return Input.GetKeyDown(KeyCode.S);
        }

        return false;
    }

    public static float GetDebugAxis(eAxis _axis)
    {
        switch (_axis)
        {
            case eAxis.VERTICAL:
                return Input.GetAxisRaw("Vertical");
                //return (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0);
            case eAxis.HORIZONTAL:
                return Input.GetAxisRaw("Horizontal");
                //return (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0);
        }

        return 0f;
    }
}
