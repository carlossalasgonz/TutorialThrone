using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : VirtualActor {

    [SerializeField] private Slider healthBar;
    [SerializeField] private Transform cursor;

    [Header("Ammo Variables")]
    [SerializeField] private GameObject AmmoPrefab;
    [SerializeField] private int AmmoCount = 50;
    [SerializeField][Tooltip("The speed in ammo per second of the gun")] private float AmmoSpeed = 1.0f;
    private List<Ammo> ammoPool = new List<Ammo>();
    private float reloadTime;

    private void Start() {
        updateHealthBar();
        for(int i = 0; i < AmmoCount; i++) {
            GameObject iAmmo = GameObject.Instantiate<GameObject>(AmmoPrefab);
            ammoPool.Add(iAmmo.GetComponent<Ammo>());
        }
        reloadTime = AmmoSpeed;
}

    void Update() {
        base.Move( new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) );
    }

    private void FixedUpdate() {
        if(reloadTime > 0)
            reloadTime -= Time.fixedDeltaTime;

        if (Input.GetButton("Fire1") && reloadTime <= 0)
            shoot();
    }

    protected override void Hurt() {
        base.Hurt();
        updateHealthBar();
    }

    protected override void Heal() {
        base.Heal();
        updateHealthBar();
    }

    protected override void Die() {
        base.Die();
        GameManager.Instance.EndGame();
    }

    private void updateHealthBar() {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void shoot() {
        Ammo selectedAmmo = ammoPool.Find( x => !x.isActiveAndEnabled );

        Vector2 relativeAim = (Vector2)cursor.transform.position - (Vector2)transform.position;
        float cursorAngle = Mathf.Atan2(relativeAim.y, relativeAim.x) * Mathf.Rad2Deg;
        selectedAmmo.transform.rotation = Quaternion.Euler(0f, 0f, (cursorAngle / 2) - 45);
        selectedAmmo.transform.position = this.transform.position;
        selectedAmmo.gameObject.SetActive(true);
        reloadTime = AmmoSpeed;
    }

    public float calculateAngle(Vector2 transformPosition, Vector2 cursorPosition) {
        Vector2 direction = cursorPosition - transformPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)(transform.position + transform.TransformDirection(transform.up)));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)cursor.position);
        /*
        Gizmos.color = Color.red;
        Vector2 aim = (Vector2)cursor.transform.position - (Vector2)transform.position;
        Gizmos.DrawLine(transform.position, aim);*/
    }
}
