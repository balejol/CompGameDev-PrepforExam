public interface IDamageable
{
    void TakeDamage(int damage, bool applyBleed = false);
    void TakeBleedDamage(int damage);
}
