using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private static readonly int FULL_CHARGE_AMOUNT = 30;
    private static readonly int SHORT_POKE_ATTACK_DURATION = 22;
    private static readonly int LONG_POKE_ATTACK_DURATION = 23;
    private static readonly int SPECIAL_ATTACK_DURATION = 49;
    private static readonly int INVINCIBLE_ATTACK_DURATION = 56;

    private BoxCollider2D[] _hurtboxes;

    private enum AttackType {
        NONE, SHORT, LONG, SPECIAL, INVINCIBLE, DEMON
    }

    private AttackType _attackType;

    private int[] _shortAttackFrameData      = { 5, 2, 16 };
    private int[] _longAttackFrameData       = { 4, 3, 15 };
    private int[] _specialAttackFrameData    = { 12, 4, 29 };
    private int[] _invincibleAttackFrameData = { 3, 6, 47 };


    private int _attackCountdown;
    private int _chargeCounter;
    private bool _isAttackCharged;

    public void ChargeSpecialAttack() {
        _chargeCounter++;
        Debug.Log("charging...");
        if (_chargeCounter >= FULL_CHARGE_AMOUNT) {
            _isAttackCharged = true;
            Debug.Log("charged!");
        }
    }

    public bool IsAttackCharged() { return _isAttackCharged; }

    public bool PerformingAttack() {
        return _attackCountdown > 0;
    }

    public void PerformLongPokeAttack() {
        _attackCountdown = LONG_POKE_ATTACK_DURATION;
        _attackType = AttackType.LONG;

    }

    public void PerformShortPokeAttack() {
        _attackCountdown = SHORT_POKE_ATTACK_DURATION;
        _attackType = AttackType.SHORT;
    }

    public void PerformSpecialAttack() {
        _attackCountdown = SPECIAL_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.SPECIAL;
    }

    public void PerformInvincibleAttack() {
        _attackCountdown = INVINCIBLE_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.INVINCIBLE;
    }

    private void ResetHurtBoxes() {
        Debug.Log("reset hurtboxes");
        _hurtboxes[2].enabled = false;
    }

    private void UpdateHurtboxes() {
        switch (_attackType) {
            case AttackType.SHORT: {

                break; 
            }

            case AttackType.LONG: {
                int attackPhase = (LONG_POKE_ATTACK_DURATION - _attackCountdown);
                if (attackPhase <= _longAttackFrameData[0]) {
                    _hurtboxes[2].enabled = true;
                    _hurtboxes[2].size = new Vector2(2.0f, 0.75f);
                    _hurtboxes[2].offset = new Vector2(1.0f, -0.77f);
                } else if(attackPhase > _longAttackFrameData[0] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] - 1) {
                } else if(attackPhase > _longAttackFrameData[1] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] + _longAttackFrameData[2]) {
                }
                break; 
            }
            case AttackType.SPECIAL: { break; }
            case AttackType.INVINCIBLE: { break; }
            case AttackType.DEMON: { break; }
        }
    }

    private void FixedUpdate() {
        if(PerformingAttack()) {
            UpdateHurtboxes();
            _attackCountdown--;
            if (_attackCountdown == 0) {
                _attackType = AttackType.NONE;
                ResetHurtBoxes();
            } else if (_attackCountdown < 0) {
                _attackCountdown = 0;
            }
        }
    }

    private void Awake() {
        _hurtboxes = GetComponentsInChildren<BoxCollider2D>();
        _hurtboxes[2].enabled = false;
        _attackType = AttackType.NONE;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
