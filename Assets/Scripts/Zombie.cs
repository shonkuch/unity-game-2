using UnityEngine;

public class Zombie : MonoBehaviour
{
    public LayerMask plantLayer;
    
    private ZombieData _data;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private float _hp;
    private float _attackCooldown;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Init(ZombieData data)
    {
        _data = data;

        _hp = data.maxHealth;
        
        _spriteRenderer.sprite = _data.sprite;
        if (_spriteRenderer.sprite != null)
        {
            _boxCollider.size = _spriteRenderer.sprite.bounds.size;
            _boxCollider.offset = _spriteRenderer.sprite.bounds.center;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Destroy(gameObject);
        } 
    }

    void Update()
    {
        if (GameState.Instance.Finished)
            return;

        _attackCooldown += Time.deltaTime;
        
        const float speedModifier = 0.05f;
        var oneTickMoveDelta = Vector3.left * (Time.deltaTime * _data.baseSpeed * speedModifier);
        
        Vector2 origin = _boxCollider.bounds.center;
        Vector2 size = _boxCollider.bounds.size;
        size.y /= 10;

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector3.left, 
            size.x / 8, plantLayer);

        if (hit)
        {
            Plant plant = hit.transform.GetComponent<Plant>();
            
            Attack(plant);
        }
        else
        {
            transform.position += Vector3.left * (Time.deltaTime * _data.baseSpeed * speedModifier);

            if (transform.position.x < GameGrid.Instance.GetGlobalPosition(new Vector2Int(0, 0)).x - 1)
            {
                GameState.Instance.Lose();
            }
        }
    }

    void Attack(Plant plant)
    {
        if (_attackCooldown >= _data.attackCooldown)
        {
            plant.TakeDamage(_data.damage);
            _attackCooldown = 0;
        }
    }
}
