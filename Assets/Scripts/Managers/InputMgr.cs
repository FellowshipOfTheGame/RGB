using UnityEngine;
#if !UNITY_WEBGL
using XInputDotNetPure; // Required in C#
#endif

public class InputMgr : MonoBehaviour
{
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

    public float m_triggMinRatio = .3f;

    [Header("Debug")]
    public bool m_debugMode = false;

    public string m_shoot   = "Space";
    public string m_changeF = "X";
    public string m_changeB = "C";
    public string m_cancel  = "M";
    public string m_special = "Z";
    public string m_pause   = "P";

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //

    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private static InputMgr m_manager = null;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - InputMgr : input manager must be unique!");
        m_manager = this;
    }
    
    // ======================================================================================
    // TODO: Explain: what is the '_player' parameter?
    public static bool GetButton(int _player, eButton _button)
    {
        if (_player > 4 || _player <= 0)
            return false;

#if UNITY_EDITOR
        if (_player == 1 && m_manager.m_debugMode)
        {
            return GetDebugButton(_button);
        }
#endif

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
        }

        return false;
#endif
    }

    // ======================================================================================
    public static float GetAxis(int _player, eAxis _axis)
    {
        if (_player > 4 || _player <= 0)
            return 0f;

#if UNITY_EDITOR
        if (_player == 1 && m_manager.m_debugMode)
        {
            return GetDebugAxis(_axis);
        }
#endif

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
                return Input.GetKey(KeyCode.Space);
            case eButton.SPECIAL:
                return Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.LeftShift);
            case eButton.CHANGEB:
                return Input.GetKey(KeyCode.Q);
            case eButton.CHANGEF:
                return Input.GetKey(KeyCode.E);
            case eButton.PAUSE:
                return Input.GetKey(KeyCode.T);
            case eButton.CANCEL:
                return Input.GetKey(KeyCode.R);
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
