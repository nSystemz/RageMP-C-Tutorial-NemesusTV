using System;
using RAGE;

namespace Freeze
{
    public class Freeze : Events.Script
    {
        public Freeze()
        {
            Events.Add("PlayerFreeze", PlayerFreeze);
        }
        public void PlayerFreeze(object [] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition((bool)args[0]);
        }
    }
}
