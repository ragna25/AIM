﻿using System;
using AIM.Autoplay.Modes;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Helpers;
using LeagueSharp;
using LeagueSharp.Common;
using AutoLevel = LeagueSharp.Common.AutoLevel;
using Orbwalking = AIM.Autoplay.Util.Orbwalking;

namespace AIM.Autoplay
{
    internal class Load
    {
        public static readonly bool ShouldUsePoroSnaxThisGame = Randoms.RandomDecision();
        public static int LoadedTime = 0;
        public Load()
        {
            Game.OnWndProc += OnWndProc;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
            Game.OnGameUpdate += OnGameUpdate;
        }

        public static void OnWndProc(EventArgs args)
        {
            //Draw AIMLoading.jpg
        }

        public void OnGameLoad(EventArgs args)
        {
            try
            {
                Utils.ClearConsole();

                LoadedTime = Environment.TickCount;
                Utility.DelayAction.Add(
                    Randoms.Rand.Next(1000, 30000), () =>
                    {
                        new Carry();
                        Console.WriteLine("Carry Init Success!");
                    });
                Utility.DelayAction.Add(
                    Randoms.Rand.Next(1000, 10000), () =>
                {
                    new AutoLevel(Util.Data.AutoLevel.GetSequence());
                    Console.WriteLine("AutoLevel Init Success!");
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Game.PrintChat("AIM {0} Successfuly Loaded, Enjoy!", Program.Version);
        }

        public static void OnGameUpdate(EventArgs args)
        {
            if (Utility.Map.GetMap().Type == Utility.Map.MapType.HowlingAbyss)
            {
                UsePorosnax();
            }
        }

        public static bool UsePorosnax()
        {
            var trinket = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Trinket);
            return trinket != null && trinket.IsReady() && ShouldUsePoroSnaxThisGame && ObjectManager.Player.Spellbook.CastSpell(SpellSlot.Trinket);
        }
    }
}