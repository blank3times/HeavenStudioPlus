using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HeavenStudio.Util;
using HeavenStudio.InputSystem;

using Jukebox;

namespace HeavenStudio.Games.Loaders
{
    using static Minigames;
    
    public static class PcoCocoaBarLoader
    {
        public static Minigame AddGame(EventCaller eventCaller)
        {
            return new Minigame("cocoaBar", "Cocoa Bar [Tentative]", "211571", false, false, new List<GameAction>()
            {
            }
            );
        }
    }
}

namespace HeavenStudio.Games
{
    /// This class handles the minigame logic.
    /// Minigame inherits directly from MonoBehaviour, and adds Heaven Studio specific methods to override.
    public class CocoaBar : Minigame
    {
        void Awake()
        {
            instance = this;
        }
    }
}