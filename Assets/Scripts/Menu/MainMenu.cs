using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Everything works pretty much the same as controlling the ships, but with the menu handling stuff
 */

public class MainMenu : MonoBehaviour
{
    
    public Transform[] transforms = new Transform[3];
    private List<Button> buttons = new List<Button>();
    [SerializeField]
    private Button returnButton;
    private Vector2 centralPosition = Vector2.zero;
    public float m_changeCoolDownDuration = 1;
    private float m_changeCooldownTimer = 0;
    private float creditsCooldownTimer = 0;
    public float creditsCooldownDuration = 0.5f;
    private float creditsTimer = 0;
    private float m_changeTimer = 0;
    private int m_shipIndex = 0;
    private bool m_changeFoward = true;
    private bool m_changeBackward = false;
    [SerializeField]
    private GameObject cueRotateLeft, cueRotateRight, creditsMenu, nameInput;

    private Image actualCueActive;
    private readonly float pressedAlpha = 1.0f;
    private readonly float commonAlpha = 0.4f;

    public float m_positionLerpFactor = 0.5f;

    public float m_scaleLerpFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Set every button not active to half its size and position everything in the right place. With the play button as the current selection
         * Calculates the central position based on the position of every button transform in the scene hierarchy
         * Important note: place the transforms within correct distance. I.E.: for an equilateral triangle, with all within the same "a" distance between them.
         */ 
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.position = transforms[i].localPosition;
            if (i != 0)
            {
                buttons[i].transform.localScale = 0.5f * Vector2.one;
            }
            else
            {
                buttons[i].GetComponent<Outline>().enabled = true;
            }
            centralPosition += (Vector2)transforms[i].localPosition;
        }
        centralPosition /= buttons.Count;
    }

    // Update is called once per frame
    void Update()
    {
        //If we are in the menu, handle input
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            //If we are in the credits screen, check if the user pressed to go back and go back if the cooldown has been reached.
            //The cooldown keeps Unity from calling and them closing immediately the screen or vice-versa.
            if (creditsMenu.activeSelf)
            {
                if (InputMgr.GetButton(1, InputMgr.eButton.ATTACK) && creditsTimer == 0)
                {
                    returnButton.onClick.Invoke();
                }
            }
            /*
             * If the button to change ships/options is pressed, change the position of the buttons.
             * Update their positions
             * If the attack/select button is pressed, do the action corresponding to the selected button.
             * Also plays the corresponding sound
            */
            else
            {
                m_changeBackward = InputMgr.GetButton(1, InputMgr.eButton.CHANGEB);
                m_changeFoward = InputMgr.GetButton(1, InputMgr.eButton.CHANGEF);
                // change ships
                if (m_changeFoward && m_changeTimer == 0)
                {
                    ChangeOption(1);
                }
                if (m_changeBackward && m_changeTimer == 0)
                {
                    ChangeOption(-1);
                }
                UpdateTransform();
                if (InputMgr.GetButton(1, InputMgr.eButton.ATTACK) && creditsTimer == 0)
                {
                    if (m_shipIndex == 0)
                        FindObjectOfType<MenuAudioManager>().Play("StartGameSnd");
                    else
                        FindObjectOfType<MenuAudioManager>().Play("OptionSelectedSnd");
                    buttons[m_shipIndex].onClick.Invoke();
                }
            }
        }
    }

    void Awake()
    {
        //Get all the buttons in the menu and disable their outlines
        foreach (Button button in GetComponentsInChildren<Button>(true))
        {
            buttons.Add(button);
            button.GetComponent<Outline>().enabled = false;
        }
    }

    // ================================  Change Menu's Options' Positions  ======================================================
    //Change them in the same rotation movement as the ships'
    private void ChangeOption(int direction = 1)
    {
        m_changeCooldownTimer = m_changeCoolDownDuration;
        m_changeTimer = m_changeCoolDownDuration;
        int mainShip = m_shipIndex;
        m_shipIndex = (m_shipIndex + direction + buttons.Count) % buttons.Count;

        //Play the sound of changing menu's options
        FindObjectOfType<MenuAudioManager>().Play("MenuScrollSnd");

        //Get the arrow image cue according to the movement's direction
        if (direction == 1)
        {
            actualCueActive = cueRotateRight.GetComponent<Image>();
        }
        else
        {
            actualCueActive = cueRotateLeft.GetComponent<Image>();
        }

        //Set the transparency of the corresponding image cue to a high value to give feedback to the player
        actualCueActive.color = new Color(actualCueActive.color.r, actualCueActive.color.g, actualCueActive.color.b, pressedAlpha);

        //Rotate every button 120º from the central position
        for (int i = 0; i < buttons.Count; i++)
        {
            transforms[i].localPosition = Quaternion.Euler(new Vector3(0, 0, -120 * direction)) * (transforms[i].localPosition - (Vector3)centralPosition) + (Vector3)centralPosition;
        }

        //Disable the outline from the previously selected button and activate it in the new one.
        buttons[mainShip].GetComponent<Outline>().enabled = false;
        buttons[m_shipIndex].GetComponent<Outline>().enabled = true;

        //Start counting the cooldown so we can change buttons again later, but keeps Unity from changing them too fast.
        StartCoroutine(ChangeCooldown());
    }

    //Just start the timer and change everything back to normal (including the arrow's visual cue) when its over
    private IEnumerator ChangeCooldown()
    {
        yield return new WaitForSeconds(m_changeCooldownTimer);
        m_changeTimer = 0;
        m_changeCooldownTimer = 0;
        actualCueActive.color = new Color(actualCueActive.color.r, actualCueActive.color.g, actualCueActive.color.b, commonAlpha);
    }

    //Same as above, without the cue
    private IEnumerator CreditsCooldown()
    {
        yield return new WaitForSeconds(creditsCooldownTimer);
        creditsTimer = 0;
        creditsCooldownTimer = 0;
    }

    // ======================================================================================
    private void UpdateTransform()
    {
        // update transform
        for (int i = 0; i < buttons.Count; i++)
        {
            //The selected button gets scale 1 and goes to the top
            if (i == m_shipIndex)
            {
                buttons[i].transform.localScale = Vector2.Lerp(buttons[i].transform.localScale, Vector2.one, m_scaleLerpFactor);
                buttons[i].transform.localPosition = Vector3.Lerp(buttons[i].transform.localPosition, transforms[i].position - transform.position + Vector3.forward, m_positionLerpFactor);
            }
            //The other buttons go down, are scaled to half the size, and, to correct the positioning after the re-scale, are shifted by half their width to the left/right
            else
            {
                buttons[i].transform.localScale = Vector2.Lerp(buttons[i].transform.localScale, 0.5f * Vector2.one, m_scaleLerpFactor);
                if((m_shipIndex+1)%3 == i)
                    buttons[i].transform.localPosition = Vector3.Lerp(buttons[i].transform.localPosition, transforms[i].position - transform.position + Vector3.forward - new Vector3(buttons[i].GetComponent<RectTransform>().rect.width/2, 0, 0), m_positionLerpFactor);
                else
                {
                    buttons[i].transform.localPosition = Vector3.Lerp(buttons[i].transform.localPosition, transforms[i].position - transform.position + Vector3.forward + new Vector3(buttons[i].GetComponent<RectTransform>().rect.width/2, 0, 0), m_positionLerpFactor);
                }
            }
        }
    }

    //Starts the cooldown countdown, disables the credits menu, going back to the main menu
    public void ReturnFromCredits()
    {
        creditsCooldownTimer = creditsCooldownDuration;
        creditsTimer = creditsCooldownDuration;
        creditsMenu.SetActive(false);
        StartCoroutine(CreditsCooldown());
    }

    //Starts the cooldown countdown, enables the credits menu, overlaying the main menu
    public void GoToCredits()
    {
        creditsCooldownTimer = creditsCooldownDuration;
        creditsTimer = creditsCooldownDuration;
        creditsMenu.SetActive(true);
        StartCoroutine(CreditsCooldown());
    }

    public void GoToNameInput()
    {
        nameInput.SetActive(true);
        
    }

    public void ReturnFromNameInput()
    {
        nameInput.SetActive(false);
    }
}
