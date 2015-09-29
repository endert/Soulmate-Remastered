using Soulmate_Remastered.Core;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder
{
    class Cheats
    {
        /// <summary>
        /// String for Console Output if the parameter were invalid
        /// </summary>
        static String invalidInput = " is an invalid parameter for ";

        /// <summary>
        /// Cheat names
        /// </summary>
        public enum Cheat
        {
            UNKOWN = -1,

            //Player affects without considereing any stats
            Heal = 0,
            HealFor = 1,
            TakeDamage = 2,

            //Set Player Stats
            SetHp = 3,
            SetMoney = 4,
            SetExp = 5,
            SetLvl = 6,
            SetDef = 7,
            SetAtt = 8,
            SetFusionValue = 9,
           
            //Other
            Help = 10,

            CheatCount
        }

        /// <summary>
        /// the Cheat Syntax
        /// </summary>
        public static string CheatSyntax
        {
            get
            {
                return
                
                    '"' + "/Heal" + '"' + " heals the Player completely.\n"+
                    '"' + "/HealFor value" + '"' + " heals the player for the given value the Player.\n"+
                    '"' + "/TakeDamage value" + '"' + " the Player takes the value as true damage.\n"+
                    '"' + "/SetHp value" + '"' + " sets the Hp of the Player.\n"+
                    '"' + "/SetMoney value" + '"' + " sets the Gold of the Player.\n"+
                    '"' + "/SetExp value" + '"' + " sets the exp of the Player.\n"+
                    '"' + "/SetLvl value" + '"' + " sets the Lvl of the Player.\n"+
                    '"' + "/SetDef value" + '"' + " sets the defense of the Player.\n"+
                    '"' + "/SetAtt value" + '"' + " sets the attack damage of the Player.\n"+
                    '"' + "/SetFusionValue value" + '"' + " sets the FusionValue of the Player.\n"+
                    '"' + "/Help" + '"' + " Opens a window with a List of all Cheats and their Syntax.\n"
                ;
            }
        }

        /// <summary>
        /// activate the entered Cheat should only be called once, when return is pressed
        /// </summary>
        /// <param name="input">String that contains: /Cheat parameter </param>
        public static void ActivateCheat(string input)
        {
            string[] inputSplit = input.Split();    //seperate /Cheat and all other parameter
            Cheat selectedCheat = Cheat.UNKOWN; //not defined cheat
            string[] parameter = new string[inputSplit.Length - 1]; //String array without /Cheat
            for (int i = 0; i < parameter.Length; i++)
            {
                parameter[i] = inputSplit[i + 1];
            }
            Params parms = new Params(parameter);

            //find the entered Cheat
            for (int i = 0; i < (int)Cheat.CheatCount; i++)
            {
                if (("/" + ((Cheat)i).ToString()).Equals(inputSplit[0]))
                {
                    selectedCheat = (Cheat)i;
                    break;
                }
            }

            _ActivateCheat(parms, selectedCheat);
        }

        /// <summary>
        /// should only be called once in activate Cheat. it is the actual activation of the given Cheat and the given parameter
        /// </summary>
        /// <param name="parms">Parameter such as float values etc.</param>
        /// <param name="ch">the entered Cheat</param>
        static void _ActivateCheat(Params parms, Cheat ch)
        {
            try
            {
                switch (ch)
                {
                    case Cheat.Heal:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.Heal);
                        break;
                    case Cheat.HealFor:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.HealFor);
                        break;
                    case Cheat.SetAtt:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetAtt);
                        break;
                    case Cheat.SetDef:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetDef);
                        break;
                    case Cheat.SetExp:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetExp);
                        break;
                    case Cheat.SetFusionValue:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetFusionValue);
                        break;
                    case Cheat.SetHp:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetHp);
                        break;
                    case Cheat.SetLvl:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetLvl);
                        break;
                    case Cheat.SetMoney:
                        PlayerHandler.Player.ActivateCheat(parms, PlayerHandler.Player.SetMoney);
                        break;
                    case Cheat.TakeDamage:
                        PlayerHandler.Player.ActivateCheat(-parms, PlayerHandler.Player.HealFor);
                        break;
                    case Cheat.Help:
                        CheatConsoleThreadStart.CheatConsole.ActivateCheat(parms, CheatConsoleThreadStart.CheatConsole.ShowCheats);
                        break;
                    default:
                        Console.WriteLine("Error, " + ch + " not implemented");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine(parms + invalidInput + ch);
            }
        }
    }
}
