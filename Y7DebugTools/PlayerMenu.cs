﻿using System;
using DragonEngineLibrary;
using ImGuiNET;


namespace Y7DebugTools
{
    public static class PlayerMenu
    {
        private static int m_chosenPlayer = 0;
        private static int m_setJobLevel = 1;
        private static int m_setLevel = 1;
        private static int m_setJob = 0;


        private static string[] m_enum_names_PlayerID;
        private static string[] m_enum_names_RPGJob;

        static PlayerMenu()
        {
            m_enum_names_PlayerID = Enum.GetNames(typeof(Player.ID));
            m_enum_names_RPGJob = Enum.GetNames(typeof(RPGJobID));
        }

        private static void Reload()
        {
            m_setLevel = (int)Player.GetLevel((Player.ID)m_chosenPlayer);
            m_setJobLevel = (int)Player.GetJobLevel((Player.ID)m_chosenPlayer);
            m_setJob = (int)Player.GetCurrentJob((Player.ID)m_chosenPlayer);
        }

        public static void Draw()
        {
            Character player = DragonEngine.GetHumanPlayer();

            if (ImGui.Begin("Player"))
            {
                if (ImGui.CollapsingHeader("Player Character"))
                    CharaRender.Draw(player);

                if (ImGui.CollapsingHeader("Player Stats Edit"))
                {
                    if (ImGui.Combo("Player:", ref m_chosenPlayer, m_enum_names_PlayerID, m_enum_names_PlayerID.Length))
                    {
                        Reload();
                        return;
                    }
                    else
                    {
                        if (ImGui.InputInt("Level", ref m_setLevel, 1, 1))
                        {
                            if (m_setLevel < 1)
                                m_setLevel = 1;
                            else
                                if (m_setLevel > 99)
                                m_setLevel = 99;

                            Player.SetLevel((uint)m_setLevel, (Player.ID)m_chosenPlayer, IntPtr.Zero);
                        }

                        if (ImGui.InputInt("Job Level", ref m_setJobLevel, 1, 1))
                        {
                            if (m_setJobLevel < 1)
                                m_setJobLevel = 1;
                            else
                                if (m_setJobLevel > 99)
                                m_setJobLevel = 99;

                            Player.SetJobLevel((uint)m_setJobLevel, (Player.ID)m_chosenPlayer);
                        }


                        if(ImGui.Combo("Job:", ref m_setJob, m_enum_names_RPGJob, m_enum_names_RPGJob.Length))
                        {
                            DragonEngine.Log("Setting job for " + (Player.ID)m_chosenPlayer + " to" + (RPGJobID)m_setJob);
                            Player.SetCurrentJob((Player.ID)m_chosenPlayer, (RPGJobID)m_setJob);
                            Reload();

                            return;
                        }
                    }
                }

                ImGui.End();
            }
        }
    }
}
