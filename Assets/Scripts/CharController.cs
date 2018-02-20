using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Iso
{
    /// <summary>
    /// 
    /// </summary>
    public class CharController : MonoBehaviour
    {

        [SerializeField] //can modify in inspector without making it public
        float moveSpeed = 4f;

        Vector3 forward, right; //forward is upward and downward for our char

        [HideInInspector]
        public bool isSelected { get; set; } //idk if I want this in the inspector

        // Use this for initialization

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            forward = Camera.main.transform.forward; //Camera is 45 degrees, assign to our Char forward vec
            forward.y = 0;
            forward = Vector3.Normalize(forward); //set length to 1 to use for motion
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //Create rotation

            if (checkIsSelected())
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }

        // Update is called once per frame

        /// <summary>
        /// 
        /// </summary>
        void Update()
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if (checkIsSelected())
                {
                    Select();
                    if (Input.GetButton("HorizontalKey") || Input.GetButton("VerticalKey"))
                    {

                        Debug.Log("Pressed: " + Input.inputString);
                        Move();

                    }
                }
                else
                {
                    Deselect();
                }
            }

        }

        //The character will move isometric up/down and left/right instread of 3D up/down and left/right

        /// <summary>
        /// 
        /// </summary>
        void Move()
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey"); //positive or negative direction for left/right
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey"); //positive or negative direction for up/down

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);  //total direction of left/right up/down and set length to 1. The direction you're HEADING

            transform.forward = heading; //apply the transformation
            transform.position += rightMovement;
            transform.position += upMovement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool checkIsSelected()
        {
            if (tag == "Player")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Select()
        {
            if (!checkIsSelected()) //if it's not already selected, select it
            {
                tag = "Player";
                isSelected = true;
            }
            Debug.Log("Gameobject: " + this.name + " = is selected");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Deselect()
        {
            tag = "Item";
            isSelected = false;
            Debug.Log("Gameobject: " + this.name + " = is NOT selected");
        }
    }
}
