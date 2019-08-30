using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float speed = 6.0f;
    private float verticalVelocity=0.0f;
    private float gravity = 12.0f;
    private bool isDead = false;
    private float count1 = 0f;
    private float count2 = 0f;
    private float count3 = 0f;
    int inputchar;

    string filepath = "bend.txt";
    SerialPort sp = new SerialPort("COM", 115200);
    // is called before the first frame update
    void Start()
    {
        //sp.Open();
        //sp.ReadTimeout = 1;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        moveVector = Vector3.zero;
        

        //X
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //z
        moveVector.z = speed;
        //Y
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        if (sp.IsOpen)
        {
            //o = o + 1;

            try
            {
                //Debug.Log("object" + o);
                switch (sp.ReadChar())
                {
                    case '1':
                        {
                            count1 = count1 + 1f;
                            Debug.Log("good going! can improve");
                            transform.Translate(speed * Time.deltaTime, 0f, 0f);
                            break;
                        }
                    case '2':
                        {
                            count2 = count2 + 1f;
                            Debug.Log("perfect! keep going");
                            transform.Translate(speed * Time.deltaTime, 0f, 0f);
                            break;
                        }
                    case '3':
                        {
                            count3 = count3 + 1f;
                            Debug.Log("Dont bend too much");
                            transform.Translate(speed * Time.deltaTime, 0f, 0f);
                            break;
                        }
                    case '4':
                        {
                            count1 = count1 + 1f;
                            Debug.Log("good going! can improve");
                            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
                            break;
                        }
                    case '5':
                        {
                            count2 = count2 + 1f;
                            Debug.Log("perfect! keep going");
                            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
                            break;
                        }
                    case '6':
                        {
                            count3 = count3 + 1f;
                            Debug.Log("Dont bend too much");
                            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
                            break;
                        }


                    default:
                        Debug.Log("saathrela");
                        break;

                }
                /*if(sp.ReadChar()=='4')
                {
                    count1 = count1 + 1f;
                    Debug.Log("good going! can improve");
                    transform.Translate(-2f * speed*Time.deltaTime, 0f, 0f);
                    
                }
                if (sp.ReadChar() == '5')
                {
                    count2 = count2 + 1f;
                    Debug.Log("great going keep it up");
                    transform.Translate(-2f * speed * Time.deltaTime, 0f, 0f);
                }
                if (sp.ReadChar() == '6')
                {
                    count3 = count3 + 1f;
                    Debug.Log("nice! could do better");
                    transform.Translate(-2f * speed * Time.deltaTime, 0f, 0f);
                }*/
                /*if (sp.ReadChar() == '1')
                {
                    count1 = count1 + 1f;
                    Debug.Log("good going! can improve");
                    transform.Translate(2f * speed*Time.deltaTime, 0f, 0f);
                }
                if (sp.ReadChar() == '2')
                {
                    count2 = count2 + 1f;
                    Debug.Log("great going keep it up");
                    transform.Translate(2f * speed * Time.deltaTime, 0f, 0f);
                }
                /*if (sp.ReadChar() == '3')
                {
                    count3 = count3 + 1f;
                    Debug.Log("nice! could do better");
                    transform.Translate(2f * speed * Time.deltaTime, 0f, 0f);
                }*/
                // Debug.Log("objdfgdfect1" + o);


            }
            catch (System.Exception)
            {

            }
        } 

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius)
        {
            Death();
        }
    }
    private void Death()
    {
        Debug.Log("Dead");
        if (Time.timeSinceLevelLoad > 2)
        {
            isDead = true;
            GetComponent<Score>().onDeath();

        }
    }
}
