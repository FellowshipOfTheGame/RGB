using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component for objects that can be damaged.
/// </summary>
public class HealthBHV : MonoBehaviour
{
    [Header("Health")]
    public float health = 1;
    public bool invincible = false;

    public delegate void OnKilledDelegate(HealthBHV healthBHV);
    public event OnKilledDelegate OnKilled;

    public SpriteTrackImage shield; // TODO: fazer classe mais adequada para o escudo

    [Header("Reward")] 
    public int killScore = 0;
    public int killMoney = 0;

    private bool dead = false; // to prevent multiple Kill() calls

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds damage to the HealthBHV component.
    /// </summary>
    public bool TakeDamage (float damage, int shipColor)
    {    
        if (invincible || dead)
        {
            return false;
        }
        else
        {
            if (this.gameObject.tag == "Enemy")
            {
                //Debug.Log("Damaged");
                if(shipColor == 0)
                    GetComponent<Animator>().SetTrigger("RedDamage");
                else if (shipColor == 1)
                    GetComponent<Animator>().SetTrigger("BlueDamage");
                else
                    GetComponent<Animator>().SetTrigger("GreenDamage");
            }
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
            return true;
        }
    }

    public bool TakeDamage(float damage)
    {
        if (invincible || dead)
        {
            return false;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
            return true;
        }
    }

    // Define o comportamento de morte do objeto
    private void Kill ()
    {
        dead = true;
        // animãção, etc
        
        Debug.Log("Killed!");
        OnKilled?.Invoke(this); // triggers event
        if (this.gameObject.tag == "Enemy")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("Explode");
        }
        else
        {
            Destroy(gameObject);//, 2*Time.deltaTime);
        }
        PlayerSO.Instance.AddScore(killScore);
        PlayerSO.Instance.AddMoney(killMoney);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void MakeInvulnerable(float time)
    {
        invincible = true;
        if (shield == null) return;
        shield.duration = time;
        shield.gameObject.SetActive(true);
    }

    private void MakeVulnerable()
    {
        invincible = false;
        if (shield == null)
        {
            Debug.Log("No shield");
            return;
        }
        shield.gameObject.SetActive(false);
    }
    
    public void SetInvulnerability(float time)
    {
        StartCoroutine(RunInvulnerability(time));
    }

    private IEnumerator RunInvulnerability(float time)
    {
        MakeInvulnerable(time);
        yield return new WaitForSeconds(time);
        MakeVulnerable();
    }
}
