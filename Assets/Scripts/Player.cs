using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator mySlashFX;
    [SerializeField] private Transform m_GroundCheck;
    bool steppingOnBox = false;
    [SerializeField] private float m_JumpForce = 200f;
    private Rigidbody2D m_Rigidbody2D;
    private Collider2D myCollider;
    private bool m_Grounded;
    const float k_GroundedRadius = .5f;
    [SerializeField] private LayerMask m_WhatIsGround;
    private Animator myAnimator;
    public bool gameOver = false;
    public Transform attackPoint;
    public Transform perfectHitPoint;
    public float attackRange = 0.5f;
    public float perfectHitRange = 0.3f;
    public LayerMask enemyLayers;

    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    public Button myJumpButton;
    public Button mySlashButton;

    public SpriteRenderer mySlashFX2;

    private HitStop myHitStopController;
    public CameraShake cameraShake;
    public GameObject myUICanvas;
    public ScoreCounter myScoreCounter;


    [SerializeField]
    private PerfectHitSign myPerfectHitSign;
    private AudioManager myAudioManager;
    private Color sampleColor;
    //[Header("Events")]
    //[Space]

    //public UnityEvent OnLandEvent;


    private void Awake()
    {
        //if (OnLandEvent == null) OnLandEvent = new UnityEvent();
        //mySlashFX = this.GetComponentInChildren<Animator>();
        myCollider = GetComponent<Collider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Grounded = true;
        myAnimator = GetComponent<Animator>();
        myHitStopController = FindObjectOfType<HitStop>();
        myAudioManager = AudioManager.instance;
        Time.timeScale = 1;
        sampleColor = mySlashFX2.GetComponent<SpriteRenderer>().color;
        myPerfectHitSign = myUICanvas.GetComponentInChildren<PerfectHitSign>();
    }

    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
                {
                    myAnimator.SetBool("Jumping", false);
                    if (!gameOver) myAudioManager.Play("Steps");
                    //OnLandEvent.Invoke();
                }
            }

        }
    }

    public void Jump()
    {
        if (m_Grounded && !gameOver)
        {
            m_Grounded = false;
            m_Rigidbody2D.velocity = new Vector2(0f, 0f);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce) * m_Rigidbody2D.mass);
            myAnimator.SetBool("Jumping", true);
            myAudioManager.StopPlaying("Steps");
        }
    }

    public void Attack()
    {
        if (Time.time > nextAttackTime && !gameOver)
        {
            myAnimator.SetTrigger("Attack");
            mySlashFX.SetTrigger("Attack1");
            nextAttackTime = Time.time + 1f / attackRate;

            StartCoroutine(WaitAttackAnimationThenProceed(0.15f));
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" && !steppingOnBox)
        {
            if (!gameOver)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), myCollider);

                Die();
                StartCoroutine(cameraShake.Shake(0.2f, 0.1f));
                myHitStopController.Stop(0.3f);
                //break object
                playDamageSound(collision.gameObject);
                collision.transform.gameObject.GetComponent<Object>().turnBlank();
                Destroy(collision.transform.parent.gameObject, 0.1f);

            }
            else //if already dead
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), myCollider);
            }
        }

    }


    void Die()
    {
        gameOver = true;
        myAudioManager.StopPlaying("Steps");

        myAnimator.SetBool("Died", true);
        GameObject foreground = GameObject.Find("FloorQuad");
        GameObject background = GameObject.Find("BackGroundQuad");
        foreground.GetComponent<ForegroundScroll>().Stop();
        background.GetComponent<ForegroundScroll>().Stop();
        StartCoroutine(WaitAndDisplay(2.0f));

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || perfectHitPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(perfectHitPoint.position, perfectHitRange);
    }


    private IEnumerator WaitAndDisplay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // myUICanvas.GetComponentInChildren<GameOverMenu>().DisplayCanvas();
        myUICanvas.transform.Find("GameOverMenu").GetComponent<GameOverMenu>().DisplayCanvas();
        Time.timeScale = 0;
    }

    private IEnumerator FloatOnSlash(float waitTime)
    {
        float ySpeed = m_Rigidbody2D.velocity.y;

        if (ySpeed > 5 || ySpeed < -5)
        {
            m_Rigidbody2D.velocity = new Vector2(0, ySpeed / 10f);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(0, ySpeed / 5f);
        }

        float temp = m_Rigidbody2D.gravityScale;
        m_Rigidbody2D.gravityScale = 0.1f;
        yield return new WaitForSeconds(waitTime);
        m_Rigidbody2D.gravityScale = temp;
    }

    private IEnumerator WaitAndDestroyObject(Collider2D enemy)
    {
        while (Time.timeScale != 1.0f)
        {
            yield return null;
        }
        Destroy(enemy.transform.parent.gameObject, 0.0f);
    }

    private IEnumerator WaitAndDisableSlashFX()
    {
        while (Time.timeScale != 1.0f)
        {
            yield return null;
        }
        mySlashFX2.enabled = false;
    }

    private IEnumerator WaitAttackAnimationThenProceed(float waitTime)
    {
        if (!m_Grounded) StartCoroutine(FloatOnSlash(1.0f));
        yield return new WaitForSeconds(waitTime);



        myAudioManager.Play("SlashSwing");
        myAudioManager.Play("Woosh");
        myAudioManager.Play("Sheathe");
        //detect enemies
        Collider2D[] hitEnemies = { };
        Collider2D[] perfectHitEnemies = { };

        if (!gameOver)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            perfectHitEnemies = Physics2D.OverlapCircleAll(perfectHitPoint.position, perfectHitRange, enemyLayers);


        }

        //destroy, damage or interact

        foreach (Collider2D enemy in hitEnemies)
        {
            Object hitObj = enemy.transform.gameObject.GetComponent<Object>();
            hitObj.turnBlank();
            hitObj.ThrowParticles();

            playHitSound(hitObj);
            hitObj.ignoreCollisions(myCollider);

            //mySlashFX2.enabled = true;
            //StartCoroutine(cameraShake.Shake(0.2f, 0.1f));
            //myHitStopController.Stop(0.2f);
            StartCoroutine(WaitAndDestroyObject(enemy));
        }

        //camera shake and hit pause check
        if (hitEnemies.Length > 0)
        {
            mySlashFX2.enabled = true;
            mySlashFX2.GetComponent<SpriteRenderer>().color = sampleColor;



            //check for perfect hit
            if (perfectHitEnemies.Length > 0)
            {
                Debug.Log("Perfect hit!");
                myPerfectHitSign.Display();
                StartCoroutine(cameraShake.Shake(0.05f, 0.2f));
                LeanTween.alpha(mySlashFX2.gameObject, 0, 0.15f);
                myHitStopController.CompositeStopAndSlowMO(0.1f, 0.5f, 0.3f);
                myAudioManager.Play("PerfectHit2");
                myScoreCounter.UpdateScore(10);

            }
            else
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.1f));
                myHitStopController.Stop(0.15f);
            }



            StartCoroutine(WaitAndDisableSlashFX());
        }


    }


    void playHitSound(Object hit)
    {
        switch (hit.transform.parent.name)
        {
            case "Crate":
                myAudioManager.Play("SlashHitCrate");
                break;
            case "Kunai":
                myAudioManager.Play("SlashHitKunai");
                break;
            case "Shuriken":
                myAudioManager.Play("SlashHitKunai");
                break;
        }
    }

    void playDamageSound(GameObject hit)
    {
        switch (hit.transform.parent.name)
        {
            case "Crate":
                myAudioManager.Play("CrateBreak");
                break;
            case "Kunai":
                myAudioManager.Play("BladeHit");
                break;
            case "Shuriken":
                myAudioManager.Play("BladeHit");
                break;
        }
    }


}
