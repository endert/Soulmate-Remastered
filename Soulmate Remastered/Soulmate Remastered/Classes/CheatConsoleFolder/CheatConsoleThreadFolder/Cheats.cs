using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder
{
    class Cheats
    {
        static String invalidInput = " is an invalid parameter for ";

        static String[] cheatNames = new String[]
        {
            "/SETHP",
            "/SETMONEY",
            "/HEAL",
            "/SETEXP",
            "/SETLVL",
            "/SETDEF",
            "/SETATT",
            "/SETFUSIONVALUE"
        };

        public static void setHp(String parameter)
        {
            try
            {
                PlayerHandler.player.setHp(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[0]);
            }
        }

        public static void setMoney(String parameter)
        {
            try
            {
                PlayerHandler.player.setMoney(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[1]);
            }
        }

        public static void heal()
        {
            PlayerHandler.player.heal();
        }

        public static void setExp(String parameter)
        {
            try
            {
                PlayerHandler.player.setExp(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[3]);
            }
        }

        public static void setLvl(String parameter)
        {
            try
            {
                PlayerHandler.player.setLvl(Convert.ToInt32(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[4]);
            }
        }

        public static void setDef(String parameter)
        {
            try
            {
                PlayerHandler.player.setDef(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[5]);
            }
        }

        public static void setAtt(String parameter)
        {
            try
            {
                PlayerHandler.player.setAtt(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[6]);
            }
        }

        public static void setFusionValue(String parameter)
        {
            try
            {
                PlayerHandler.player.setFusionValue(Convert.ToSingle(parameter));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine(parameter + invalidInput + cheatNames[7]);
            }
        }

        public static void activateCheat(String input)
        {
            String[] inputSplit = input.Split();
            int selectedCheat = -1;

            for (int i = 0; i < cheatNames.Length; i++)
            {
                if (cheatNames[i].Equals(inputSplit[0]))
                {
                    selectedCheat = i;
                    break;
                }
            }

            switch (selectedCheat)
            {
                case 0:
                    setHp(inputSplit[1]);
                    break;
                case 1:
                    setMoney(inputSplit[1]);
                    break;
                case 2:
                    heal();
                    break;
                case 3:
                    setExp(inputSplit[1]);
                    break;
                case 4:
                    setLvl(inputSplit[1]);
                    break;
                case 5:
                    setDef(inputSplit[1]);
                    break;
                case 6:
                    setAtt(inputSplit[1]);
                    break;
                case 7:
                    setFusionValue(inputSplit[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
