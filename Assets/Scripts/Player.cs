using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private AudioClip _moveClip, _loseClip;

    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab;

    private const string playerTokenString = "PlayerToken";

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlaySound(_moveClip);
            _rotateSpeed *= -1f;
        }    
    }

    private void Start()
    {
        UsePowerUp();
    }

    private void UsePowerUp()
    {
        if (PlayerPrefs.HasKey(playerTokenString))
        {
            int currentTokens = PlayerPrefs.GetInt(playerTokenString);
            if (currentTokens > 0)
            {
                currentTokens--;
                PlayerPrefs.SetInt(playerTokenString, currentTokens);
                PlayerPrefs.Save();
                currentTokens = PlayerPrefs.GetInt(playerTokenString);
                Debug.Log("PlayerToken reduced by 1. Remaining: " + currentTokens);
                _rotateSpeed *= 1.5f;
            }
            else
            {
                Debug.Log("PlayerToken value is 0. Cannot deduct further.");
                return;
            }
        }
        else
        {
            Debug.LogWarning("playerTokenString does not exist in PlayerPrefs!");
            return;
        }
    }


    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(_explosionPrefab, transform.GetChild(0).position, Quaternion.identity);
            SoundManager.Instance.PlaySound(_loseClip);
            _gm.GameEnded();
            Destroy(gameObject);
        }
    }
}
