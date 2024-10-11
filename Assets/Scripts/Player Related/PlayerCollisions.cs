using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    LayerMask _groundLayerMask;
    private const float GroundRaycastDistance = 1.1f;
    private FadeTransition _fadeTransition;
    
    // Start is called before the first frame update
    void Start()
    {
        _groundLayerMask = LayerMask.GetMask("Ground");
        _fadeTransition = FindFirstObjectByType<FadeTransition>();
    }

    public bool GroundCollisionCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, GroundRaycastDistance, _groundLayerMask);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_fadeTransition != null)
            {
                StartCoroutine(OnCollisionWithEnemy());
                return;
            }
            
            Debug.LogWarning("Fade Transition was not found at start().");
        }
    }

    private IEnumerator OnCollisionWithEnemy()
    {
        _fadeTransition.FadeIn(1);
        yield return new WaitForSeconds(1);
        _fadeTransition.ReloadCurrentScene();
        _fadeTransition.FadeOut(1);
    }
}
