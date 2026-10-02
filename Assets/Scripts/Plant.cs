using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour
{
    private PlantData _data;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Vector2Int _position;
    
    public GameObject projectilePrefab;
    
    private float _hp;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Init(PlantData data, Vector2Int position)
    {
        _data = data;
        _position = position;
        
        _hp = data.maxHealth;
        
        _spriteRenderer.sprite = _data.sprite;
        if (_spriteRenderer.sprite != null)
        {
            _boxCollider.size = _spriteRenderer.sprite.bounds.size;
            _boxCollider.offset = _spriteRenderer.sprite.bounds.center;
        }
        GameGrid.Instance.Plants[_position.x, _position.y] = this;
        
        if (data.role == PlantRole.Shooter)
            StartCoroutine(RunProjectileSpawner());
        else if (data.role == PlantRole.Sunflower)
            StartCoroutine(RunIncomeGenerator());
    }
    
    IEnumerator RunIncomeGenerator()
    {
        while (true)
        {
            if (GameState.Instance.Finished)
                break;
            
            yield return new WaitForSeconds(_data.actionCooldown);

            GameState.Instance.playerCurrency += 50;
        }
    }
    IEnumerator RunProjectileSpawner()
    {
        while (true)
        {
            if (GameState.Instance.Finished)
                break;
            
            yield return new WaitForSeconds(_data.actionCooldown);
                
            SpawnProjectile();
        }
    }
    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
    
    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            GameGrid.Instance.Plants[_position.x, _position.y] = null;
            
            Destroy(gameObject);
        } 
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
