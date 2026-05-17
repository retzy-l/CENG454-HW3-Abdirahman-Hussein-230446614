// WeaponDecorator.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614

// Base decorator — wraps any IWeapon
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrapped;

    public WeaponDecorator(IWeapon weapon)
    {
        wrapped = weapon;
    }

    public virtual void Fire() => wrapped.Fire();
    public virtual float GetDamage() => wrapped.GetDamage();
    public virtual float GetFireRate() => wrapped.GetFireRate();
}