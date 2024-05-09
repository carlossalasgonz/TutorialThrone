using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

abstract public class VirtualActor : MonoBehaviour {

    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float maxHealth = 100.0f;
    protected float actorWidth;

    private Rigidbody2D rg;
    public Rigidbody2D Rg { get => rg; }

    protected float health;
    public float Health {
        get => this.health;

        set {
            this.health += value;
            if (value > 0) {
                Heal();
            } else {
                Hurt();
                if (this.health <= 0)
                    Die();
            }
        }
    }

    virtual protected void Awake() {
        rg = this.gameObject.GetComponent<Rigidbody2D>();
    }

    virtual protected void OnEnable() {
        health = maxHealth;
        actorWidth = transform.localScale.x * GetComponent<CircleCollider2D>().radius;
    }

    virtual protected void Move(Vector2 moveVector) {
        Vector2 mV = moveVector.normalized * speed;
        Rg.velocity = mV;
    }

    virtual protected void Hurt() { }
    virtual protected void Heal() { }
    virtual protected void Die() { }
}
