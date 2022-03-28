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

    private BoxCollider2D _hurtboxBody;
    private BoxCollider2D _hurtboxLimb;
    private BoxCollider2D _hitbox;

    private SpriteRenderer _hurtboxBodySprite;
    private SpriteRenderer _limbVerticalSprite;
    private SpriteRenderer _limbHorizontalSprite;


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
        if (_chargeCounter >= FULL_CHARGE_AMOUNT) {
            _isAttackCharged = true;
        }
    }

    public bool IsAttackCharged() { return _isAttackCharged; }

    public bool PerformingAttack() {
        return _attackCountdown > 0;
    }

    public void PerformLongPokeAttack() {
        _attackCountdown = LONG_POKE_ATTACK_DURATION;
        _attackType = AttackType.LONG;
        _limbHorizontalSprite.enabled = true;
        _limbHorizontalSprite.transform.localPosition = new Vector3(1.25f, -0.71f, 0);
    }

    public void PerformShortPokeAttack() {
        _attackCountdown = SHORT_POKE_ATTACK_DURATION;
        _attackType = AttackType.SHORT;
        _limbVerticalSprite.enabled = true;
    }

    public void PerformSpecialAttack() {
        _attackCountdown = SPECIAL_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.SPECIAL;
        _limbHorizontalSprite.enabled = true;
        _limbHorizontalSprite.transform.localPosition = new Vector3(1.25f, 0.477f, 0);
    }

    public void PerformInvincibleAttack() {
        _attackCountdown = INVINCIBLE_ATTACK_DURATION;
        _chargeCounter = 0;
        _isAttackCharged = false;
        _attackType = AttackType.INVINCIBLE;
        _limbVerticalSprite.enabled = true;
    }

    private void UpdateHurtboxes() {
        switch (_attackType) {
            case AttackType.SHORT: {
                int attackPhase = (SHORT_POKE_ATTACK_DURATION - _attackCountdown);
                if (attackPhase <= _shortAttackFrameData[0]) {
                    _hurtboxLimb.enabled = true;
                    _hurtboxLimb.size = new Vector2(1.4f, 1.75f);
                } else if (attackPhase > _shortAttackFrameData[0] && attackPhase <= _shortAttackFrameData[0] + _shortAttackFrameData[1] - 1) {
                    _hitbox.enabled = true;
                    _hitbox.size = new Vector2(1.4f, 1.75f);
                    _hitbox.offset = new Vector2(0.875f, -0.435f);

                    _limbVerticalSprite.color = Color.red;
                } else if (attackPhase > _shortAttackFrameData[1] && attackPhase <= _shortAttackFrameData[0] + _shortAttackFrameData[1] + _shortAttackFrameData[2]) {
                    _hitbox.enabled = false;
                    _hitbox.size = Vector2.zero;
                    _hitbox.offset = Vector2.zero;

                    _limbVerticalSprite.color = Color.white;
                }
                break; 
            }

            case AttackType.LONG: {
                int attackPhase = (LONG_POKE_ATTACK_DURATION - _attackCountdown);
                if (attackPhase <= _longAttackFrameData[0]) {
                    _hurtboxLimb.enabled = true;
                    _hurtboxLimb.size = new Vector2(2.0f, 0.75f);
                } else if(attackPhase > _longAttackFrameData[0] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] - 1) {
                    _hitbox.enabled = true;
                    _hitbox.size = new Vector2(2.0f, 0.75f);
                    _hitbox.offset = new Vector2(1.25f, -0.71f);

                    _limbHorizontalSprite.color = Color.red;
                } else if(attackPhase > _longAttackFrameData[1] && attackPhase <= _longAttackFrameData[0] + _longAttackFrameData[1] + _longAttackFrameData[2]) {
                    _hitbox.enabled= false;
                    _hitbox.size = Vector2.zero;
                    _hitbox.offset = Vector2.zero;

                    _limbHorizontalSprite.color = Color.white;
                }
                break; 
            }

            case AttackType.SPECIAL: {
                int attackPhase = (SPECIAL_ATTACK_DURATION - _attackCountdown);
                if (attackPhase <= _specialAttackFrameData[0]) {
                    _hurtboxLimb.enabled = true;
                    _hurtboxLimb.size = new Vector2(2.0f, 0.75f);
                } else if (attackPhase > _specialAttackFrameData[0] && attackPhase <= _specialAttackFrameData[0] + _specialAttackFrameData[1] - 1) {
                    _hitbox.enabled = true;
                    _hitbox.size = new Vector2(2.0f, 0.75f);
                    _hitbox.offset = new Vector2(1.25f, 0.75f);

                    _limbHorizontalSprite.color = Color.red;
                } else if (attackPhase > _specialAttackFrameData[1] && attackPhase <= _specialAttackFrameData[0] + _specialAttackFrameData[1] + _specialAttackFrameData[2]) {
                    Debug.Log("here");
                    _hitbox.enabled = false;
                    _hitbox.size = Vector2.zero;
                    _hitbox.offset = Vector2.zero;

                    _limbHorizontalSprite.color = Color.white;
                }
                break;
            }

            case AttackType.INVINCIBLE: {
                int attackPhase = (INVINCIBLE_ATTACK_DURATION - _attackCountdown);
                if (attackPhase <= _invincibleAttackFrameData[0]) {
                    _hurtboxBody.enabled = false;
                    _hurtboxBodySprite.enabled = false;
                } else if (attackPhase > _invincibleAttackFrameData[0] && attackPhase <= _invincibleAttackFrameData[0] + _invincibleAttackFrameData[1] - 1) {
                    _hurtboxBody.enabled = true;
                    _hurtboxBodySprite.enabled = true;
                    _hitbox.enabled = true;
                    _hitbox.size = new Vector2(1.4f, 1.75f);
                    _hitbox.offset = new Vector2(0.875f, -0.435f);

                    _limbVerticalSprite.color = Color.red;
                } else if (attackPhase > _invincibleAttackFrameData[1] && attackPhase <= _invincibleAttackFrameData[0] + _invincibleAttackFrameData[1] + _invincibleAttackFrameData[2]) {
                    _hitbox.enabled = false;
                    _hitbox.size = Vector2.zero;
                    _hitbox.offset = Vector2.zero;

                    _limbVerticalSprite.color = Color.white;
                }
                break;
            }

            case AttackType.DEMON: { break; }
        }
    }

    private void FixedUpdate() {
        if(PerformingAttack()) {
            UpdateHurtboxes();
            _attackCountdown--;
            if (_attackCountdown == 0) {
                _attackType = AttackType.NONE;
                _hurtboxLimb.enabled = false;
                if(_limbVerticalSprite.enabled) {
                    _limbVerticalSprite.enabled = false;
                } 
                if(_limbHorizontalSprite.enabled) {
                    _limbHorizontalSprite.enabled = false;
                }
            } else if (_attackCountdown < 0) {
                _attackCountdown = 0;
            }
        }
    }

    private void Awake() {
        _hurtboxBody = gameObject.transform.Find("Hurtboxes/HurtboxBody").GetComponent<BoxCollider2D>();
        
        _hurtboxLimb = gameObject.transform.Find("Hurtboxes/HurtboxLimbVertical").GetComponent<BoxCollider2D>();
        _hurtboxLimb.enabled = false;

        _hitbox = gameObject.transform.Find("Hitbox").GetComponent<BoxCollider2D>();
        _hitbox.enabled = false;

        _hurtboxBodySprite = gameObject.transform.Find("Hurtboxes/HurtboxBody").GetComponent<SpriteRenderer>();

        _limbVerticalSprite = gameObject.transform.Find("Hurtboxes/HurtboxLimbVertical").GetComponent<SpriteRenderer>();
        _limbVerticalSprite.enabled = false;

        _limbHorizontalSprite = gameObject.transform.Find("Hurtboxes/HurtboxLimbHorizontal").GetComponent<SpriteRenderer>();
        _limbHorizontalSprite.enabled = false;

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
