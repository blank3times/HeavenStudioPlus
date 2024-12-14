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
                new GameAction("pancake", "Pancake")
                {
                    function = delegate { CocoaBar.instance.DoPancake(eventCaller.currentEntity.beat); },
                    defaultLength = 4.0f,
                },
                new GameAction("cocoa", "Hot Cocoa")
                {
                    function = delegate { CocoaBar.instance.DoCocoa(eventCaller.currentEntity.beat); },
                    defaultLength = 3.5f,
                }
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
        public static CocoaBar instance;

        const int IAAltDownCat = IAMAXCAT;
        const int IAAltUpCat = IAMAXCAT + 1;

        protected static bool IA_PadAltPress(out double dt)
        {
            return PlayerInput.GetPadDown(InputController.ActionsPad.South, out dt);
        }
        protected static bool IA_BatonAltPress(out double dt)
        {
            return PlayerInput.GetSqueezeDown(out dt);
        }
        
        protected static bool IA_PadAltRelease(out double dt)
        {
            return PlayerInput.GetPadUp(InputController.ActionsPad.South, out dt);
        }
        protected static bool IA_BatonAltRelease(out double dt)
        {
            return PlayerInput.GetSqueezeUp(out dt);
        }

        public static PlayerInput.InputAction InputAction_AltPress =
            new("PcoCocoaBarAltPress", new int[] { IAAltDownCat, IAAltDownCat, IAAltDownCat },
            IA_PadAltPress, IA_TouchBasicPress, IA_BatonAltPress);
        public static PlayerInput.InputAction InputAction_AltFlickRelease =
            new("PcoCocoaBarAltRelease", new int[] { IAAltUpCat, IAFlickCat, IAAltUpCat },
            IA_PadAltRelease, IA_TouchFlick, IA_BatonAltRelease);
        public static PlayerInput.InputAction InputAction_TouchRelease =
            new("PcoCocoaBarTouchRelease", new int[] { IAEmptyCat, IAReleaseCat, IAEmptyCat },
            IA_Empty, IA_TouchBasicRelease, IA_Empty);

        void Awake()
        {
            instance = this;
        }
        public void DoPancake(double beat)
        {
            //Pancake Multisound
            ScheduleInput(beat, 2.00f, InputAction_BasicPress, PlateSuccess, ServeMiss, Nothing);
            ScheduleInput(beat, 2.75f, InputAction_BasicPress, PlateSuccess, ServeMiss, Nothing);            
            ScheduleInput(beat, 3.50f, InputAction_BasicPress, PlateSuccess, ServeMiss, Nothing);

            MultiSound.Play(new MultiSound.Sound[] {
                new MultiSound.Sound("cocoaBar/ding_temp", beat),
                new MultiSound.Sound("cocoaBar/customer_temp", beat + 1.0f),
                new MultiSound.Sound("cocoaBar/customer_temp", beat + 1.5f)
            });
        }

        public void DoCocoa(double beat)
        {
            //Pancake Multisound
            ScheduleInput(beat, 1.50f, InputAction_AltPress, FillSuccess, ServeMiss, Nothing);
            ScheduleInput(beat, 2.50f, InputAction_AltFlickRelease, CupSuccess, ServeMiss, Nothing);            

            MultiSound.Play(new MultiSound.Sound[] {
                new MultiSound.Sound("cocoaBar/ding_temp", beat),
                new MultiSound.Sound("cocoaBar/customer_temp", beat + 0.5f),
                new MultiSound.Sound("cocoaBar/customer_temp", beat + 1.0f)
            });
        }

        public void PlateSuccess(PlayerActionEvent caller, float state)
        {
            if (state >= 1f || state <= -1f)
            {
                SoundByte.PlayOneShotGame("cocoaBar/whiff_temp");
                return;
            }
            SoundByte.PlayOneShotGame("cocoaBar/whoosh_temp");
        }

        public void FillSuccess(PlayerActionEvent caller, float state)
        {
            if (state >= 1f || state <= -1f)
            {
                SoundByte.PlayOneShotGame("cocoaBar/whiff_temp");
                return;
            }
            SoundByte.PlayOneShotGame("cocoaBar/marshmallows_temp");
        }

        public void CupSuccess(PlayerActionEvent caller, float state)
        {
            if (state >= 1f || state <= -1f)
            {
                SoundByte.PlayOneShotGame("cocoaBar/whiff_temp");
                return;
            }
            SoundByte.PlayOneShotGame("cocoaBar/whoosh_temp");
            SoundByte.PlayOneShotGame("cocoaBar/sparkle_temp");
        }

        public void ServeMiss(PlayerActionEvent caller)
        {
            SoundByte.PlayOneShotGame("cocoaBar/whiff_temp");
        }

        public void Nothing(PlayerActionEvent caller)
        {

        }
    }
}