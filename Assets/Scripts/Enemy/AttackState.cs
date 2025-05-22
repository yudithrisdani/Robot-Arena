using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter() 
    {
        
    }

    public override void Exit() 
    {
       
    }

    public override void Perform() 
    {
        if (enemy.CanSeePlayer()) 
        {

            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //if shot Timer > fireRate
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            //Move the Enemy to a random Position
            if (moveTimer > Random.Range(3, 7)) 
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else 
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8) 
                {
                //Change to Search State
                stateMachine.ChangeState(new ShearchState());
            }
        }

    }
    public void Shoot() {
        //stpre reftence to gun barrel
        Transform gunbarrel = enemy.gunBarrel;

        //instante a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);

        //calculete the direction to rhe player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.position).normalized;

        //add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3,3f),Vector3.up) *  shootDirection * 40;
        Debug.Log("Shoot");
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 