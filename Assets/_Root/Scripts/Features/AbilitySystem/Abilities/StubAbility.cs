namespace Features.AbilitySystem
{
    internal class StubAbility : IAbility
    {
        public static readonly IAbility Default = new StubAbility();
        
        public void Apply(IAbilityActivator activator){}
    }
}