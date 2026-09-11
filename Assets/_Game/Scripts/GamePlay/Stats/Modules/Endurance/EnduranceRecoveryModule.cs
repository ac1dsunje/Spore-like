using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules.Endurance
{
public class EnduranceRecoveryModule : StatModule
{
    public float RecoveryRate { get; private set; }
        
    public bool IsRecovering => _users.Count == 0;
        
    private readonly HashSet<IEnduranceUser> _users = new();

    protected override void Configure()
    {
        BindStat(StatType.EnduranceRecovery, UpdateRecoveryRate);
    }

    public void AddUser(IEnduranceUser user) => _users.Add(user);
    public void RemoveUser(IEnduranceUser user) => _users.Remove(user);
        
    private void UpdateRecoveryRate(float value) => RecoveryRate = value;
}
}