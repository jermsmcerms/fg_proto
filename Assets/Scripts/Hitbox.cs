using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private BoxCollider2D _hitbox;
    private Dictionary<AttackType, Vector2[]> _hitData;

    public void Activate(AttackType type) {
        _hitbox.enabled = true;
        Vector2[] hitboxData = _hitData[type];
        _hitbox.offset = hitboxData[0];
        _hitbox.size = hitboxData[1];
    }

    public void Deactivate() {
        _hitbox.enabled = false;
        _hitbox.size = Vector2.zero;
        _hitbox.offset = Vector2.zero;
    }

    private void Awake() {
        _hitbox = GetComponent<BoxCollider2D>();
        _hitbox.enabled = false;

        _hitData = new Dictionary<AttackType, Vector2[]>();
        _hitData.Add(AttackType.SHORT, new Vector2[] { new Vector2(1.01f, -0.59f), new Vector2(0.97f, 1.45f) });
        _hitData.Add(AttackType.LONG, new Vector2[] { new Vector2(1.12f, -1.0f), new Vector2(1.7f, 0.6f) });
        _hitData.Add(AttackType.SPECIAL, new Vector2[] { new Vector2(1.38f, 0.74f), new Vector2(2.22f, 0.7f) });
        _hitData.Add(AttackType.INVINCIBLE, new Vector2[] { new Vector2(1.0f, -0.33f), new Vector2(0.94f, 1.97f) });

    }

    private void OnTriggerEnter2D(Collider2D collision) {      
        Debug.Log("called: " + collision.gameObject.name);
    }
}
