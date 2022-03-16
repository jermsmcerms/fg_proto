using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtboxes : MonoBehaviour
{
    private BoxCollider2D[] _hurtboxes;
    private bool _performingAttack;
    private int _attackCountdown;

    public void ResizeForJump() {
        foreach (BoxCollider2D box in _hurtboxes) {
            box.size = new Vector2(0.8f, 1.0f);
            box.offset = Vector2.zero;
        }
    }

    public void ResetDefaultBoxSettings() {
        _hurtboxes[0].size = new Vector2(0.78f, 0.38f);
        _hurtboxes[0].offset = new Vector2(-0.04f, -0.31f);

        _hurtboxes[1].size = new Vector2(0.7f, 0.67f);
        _hurtboxes[1].offset = new Vector2(0, 0.22f);

        _hurtboxes[2].size = new Vector2(0.25f, 0.25f);
        _hurtboxes[2].offset = new Vector2(0.17f, 0.58f);
    }
    public bool PerfomingAttack() {
        return _attackCountdown > 0;
    }

    public void StartAttack(int countdown) {
        _attackCountdown = countdown;
        _performingAttack = true;
    }

    public void UpdateHurtboxes() {
        if(_performingAttack) {
            if(_attackCountdown > 0) {
                if(_attackCountdown == 12) {
                    _hurtboxes[2].size = new Vector2(0.75f,0.25f);
                    _hurtboxes[2].offset = new Vector2(0.42f, 0.58f);
                }
                _attackCountdown--;
            } else {
                _performingAttack = false;
                ResetDefaultBoxSettings();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _hurtboxes = GetComponents<BoxCollider2D>();
        _performingAttack = false;
        _attackCountdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
