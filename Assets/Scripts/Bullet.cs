using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 _targetPosition;
    float _endDistance = 0;
    public Vector3 targetPosition
    {
        set
        {
            _targetPosition = value;
            _endDistance = Vector2.Distance(this.transform.position, _targetPosition) * 0.01f;
        }
        get { return _targetPosition; }
    }

    void Start()
    {

    }

    // public bool shouldDestroyBulletOnBotCollision(Bot bot) {
    //     return true;
    // }

    // Update is called once per frame
    void Update()
    {
        if (_targetPosition != null)
        {
            float currentDistance = Vector2.Distance(this.transform.position, _targetPosition);
            if (currentDistance > _endDistance)
            {
                this.transform.position = Vector2.Lerp(
                                this.transform.position,
                                _targetPosition,
                                Time.fixedDeltaTime
                            );
            } else {
                // Debug.Log($"Destroy");
                Destroy(this.gameObject);
            }
        }
    }
}
