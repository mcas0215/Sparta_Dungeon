using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}
// UI�� ������ �� �ִ� PlayerCondition
// �ܺο��� �ɷ�ġ ���� ����� �̰��� ���ؼ� ȣ��. ���������� UI ������Ʈ ����.
public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   // hunger�� 0�϶� ����� �� (value > 0)
    public event Action onTakeDamage;   // Damage ���� �� ȣ���� Action (6�� ������ ȿ�� �� ���)

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue < 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        Debug.Log("������");
        onTakeDamage?.Invoke();
    }
}