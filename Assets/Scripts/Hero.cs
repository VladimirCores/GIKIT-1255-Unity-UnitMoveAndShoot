using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    public UnityEvent<GameObject, GameObject> eventCollision = new UnityEvent<GameObject, GameObject>();

    [Range(1, 20)]
    public int moveSpeed = 10;
    
    public int health = 10;
    Vector2 _targetPosition;
    float _disanceWhenMoveStops;

    public GameObject bullet;

    Vector3 _currentPositionOnScreen {
        get { return Camera.main.WorldToScreenPoint(this.transform.position); }   // get method
    }

    void Awake()
    {
        this.name = StringUtils.GenerateFullName();
    }

    void Start() {
        SetTargetPositionTo(_currentPositionOnScreen);
        Debug.Log($"> Hero -> _targetPosition = {_targetPosition}{this.transform.position}");
        Debug.Log($"> Hero -> _disanceWhenMoveStops = {_disanceWhenMoveStops}");
    }

    void Update()
    {
        Vector3 direction = Input.mousePosition - _currentPositionOnScreen;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0)) {
            SetTargetPositionTo(Input.mousePosition);
        }

        if (Input.GetKeyUp("space") && bullet != null) {
            var goBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
            goBullet.GetComponent<Bullet>().targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FixedUpdate() 
    {
        if (CheckNotReachTarget()) {
            var step = Time.fixedDeltaTime * moveSpeed;
            this.transform.position = Vector2.MoveTowards(
                this.transform.position, 
                _targetPosition, 
                step
            );
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log($"> Hero -> OnTriggerEnter2D: {collision.gameObject.name}");
        eventCollision.Invoke(collision.gameObject, this.gameObject);
    }

    bool CheckNotReachTarget() {
        var currentDistance = Vector2.Distance(this.transform.position, _targetPosition);
        return _disanceWhenMoveStops < currentDistance;
    }

    void SetTargetPositionTo(Vector2 position) {
        _targetPosition = Camera.main.ScreenToWorldPoint(position);
        _disanceWhenMoveStops = Vector2.Distance(this.transform.position, _targetPosition) * 0.01f;
    }
}
