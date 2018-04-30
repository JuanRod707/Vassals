using UnityEngine;
using System.Collections;

public class UnitCharge : MonoBehaviour
{
    public float Speed;
    public float RetreatSpeed;
    public float MaxStartIdleTime;
    public UnitType TypeOfUnit;
    public float RetreatDistance;

    /// <summary>
    /// MoveStates 0: idle; 1: Walking; 2: Attacking; 3: Retreating; 4:Cheering
    /// </summary>
    private int moveState = 0;
    private Animator anim;
    private float startTime;
    private float startTimerElapsed = 0;
    private GameObject triggered;
    private bool IsRetreating = false;
    private SpriteRenderer unitSprite;
    
    void Start()
    {
        anim = this.GetComponent<Animator>();
        startTime = Random.Range(0f, MaxStartIdleTime);
        unitSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState == 0 & !IsRetreating)
        {
            startTimerElapsed += Time.deltaTime;

            if (startTimerElapsed > startTime)
            {
                moveState = 1;
                anim.SetInteger("MovementState", moveState);
            }
        }

        if (moveState == 1 & !IsRetreating)
        {
            this.transform.Translate(Vector2.right * Speed);
        }

        if (moveState == 2 & !IsRetreating)
        {
            if (triggered == null)
            {
                moveState = 1;
                anim.SetInteger("MovementState", moveState);
            }
        }

        if (moveState == 3)
        {
            this.transform.Translate(Vector2.right * RetreatSpeed);
        }

        if (Mathf.Abs(this.transform.position.x) > RetreatDistance)
        {
            unitSprite.enabled = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals(this.tag) & !IsRetreating)
        {
            moveState = 1;
            anim.SetInteger("MovementState", moveState);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals(this.tag) & !IsRetreating)
        {
            moveState = 2;
            anim.SetInteger("MovementState", moveState);
            triggered = collision.gameObject;
        }
    }

    public void Retreat()
    {
        moveState = 3;
        anim.SetInteger("MovementState", 1);
        IsRetreating = true;
        unitSprite.flipX = !unitSprite.flipX;
    }

    public void Cheer()
    {
        moveState = 4;
        anim.SetInteger("MovementState", moveState);
    }
}
