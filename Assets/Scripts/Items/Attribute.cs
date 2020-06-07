using System;

public class Attribute
{
    public float hp;
    public float attack;

    public Attribute(float hp, float attack)
    {
        this.hp = hp;
        this.attack = attack;
    }

    public Attribute Copy()
    {
        return new Attribute(hp, attack);
    }

    public string GetDescription()
    {
        return string.Format("<b>hp:</b> {0}\n<b>attack:</b> {1}", hp, attack);
    }
}