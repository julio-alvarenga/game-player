/*
 * The Locomotion class contains the Character Controller and functions used to manipulate the movement of a pilot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion
{

    CharacterController cc;

    float gravity_influnce = 10.0f;
    byte c_speed, m_speed, weight; //usually stored as integers, but speed and weight values will never surpass ~50 so efficient to store as a byte
    float s_influence, velocity;

    bool exhausted;

    Vector3 move, direction; //move is the actual movement vector, direction is the value passed in by the Character class

    public Locomotion(CharacterController c) {
        this.cc = c;
        this.m_speed = 10;
        this.c_speed = m_speed;
        this.s_influence = 0.85f;
        this.weight = 15;

        exhausted = false;
    }

    //to be called in Update by Character
    public void Move(Vector3 d) {

        SetDirection(d);
        ApplyMovement();
        
    }

    public void ApplyMovement() {

        MovementCheck();

        cc.Move(this.move * Time.deltaTime);
    }

    void MovementCheck() {

        move = Vector3.Lerp(move, direction * c_speed, s_influence);

        //TODO: rework so lower weight == higher jumps and slower descends
        move.y += -weight * 0.15f;
        move.y = Mathf.Clamp(move.y, -weight * 30, weight * 30);
    }

    public void Jump(int jh) {
        move.y = jh;
    }

    public void BurstSpeed() {
        move = direction * m_speed;
        Debug.Log("BURST: " + direction);
    }

    public void SetSpeed(byte s) { this.c_speed = s; }
    public void SetMaxSpeed(byte ms) { this.m_speed = ms; this.c_speed = this.m_speed; }
    public void SetWeight(byte w) { this.weight = w; }
    public void SetInfluence(float si) { this.s_influence = si; }
    public void SetDirection(Vector3 d) { this.direction = d; }
    //public void SetButtonByte(byte b) { this.b_press = b; }

    //Animator Values
    public float GetNormalizedVelocity() { return (cc.velocity.sqrMagnitude*0.05f) / this.m_speed; }
    public float GetVerticalVelocity() { return this.move.y / this.weight; }
    public float GetDelta() { return Vector3.SignedAngle(new Vector3(this.direction.x, 0.0f, this.direction.z), new Vector3(this.move.x, 0.0f, this.move.z), Vector3.up); }
    public float GetWeight() { return this.weight; }
    public bool  GetGrounded() { return this.cc.isGrounded; }
    public int   GetMaxSpeed() { return (int)this.m_speed; }
    public Vector3 GetMovement() { return new Vector3(this.move.x, 0.0f, this.move.z); }
}

//s_influence = Mathf.Lerp(s_influence, 0.0f, Time.deltaTime);
