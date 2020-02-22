using System;

public interface IModifier
{
    Attribute UpdateAttribute(Attribute attribute);
}

public class CustomModifier : IModifier
{
    private Func<Attribute, Attribute> updater;

    public CustomModifier(Func<Attribute, Attribute> updater)
    {
        this.updater = updater;
    }

    public Attribute UpdateAttribute(Attribute attribute)
    {
        return updater(attribute);
    }
}

public class AddValueModifier : IModifier
{
    private float hp;
    private float attack;

    public AddValueModifier(float hp = 0, float attack = 0)
    {
        this.hp = hp;
        this.attack = attack;
    }

    public Attribute UpdateAttribute(Attribute attribute)
    {
        Attribute newAtrribute = attribute.Copy();
        newAtrribute.hp += hp;
        newAtrribute.attack += attack;
        return newAtrribute;
    }
}