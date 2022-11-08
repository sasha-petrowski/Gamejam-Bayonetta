using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Character : MonoBehaviour
{
    public static Character Instance;
    
    [Header("Refs")]

    [SerializeField]
    private GameObject _foot;
    [SerializeField]
    private Weapon[] _weaponList;


    [Header("Leg")]

    [SerializeField]
    private int _maxLegAngle;
    [SerializeField] 
    private Vector3 _footAngleVector;
    [SerializeField]
    private float _legMinLenght;

    [SerializeField]
    [Min(0)]
    private float _legMaxLenght;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    private int _maxHealth;

    [SerializeField]
    [Min(0)]
    private float _horizontalSpeed;

    [SerializeField]
    [Min(0)]
    private float _movementAcceleration;
    /*
    [SerializeField]
    [Min(0)]
    private float _movementDrag;
    */
    [Header("Jump")]

    [SerializeField]
    [Min(0)]
    private float _jumpHeight;

    [SerializeField]
    [Min(0)]
    private float _jumpTime;

    [SerializeField]
    private AnimationCurve _jumpCurve;

    [Header("Dash")]

    [SerializeField]
    [Min(0)]
    private float _dashSpeed;

    [SerializeField]
    [Min(0)]
    private float _dashCooldown;

    [SerializeField]
    [Min(0)]
    private float _dashTime;

    [SerializeField]
    private AnimationCurve _dashCurve;

    [HideInInspector]
    public Weapon currentWeapon;
    public int currentWeaponIndex;

    [HideInInspector]
    public Vector3 legDirection;
    private Vector3 _maxLegDirection;

    private bool _isBlockedByCamera;
    private float _cameraLimitLeft;
    private float _cameraLimitRight;

    private float _legDistance;
    private float _timeAtJumpStart;
    private float _timeAtDashStart;
    private float _timeAtAttackStart;
    private float _attackCooldown;

    private float _dashDirection;
    private bool _isJumping = false;
    private bool _isAttacking = false;
    private bool _isLegUp = false;
    private float _direction;

    private int _health;

    private Plane _raycastPlane;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _weaponList.Length; i++)
        {
            _weaponList[i].character = this;
        }

        currentWeapon = _weaponList[0];
        currentWeapon.OnSelect();

        _maxLegDirection = new Vector3(Mathf.Sin(_maxLegAngle * Mathf.Deg2Rad), -Mathf.Cos(_maxLegAngle * Mathf.Deg2Rad), 0);
        _legDistance = _legMaxLenght;

        _raycastPlane = new Plane(new Vector3(0, 0, 1), Vector3.zero);

        _health = _maxHealth;


        CameraBlock(false);
    }

    private void Update()
    {
        // Movement

        //Jump
        float jumpY = 0;
        if (_isJumping)
        {
            if ((Time.time - _timeAtJumpStart) / _jumpTime >= 1)
            {
                jumpY = _jumpCurve.Evaluate(1) * _jumpHeight;
                OnJumpEnd();
            }
            else
            {
                jumpY = _jumpCurve.Evaluate((Time.time - _timeAtJumpStart) / _jumpTime) * _jumpHeight;
            }
        } // Evaluate the height based on _timeAtJumpStart, call OnJumpEnd() if needed.

        float dashStep = 0;
        if (_dashDirection != 0)
        {
            if ((Time.time - _timeAtDashStart) / _dashTime >= 1)
            {
                dashStep = _dashDirection * _dashCurve.Evaluate(1) * _dashSpeed * Time.deltaTime;
                OnDashEnd();
            }
            else
            {
                dashStep = _dashDirection * _dashCurve.Evaluate((Time.time - _timeAtDashStart) / _dashTime) * _dashSpeed * Time.deltaTime;
            }
        } // Evaluate the dashStep based on _timeAtDashStart, call OnDashEnd() if needed.

        float moveStep = _direction * _horizontalSpeed * Time.deltaTime;

        if (_isBlockedByCamera)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + dashStep + moveStep, _cameraLimitLeft, _cameraLimitRight), jumpY, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x + dashStep + moveStep, jumpY, transform.position.z);
        //_direction = Mathf.MoveTowards(_direction, 0, Time.deltaTime * _movementDrag);

        // Update foot position
        Vector3    targetPosition = Vector3.zero;
        Quaternion targetRotation = Quaternion.identity;

        if (_isLegUp || _isAttacking)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;
            if (_raycastPlane.Raycast(ray, out enter))
            {
                legDirection = (ray.GetPoint(enter) - transform.position).normalized;

                if (legDirection.y > _maxLegDirection.y)
                {
                    legDirection.y = _maxLegDirection.y;
                    legDirection.x = _maxLegDirection.x * Mathf.Sign(legDirection.x);
                }

                targetPosition = legDirection * _legDistance + transform.position;

                targetRotation = Quaternion.FromToRotation(new Vector3(0, -1, 0), legDirection);
            }

        }
        else
        {
            targetPosition = transform.position;
            targetPosition.y -= _legMaxLenght;
        }
        _foot.transform.position = targetPosition;

        _foot.transform.rotation = targetRotation;
        _foot.transform.Rotate(_footAngleVector);
        //_foot.transform.rotation = Quaternion.RotateTowards(_foot.transform.rotation, targetRotation, 360 * Time.deltaTime);

    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log(_health);
        if(_health <= 0)
        {
            //dead
        }
    }


    public void CameraBlock(bool cameraBlock)
    {
        _isBlockedByCamera = cameraBlock;
        if (_isBlockedByCamera)
        {
            Ray ray;
            float enter = 0.0f;

            ray = Camera.main.ScreenPointToRay(new Vector3(0, 0, 0));
            if (_raycastPlane.Raycast(ray, out enter))
            {
                _cameraLimitLeft = ray.GetPoint(enter).x;
            }
            
            ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth, 0, 0));
            if (_raycastPlane.Raycast(ray, out enter))
            {
                _cameraLimitRight = ray.GetPoint(enter).x;
            }
        }
    }

    public void NextWeapon()
    {
        ChangeWeapon(1);
    }
    public void PreviousWeapon()
    {
        ChangeWeapon(_weaponList.Length - 1);
    }
    private void ChangeWeapon(int step) 
    {
        currentWeapon.OnQuit();
        currentWeaponIndex = (currentWeaponIndex + step) % _weaponList.Length;
        currentWeapon = _weaponList[currentWeaponIndex];
        currentWeapon.OnSelect();
    }


    public void GrabLeg()
    {
        _isLegUp = true;
    }
    public void HoldLeg()
    {
    }
    public void DropLeg()
    {
        _isLegUp = false;
    }

    public void DoRetractLeg(float time, TweenCallback onComplete)
    {
        DOTween.To(() => _legDistance, x => _legDistance = x, _legMinLenght, (_legDistance - _legMinLenght) * time).onComplete = onComplete;
    }
    public void DoExtendLeg(float time, TweenCallback onComplete)
    {
        DOTween.To(() => _legDistance, x => _legDistance = x, _legMaxLenght, (_legMaxLenght - _legDistance) * time).onComplete = onComplete;
    }

    public void AttackStart()
    {
        if (_isLegUp && Time.time >= _timeAtAttackStart + _attackCooldown)
        {
            _timeAtAttackStart = Time.time;
            _isAttacking = true;
            _attackCooldown = currentWeapon.Attack();
        }
    }
    public void OnAttackEnd()
    {
        _isAttacking = false;
    }

    public void JumpStart()
    {
        if (! _isJumping)
        {
            _isJumping = true;
            _timeAtJumpStart = Time.time;
        }
    }
    private void OnJumpEnd()
    {
        _isJumping = false;
    }

    public void DashStart(float direction)
    {
        if (_isJumping == false && _dashDirection == 0 && _timeAtDashStart + _dashTime + _dashCooldown <= Time.time)
        {
            _dashDirection = direction;
            _timeAtDashStart = Time.time;
        }
    }
    private void OnDashEnd()
    {
        _dashDirection = 0;
    }

    public void HorizontalMovement(float direction)
    {
        if (_isLegUp == false || _isJumping == true || direction == 0)
        {
            _direction = Mathf.MoveTowards(_direction, direction, Time.deltaTime * _movementAcceleration);
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (_raycastPlane.Raycast(ray, out enter))
        {
            Vector3 point = ray.GetPoint(enter) - transform.position;
            Gizmos.DrawLine(transform.position, point + transform.position);
        }
    }
}
