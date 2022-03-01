using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 30;

    public bool TakeHit()
    {
        healthPoints -= 10;
        bool isDead = healthPoints <= 0;
        if (isDead) Die();
        return isDead;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
