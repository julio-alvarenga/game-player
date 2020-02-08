/*
 * This is the super class for all playable entities, hereby reffered to as "pilots"
 * Each pilot is a humanoid model and requires an Animator and Character Controller component
 * 
 * The Animator component controls the changing of animation states (ie decides when to play the walk or run animation) 
 * The Character Controller component controls the location of the pilot and how to change the location
 *
 * The Character class acts as a link between both, passing values from the Character Controller to the Animator
 * 
 * The Locomotion class contains the Character Controller and functions used to determine movement
 * The Action class contains the Animator and functions used to change values in the Animator
 *
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Locomotion locomotion;
    protected Action action;

    private Vector3 direction; //used to move/rotate the pilot in a direction 
    private float sensitivity; //used to change animation states based on how hard a player is holding the joystick

    byte b_input; //each bit represents a button being pressed: 1 for pressed, 0 for not

    private void Awake()
    { 
        locomotion = new Locomotion(this.GetComponent<CharacterController>());
        action = new Action(this.GetComponent<Animator>());
    }

    private void Update()
    {
        this.locomotion.Move(direction); //moves the pilot in a direction

        this.action.Animate(this.locomotion.GetNormalizedVelocity(), b_input, this.locomotion.GetDelta(), this.locomotion.GetGrounded(), this.locomotion.GetVerticalVelocity(), this.sensitivity); //animates the player using player input values and values from the locomotion class

        //update locomotion values grabbed from the Action class
        this.locomotion.SetInfluence(this.action.GetSpeedInfluence());
        this.locomotion.SetSpeed((byte)this.action.GetSpeed());

        this.SetRotation(this.locomotion.GetMovement()); //rotates pilot in the direction they are moving
    }

    public void Jump(int jh) {
        this.locomotion.Jump(jh);
    }

    public void Burst() {
        this.locomotion.BurstSpeed();
    }

    public void SetButtonInput(byte b){
        this.b_input = b;
    }

    public void SetDirection(Vector3 d) {
        direction = d;
    }

    public void SetSensitivity(float s) {
        sensitivity = s;
    }

    //update locomotion speed AND animator speed. Animator uses int, so cast as byte for locomotion since locomotion stores speed as a byte
    public void ChangeMaxSpeed(int ms) { this.locomotion.SetMaxSpeed((byte)ms); this.action.SetMaxSpeed(ms); }

    public void RotatePlayer(Vector3 direction)
    {
        direction.y = 0.0f;

        if (direction.x != 0.0f || direction.z != 0.0f)
        {
            Quaternion dir = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, 5.0f * Time.deltaTime);
        }
    } //these might work better in locomotion...

    public void SetRotation(Vector3 direction) {
        direction.y = 0.0f;

        if (direction.x != 0.0f || direction.z != 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    public Transform GetTransform() { return this.transform; }
    public int GetCurrentSpeed() { return this.action.GetSpeed(); }
}
