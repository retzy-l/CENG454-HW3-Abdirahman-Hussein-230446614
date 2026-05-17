// DoubleDamageDecorator.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614

public class DoubleDamageDecorator : WeaponDecorator
{
    public DoubleDamageDecorator(IWeapon weapon) : base(weapon) { }

    public override float GetDamage() => wrapped.GetDamage() * 2f;
}