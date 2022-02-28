using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Cainos.PixelArtMonster_Dungeon
{
    //to feed the MonsterController input parameters using mouse and keyboard input
    public class MonsterInputMouseAndKeyboard : MonoBehaviour
    {
        public KeyCode upKey = KeyCode.W;
        public KeyCode downKey = KeyCode.S;
        public KeyCode leftKey = KeyCode.A;
        public KeyCode rightKey = KeyCode.D;

        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode moveModifierKey = KeyCode.LeftShift;

        public KeyCode attackKey = KeyCode.Mouse0;
        public bool isLeft = false;
        public float attackSpeed = 1.0f;
        public GameObject player;
        private MonsterController controller;
        private MonsterFlyingController controllerFlying;

        public Vector2 inputMove;
        private bool inputMoveModifier;
        private bool inputJump;
        private bool inputAttack;
        public float timeToFire;
        private bool foundPlayer = false;
        public float health = 100;
        public float attackDammage = 10;
        private void Awake()
        {
            controller = GetComponent<MonsterController>();
            controllerFlying = GetComponent<MonsterFlyingController>();
            timeToFire = 0f;
 
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.gameObject == player)
            {
                Debug.Log("hit player!");
                foundPlayer = true;
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.gameObject == player)
            {
                Debug.Log("done hitting player!");
                foundPlayer = false;
            }
        }

        public void KnockBack()
        {
            controller.walkSpeedMax = 8f;
            //inputMove.
        }
        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(3f);
            gameObject.active = false;
        }

        IEnumerator AttackDelayCoroutine(Cainos.CustomizablePixelCharacter.PixelCharacter script1, Cainos.CustomizablePixelCharacter.PixelCharacterController script2)
        {
            yield return new WaitForSeconds(0.45f);
            var monsterFacing = gameObject.GetComponent<Cainos.PixelArtMonster_Dungeon.PixelMonster>();
            script2.TakeDammage(attackDammage);
            if( (script1.Facing == 1 && monsterFacing.Facing == -1 )|| (script1.Facing == -1 && monsterFacing.Facing == 1))
            {
                script1.InjuredFront();
            }
            else
            {
                script1.InjuredBack();
            }
            
        }

        private void Update()
        {
            bool pointerOverUI = EventSystem.current && EventSystem.current.IsPointerOverGameObject();
            if(health <= 0)
            {
                controller.IsDead = true;
                gameObject.layer = 8;
                StartCoroutine(SelfDestruct());
            }
            if (!pointerOverUI)
            {
                inputMoveModifier = Input.GetKey(moveModifierKey);
                inputJump = Input.GetKey(jumpKey);
                inputAttack = Input.GetKeyDown(attackKey);
                if (controller)
                {
                    controller.inputMoveModifier = inputMoveModifier;
                    controller.inputJump = inputJump;
                    
                }
                if (controllerFlying)
                {
                    controllerFlying.inputAttack = inputAttack;
                }
            }

            if (!foundPlayer)
            {
                if (isLeft)
                {
                    inputMove.x = 1.0f;
                }
                else
                {
                    inputMove.x = -1.0f;
                }
            }
            else
            {
                timeToFire -= Time.deltaTime;
                inputMove.x = 0f;
                if (timeToFire <= 0f)
                {
                    Debug.Log("attack!");
                    timeToFire = attackSpeed;
                    var script1 = player.GetComponent<Cainos.CustomizablePixelCharacter.PixelCharacter>();
                    var script2 = player.GetComponent<Cainos.CustomizablePixelCharacter.PixelCharacterController>();
                    StartCoroutine(AttackDelayCoroutine(script1,script2));
                    controller.inputAttack = true;
                    
                }
            }
         
            
          
            //move horizontal
            /*
            if (Input.GetKey(leftKey))
            {
                inputMove.x = -1.0f;
            }
            else if (Input.GetKey(rightKey))
            {
                inputMove.x = 1.0f;
            }
            else
            {
                inputMove.x = 0.0f;
            }

            //move vertical
            if (Input.GetKey(downKey))
            {
                inputMove.y = -1.0f;
            }
            else if (Input.GetKey(upKey))
            {
                inputMove.y = 1.0f;
            }
            else
            {
                inputMove.y = 0.0f;
            }

            */
            if (controller) controller.inputMove = inputMove;
            if (controllerFlying) controllerFlying.inputMove = inputMove;
            
        }
    }



}
