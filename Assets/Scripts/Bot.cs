using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bot : MonoBehaviour
{
    public UnityEvent<GameObject, GameObject> eventCollision = new UnityEvent<GameObject, GameObject>();

    const float DISPERSION = 15;
    [HideInInspector]
    public Transform followTarget;
    public float damage;
    bool hasFollowTarget
    {
        get { return followTarget != null; }
    }

    Color _color;
    Vector2 _targetPosition;
    Vector3 _positionOffset = Vector3.zero;
    float _disanceWhenGenerateNextTargetPosition;
    // Start is called before the first frame update
    void Awake()
    {
        _color = this.GetComponent<SpriteRenderer>().color;
        this.name = StringUtils.GenerateFullName();
        if (hasFollowTarget)
        {
            this.transform.position = Main.GetRandomScreenPosition();
            GenerateRandomOffsetForFollowerMove();
        }
        GenerateNextTargetPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasFollowTarget)
        {
            GenerateNextTargetPosition();
        }
        else
        {
            if (CheckTargetPositonReached())
            {
                GenerateNextTargetPosition();
            }
        }

        this.transform.position = Vector2.Lerp(this.transform.position, _targetPosition, Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log($"> Bot -> OnTriggerEnter2D: {collision.gameObject.name}");
        eventCollision.Invoke(collision.gameObject, this.gameObject);
    }

    void OnMouseOver()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = _color;
    }
    bool CheckTargetPositonReached()
    {
        float currentDistance = Vector2.Distance(this.transform.position, _targetPosition);
        return currentDistance < _disanceWhenGenerateNextTargetPosition;
    }

    void GenerateNextTargetPosition()
    {
        if (hasFollowTarget)
        {
            _targetPosition = followTarget.position + _positionOffset;
        }
        else
        {
            _targetPosition = Main.GetRandomScreenPosition();
        }

        _disanceWhenGenerateNextTargetPosition = Vector2.Distance(
            this.transform.position, _targetPosition) * 0.01f;
    }

    void GenerateRandomOffsetForFollowerMove()
    {
        var rnd = new System.Random();
        float angle = (float)rnd.NextDouble() * Mathf.PI * 2;
        float dispersion = ((float)rnd.NextDouble() + 0.1f) * (DISPERSION - 0.1f);
        _positionOffset.x = Mathf.Cos(angle) * dispersion;
        _positionOffset.y = Mathf.Sin(angle) * dispersion;
        _positionOffset = (Vector2)Camera.main.ScreenToWorldPoint(Vector2.zero) * _positionOffset / 100;
    }
}
