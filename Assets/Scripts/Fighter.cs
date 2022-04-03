using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum AttackType {
    NONE, SHORT, LONG, SPECIAL, INVINCIBLE, DEMON
}

public class Fighter : MonoBehaviour
{
    /* These fields may be able to be pulled out of code as data */
    private static readonly int FULL_CHARGE_AMOUNT = 30;
    private static readonly int SHORT_POKE_ATTACK_DURATION = 22;
    private static readonly int LONG_POKE_ATTACK_DURATION = 23;
    private static readonly int SPECIAL_ATTACK_DURATION = 49;
    private static readonly int INVINCIBLE_ATTACK_DURATION = 56;
    private static readonly int FORWARD_DASH_DURATION = 15;
    private static readonly int BACKWARD_DASH_DURATION = 10;
    private int[] _shortAttackFrameData      = { 5, 2, 16 };
    private int[] _longAttackFrameData       = { 4, 3, 15 };
    private int[] _specialAttackFrameData    = { 12, 4, 29 };
    private int[] _invincibleAttackFrameData = { 3, 6, 47 };
    private AttackType _attackType;
    private int _numBlocks = 3;
    private int _chargeCounter;
    private bool _canCancel;
    /*-------------------------------------------------------------*/


    [SerializeField] private bool _facingRight;

    private BoxCollider2D _hurtboxBody;
    private BoxCollider2D _hurtboxLimbVertical;
    private BoxCollider2D _hurtboxLimbHorizontal;
    private Hitbox _hitbox;


    private int _actionCountdown;
    private bool _isAttackCharged;
    private bool _hitUnblockingOpponent;
    [SerializeField]private bool _isBlocking;
    private bool _applyHit;

    public void ChargeSpecialAttack() {
        _chargeCounter++;
        if (_chargeCounter >= FULL_CHARGE_AMOUNT) {
            _isAttackCharged = true;
        }
    }

    public bool CanCancel() { return _canCancel; }

    internal AttackType GetAttackType() { return _attackType; }

    public bool IsFacingRight() { return _facingRight; }

    public bool IsAttackCharged() { return _isAttackCharged; }

    public bool PerformingAction() { return _actionCountdown > 0; }

    public void PerformLongPokeAttack() {
        _actionCountdown = LONG_POKE_ATTACK_DURATION;
        _attackType = AttackType.LONG;
    }

    public void PerformShortPokeAttack() {
        _actionCountdown = SHORT_POKE_ATTACK_DURATION;
        _attackType = AttackType.SHORT;
    }

    public void PerformSpecialAttack() {
        _actionCountdown = SPECIAL_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.SPECIAL;
    }

    public void PerformInvincibleAttack() {
        _actionCountdown = INVINCIBLE_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.INVINCIBLE;
    }

    private void UpdateHurtboxes() {
        switch (_attackType) {
            case AttackType.SHORT: {
                int attackPhase = (SHORT_POKE_ATTACK_DURATION - _actionCountdown);
                if (attackPhase <= _shortAttackFrameData[0]) {
                } else if (attackPhase > _shortAttackFrameData[0] && attackPhase <= _shortAttackFrameData[0] + _shortAttackFrameData[1] - 1) {
                    _hitbox.Activate(_attackType);                  
                } else if (attackPhase > _shortAttackFrameData[1] && attackPhase <= _shortAttackFrameData[0] + _shortAttackFrameData[1] + _shortAttackFrameData[2]) {
                   _hitbox.Deactivate();
                }
                break; 
            }

            case AttackType.LONG: {
                int attackPhase = (LONG_POKE_ATTACK_DURATION - _actionCountdown);
                if (attackPhase <= _longAttackFrameData[0]) {
                } else if(attackPhase > _longAttackFrameData[0] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] - 1) {
                    _hitbox.Activate(_attackType);
                } else if(attackPhase > _longAttackFrameData[1] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] + _longAttackFrameData[2]) {
                    _hitbox.Deactivate();
                }
                break; 
            }

            case AttackType.SPECIAL: {
                int attackPhase = (SPECIAL_ATTACK_DURATION - _actionCountdown);
                if (attackPhase <= _specialAttackFrameData[0]) {
                } else if (attackPhase > _specialAttackFrameData[0] && attackPhase <= _specialAttackFrameData[0] + _specialAttackFrameData[1] - 1) {
                    _hitbox.Activate(_attackType);
                } else if (attackPhase > _specialAttackFrameData[1] && attackPhase <= _specialAttackFrameData[0] + _specialAttackFrameData[1] + _specialAttackFrameData[2]) {
                    _hitbox.Deactivate();
                }
                break;
            }

            case AttackType.INVINCIBLE: {
                int attackPhase = (INVINCIBLE_ATTACK_DURATION - _actionCountdown);
                if (attackPhase <= _invincibleAttackFrameData[0]) {
                    _hurtboxBody.enabled = false;
                } else if (attackPhase > _invincibleAttackFrameData[0] && attackPhase <= _invincibleAttackFrameData[0] + _invincibleAttackFrameData[1] - 1) {
                    _hitbox.Activate(_attackType);
                } else if (attackPhase > _invincibleAttackFrameData[1] && attackPhase <= _invincibleAttackFrameData[0] + _invincibleAttackFrameData[1] + _invincibleAttackFrameData[2]) {
                    _hitbox.Deactivate();
                }
                break;
            }

            case AttackType.DEMON: { break; }
        }
    }

    internal void SetBlocking(bool blocking) {
        _isBlocking = blocking;
    }

    private void ApplyBlockStun(int stun) {
        _actionCountdown = stun; // TODO: change this to something that can block movement as well.
        _numBlocks--;
    }

    public bool GetBlockState() {
        return _isBlocking;
    }

    internal void PerformRightDash() {
        if(_facingRight) {
            _actionCountdown = FORWARD_DASH_DURATION;
        } else {
            _actionCountdown = BACKWARD_DASH_DURATION;
        }
    }

    internal void PerformLeftDash() {
        if (_facingRight) {
            _actionCountdown = BACKWARD_DASH_DURATION;
        } else {
            _actionCountdown = FORWARD_DASH_DURATION;
        }
    }

    private void FixedUpdate() {
        if(PerformingAction()) {
            UpdateHurtboxes();
            _actionCountdown--;
            if (_actionCountdown == 0) {
                _canCancel = false;
                _attackType = AttackType.NONE;
            } else if (_actionCountdown < 0) {
                _actionCountdown = 0;
            }
        }
    }

    private void Awake() {
            _hurtboxBody = gameObject.transform.Find("Hurtboxes/HurtboxBody").GetComponent<BoxCollider2D>();
        
            _hurtboxLimbVertical = gameObject.transform.Find("Hurtboxes/HurtboxLimbVertical").GetComponent<BoxCollider2D>();
            _hurtboxLimbVertical.enabled = false;

            _hurtboxLimbHorizontal= gameObject.transform.Find("Hurtboxes/HurtboxLimbHorizontal").GetComponent<BoxCollider2D>();
            _hurtboxLimbHorizontal.enabled = false;

            _hitbox = gameObject.transform.Find("Hitbox").GetComponent<Hitbox>();
            _hitbox.enabled = false;

            _attackType = AttackType.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        // The opponent can only guard three attacks. So, if they're out of blocks, then override their block ability.
        if(_numBlocks <= 0) {
            _isBlocking = false;
        }

        if (_hitUnblockingOpponent && (_attackType == AttackType.SPECIAL || _attackType == AttackType.INVINCIBLE)) {
            Debug.Log("KO!");
            if(_actionCountdown <= 1) {
                SceneManager.LoadScene("SampleScene");

            }
        }
    }
}
