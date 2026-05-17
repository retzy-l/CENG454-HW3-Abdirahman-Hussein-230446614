// RapidFireDecorator.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614

public class RapidFireDecorator : WeaponDecorator
{
    private float fireRateMultiplier = 2f;

    public RapidFireDecorator(IWeapon weapon) : base(weapon) { }

    public override float GetFireRate() => wrapped.GetFireRate() * fireRateMultiplier;
}