using System.Collections;
using UnityEngine;
using CodeBase.Managers;

namespace CodeBase.Components
{
    public class MovementComponent : MonoBehaviour
    {
        private const float _kDistance = .05f;
        [SerializeField] private Transform _movePoint;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _tileZone;
        [SerializeField] private LayerMask _obstacleZone;

        private bool _isDown = false;
        void Update()
        {
            DownCheck();
            DistanceCheck();
        }
        private void DistanceCheck()
        {
            if (Vector3.Distance(transform.position, _movePoint.position) <= _kDistance)
            {
                if (Input.anyKey)
                {
                    MovePointMovement();
                }
            }
        }
        private void DownCheck()
        {
            if (!_isDown)
                SmoothMoveCharacter();
            else
                FallDownEffect();
        }
        private void SmoothMoveCharacter()
        {
            transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _speed * Time.deltaTime);
        }
        private void FallDownEffect()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, _speed * Time.deltaTime);
        }
        private void MovePointMovement()
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, _obstacleZone))
                {
                    _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    Debug.Log("Horizontal Moving");
                }
                else
                    Debug.Log("Not moving, tile is occupied");
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, _obstacleZone))
                {
                    _movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    Debug.Log(" Vertical Moving");
                }
                else
                    Debug.Log("Not moving, tile is occupied");
            }
            StartCoroutine(PhysicsCheck());
        }
        private IEnumerator PhysicsCheck()
        {
            if (Physics2D.OverlapCircle(_movePoint.position, .2f, _tileZone))
                yield break;
            else
            {
                Debug.LogError("No tile to move, character falls");
                yield return new WaitForSeconds(.25f);
                _isDown = true;
                yield return new WaitForSeconds(.5f);
                UIManager.Instance.RestartButton();
            }
        }
    }
}
