using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class player : NetworkBehaviour{
        [Header("PlayerData")]
        [SyncVar]
        public int playerID;

        [SyncVar]
        public Color color;

        private Rigidbody2D rb;
        private Animator anim;
        private float moveInput;
        public float speed;
        public float jumpforce;
        private bool facingRight = true;
        public float groundRadius;
        public LayerMask whatIsGround;
        public SpriteRenderer[] bodyParts;

        public List<Color> colors = new List<Color>();
       // public GameObject[] allPlayers;



        void Start(){
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            //allPlayers = GameObject.FindGameObjectsWithTag("Player");
            //playerID = ((int)netId );
            
        }
        public override void OnStartClient()
        {
            base.OnStartClient();
            if (isLocalPlayer)
            {
                //playerID = ((int)netId );
                SetColor(colors[(int)netId]);
                Debug.Log((int)netId);
            }
        }


        [Command]
        void Cmd_Color(Color c)
        {
            color = c;
        }

        public void SetColor(Color c)
        {
            for(int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].color = c;
            }
            Cmd_Color(c);     
        }
        

        void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            RaycastHit2D onGround = Physics2D.Raycast(transform.position, -transform.up, groundRadius, whatIsGround);
            if (onGround)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
                }
            }

            if (facingRight && moveInput < 0||!facingRight == true && moveInput > 0)
            {
                facingRight = !facingRight;
                CmdFlip(facingRight);
            }
           

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetBool("run", true);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("run", false);
            }
        }


        [SyncVar(hook = "FacingCallback")]
        public bool netFacingRight = true;

        [Command]
        public void CmdFlip(bool facing)
        {
            netFacingRight = facing;
            if (netFacingRight)
            {
                Vector3 SpriteScale = transform.localScale;
                SpriteScale.x = 1;
                transform.localScale = SpriteScale;
            }
            else
            {
                Vector3 SpriteScale = transform.localScale;
                SpriteScale.x = -1;
                transform.localScale = SpriteScale;
            }
        }

        void FacingCallback(bool facing)
        {
            netFacingRight = facing;
            if (netFacingRight)
            {
                Vector3 SpriteScale = transform.localScale;
                SpriteScale.x = 1;
                transform.localScale = SpriteScale;
            }
            else
            {
                Vector3 SpriteScale = transform.localScale;
                SpriteScale.x = -1;
                transform.localScale = SpriteScale;
            }
        }


    }
}

