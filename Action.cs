/*
 * Action class contains an Animator, and fields used by the Animator that change animations
 * Animations are represented as a state machine. Animations exist as states, ie "Idle", "Run", and "Jump" are states
 * Animator changes states based on values, use these setters to update the values which then change animations
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    Animator anim;

    float velocity_normalized;
    float vertical_velocity;
    float js_direction;
    float delta;
    float sensitivity;

    bool grounded;

    byte b_press;

    public Action(Animator animator) {
        this.anim = animator;
    }

    //to be called in Update by Character class
    public void Animate(float vn, byte input, float d, bool g, float vv, float s) {

        //these functions update the values...
        SetVelocityNormalized(vn);
        SetVerticalVelocity(vv);
        SetButtonByte(input);
        SetDelta(d);
        SetGrounded(g);
        SetSensitivity(s);

	//... these functions reflect the changes to the Animator
        SetFloats();
        SetBools();
    }

    void SetFloats() {
        anim.SetFloat("velocity", this.velocity_normalized);
        anim.SetFloat("vertical", this.vertical_velocity);
        anim.SetFloat("sensitivity", this.sensitivity); //sensitivity of joystick
        anim.SetFloat("delta", this.delta);
    }

    void SetBools() {
        anim.SetBool("roll", ((this.b_press & 2) == 2));
        anim.SetBool("jump", ((this.b_press & 1) == 1));
        anim.SetBool("grounded", this.grounded);
    }

    void SetVelocityNormalized(float vn) { this.velocity_normalized = vn; }
    void SetVerticalVelocity(float vv) { this.vertical_velocity = vv; }
    void SetDelta(float d) { this.delta = d; }
    void SetGrounded(bool g) { this.grounded = g; }
    void SetButtonByte(byte b) { this.b_press = b; }
    void SetSensitivity(float s) { this.sensitivity = s; }


    public void SetMaxSpeed(int ms) { anim.SetInteger("m_speed", ms); }

    public float GetSpeedInfluence() { return anim.GetFloat("s_influence"); }
    public int GetSpeed() { return anim.GetInteger("c_speed"); }
    public int GetMaxSpeed() { return anim.GetInteger("m_speed"); }

}