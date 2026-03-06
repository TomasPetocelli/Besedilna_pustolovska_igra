namespace Besedilna_pustolovska_igra {
    class Program {

        //--------------------DEBUG MODE--------------------
        static bool debugMode = true;

        //--------------------GLOBAL STATS--------------------
        static int morality = 0;
        static int ambition = 0;
        static int playerPower = 5;

        static bool villagersSaved = false;
        static bool shadowMarked = false;
        static bool entityBlessing = false;

        static int knightTrust = 0;
        static bool knightAlive = true;

        static Random rng = new Random();

        static void Main() {
            Intro();
            DebugStats();

            VillageScene();
            DebugStats();

            CrossroadScene();
            DebugStats();

            FortressScene();
            DebugStats();

            RuinsScene();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }//Konec Main

        //--------------------INTRO--------------------
        static void Intro() {
            TypeText("The kingdom of Eryndor stands on the brink of collapse.");
            TypeText("For centuries the Heart of Shadow slept beneath forgotten ruins.");
            TypeText("Now it has awakened.");
            TypeText("Its power can save the world... or destroy it.");
            TypeText("Kings, knights and scholars fear what may happen.");
            TypeText("Yet you are the one who chose to depart.");
        }//Konec Intro

        //--------------------VILLAGE--------------------
        static void VillageScene() {
            TypeText("\nYou arrive near a village consumed by flames.");
            TypeText("Screams echo through the night as raiders flee with their spoils.");

            Console.WriteLine("\n1) Save the villagers");
            Console.WriteLine("2) Ignore them");
            Console.WriteLine("3) Plunder");

            int choice = GetChoice(3);

            if (choice == 1) {
                villagersSaved = true;
                morality += 2;
                playerPower += 1;

                TypeText("You rush into the burning streets.");
                TypeText("Through smoke and fire you guide families to safety.");

            } else if (choice == 2) {
                ambition++;
                TypeText("You watch silently.");
                TypeText("Your destiny lies elsewhere.");

            } else {
                morality -= 2;
                playerPower += 2;
                shadowMarked = true;

                TypeText("While chaos reigns, you gather what power you can.");
                TypeText("The darkness seems pleased.");

            }//Konec if-else
            KnightFirstMeeting();

        }//Konec VillageScene

        //--------------------KNIGHT--------------------
        static void KnightFirstMeeting() {
            TypeText("\nFrom the smoke emerges a Royal Knight.");
            TypeText("His armor is scarred by battle, but his eyes are sharp.");

            if (morality > 0) {
                knightTrust += 2;
                TypeText("He studies you and nods with respect.");

            } else if (morality < 0) {
                knightTrust -= 2;
                TypeText("His gaze is cold and distrustful.");

            }//Konec if-else

            Console.WriteLine("\n1) Offer alliance");
            Console.WriteLine("2) Remain neutral");
            Console.WriteLine("3) Provoke him");

            int choice = GetChoice(3);

            if (choice == 1) {
                knightTrust++;
                morality++;

                TypeText("You propose an alliance.");
                TypeText("The Knight accepts cautiously.");

            } else if (choice == 3) {
                knightTrust -= 2;
                morality--;

                TypeText("Your words anger him.");
                TypeText("He will remember this.");

            }//Konec if-else
        }//Konec KnightFirstMeeting

        //--------------------CROSSROAD--------------------
        static void CrossroadScene() {
            TypeText("\nDays later you reach a crossroads.");
            TypeText("Three ancient paths stretch into the unknown.");

            Console.WriteLine("\n1) Monks' Temple");
            Console.WriteLine("2) Stone Bridge");
            Console.WriteLine("3) Whispering Forest");

            int choice = GetChoice(3);

            if (choice == 1) TempleScene();
            else if (choice == 2) BridgeScene();
            else ForestScene();

        }//konec CrossroadScene

        //--------------------TEMPLE--------------------
        static void TempleScene() {
            TypeText("\nYou climb the steps of the Monks' Temple.");
            TypeText("Ancient bells ring softly in the wind.");

            if (morality >= 0) {
                morality++;
                playerPower += 2;

                TypeText("The monks recognize a noble spirit.");
                TypeText("They grant you a sacred blessing.");

            } else {
                playerPower -= 1;

                TypeText("The sacred light rejects your presence.");
                TypeText("You leave under their silent judgment.");

            }//Konec if-else
        }//Konec TempleScene

        //--------------------BRIDGE--------------------
        static void BridgeScene() {
            TypeText("\nA lone guardian stands before the Stone Bridge.");

            if (morality >= 0) {
                playerPower += 1;
                TypeText("He recognizes honor and allows you to pass.");

            } else {
                playerPower += 1;
                TypeText("He allows passage, though his eyes follow you carefully.");

            }//Konec if-else
        }//Konec BridgeScene

        //--------------------FOREST--------------------
        static void ForestScene() {
            TypeText("\nYou enter the Whispering Forest.");
            TypeText("Strange voices echo between the ancient trees.");

            Console.WriteLine("\n1) Accept the power of the Shadow");
            Console.WriteLine("2) Resist");
            Console.WriteLine("3) Seek the source");

            int choice = GetChoice(3);

            if (choice == 1) {
                if (morality < 0)
                    playerPower += 2;
                else
                    playerPower -= 1;

                morality--;
                shadowMarked = true;

                TypeText("Cold power flows through your veins.");

            } else if (choice == 2) {
                morality++;
                TypeText("You resist the whispers.");

            } else {
                entityBlessing = true;
                playerPower += 1;

                TypeText("A mysterious entity watches you.");
                TypeText("It grants a silent blessing.");

            }//Konec if-else
        }//Konec ForestScene

        //--------------------FORTRESS--------------------
        static void FortressScene() {
            TypeText("\nYou arrive at a ruined fortress.");
            TypeText("Broken towers loom over the empty courtyard.");

            Console.WriteLine("\n1) Explore");
            Console.WriteLine("2) Search for relics");
            Console.WriteLine("3) Rest");

            int choice = GetChoice(3);

            if (choice == 1) {
                playerPower += 2;
                TypeText("You discover old weapons still usable.");

            } else if (choice == 2) {
                morality++;
                TypeText("While searching the ruins, a hidden trap activates.");

                if (knightAlive) {
                    TypeText("The Knight pushes you aside and takes the blow.");

                    int roll = rng.Next(100);

                    if (roll < 50) {
                        knightAlive = false;
                        TypeText("The Knight is mortally wounded protecting you.");
                    } else {
                        TypeText("The Knight survives, though badly injured.");

                    }//Konec if-else
                }//Konec if

            } else {
                TypeText("You decide to rest inside the fortress.");

                if (morality < 0 && knightAlive) {
                    TypeText("During the night the Knight attacks!");

                    int roll = rng.Next(100);

                    if (roll < 80) {
                        TypeText("You are caught unprepared.");
                        TypeText("\nENDING: The Knight Savior.");

                        Environment.Exit(0);

                    } else {
                        TypeText("You barely survive the ambush.");
                        knightAlive = false;

                    }//Konec if-esle
                }//Konec if
            }//Konec if-esle
        }//Konec FortressScene

        // ===== RUINS =====
        static void RuinsScene() {
            TypeText("\nAt last you reach the Temple Ruins.");
            TypeText("The Heart of Shadow floats above the altar.");
            TypeText("Dark energy pulses through the air.");

            if (morality < 0 && knightAlive) {
                TypeText("\nThe Knight stands before you.");

                bool win = FightKnight();

                if (!win) {
                    TypeText("The Knight defeats you.");
                    TypeText("He destroys the Heart.");

                    TypeText("\nENDING: The Knight Savior.");
                    return;

                }//Konec if
                knightAlive = false;
                morality--;

            }//Konec if
            FinalDecision();

        }//Konec RuinsScene

        //--------------------FINAL--------------------
        static void FinalDecision() {
            Console.WriteLine("\n===== FINAL STATS =====");
            DebugStats();

            if (morality >= 3 && knightAlive) {
                TypeText("The Knight sacrifices himself to stabilize the Heart.");
                TypeText("\nENDING: The Knight's Sacrifice.");

            } else if (morality >= 3) {
                TypeText("You sacrifice yourself to destroy the Heart.");
                TypeText("\nENDING: Martyr of the Kingdom.");

            } else if (morality <= -3) {
                TypeText("You absorb the power of the Heart.");
                TypeText("\nENDING: Dark Lord.");

            } else if (entityBlessing) {
                TypeText("You seal the Heart forever.");
                TypeText("\nENDING: Keeper of Balance.");

            } else {
                TypeText("The Heart explodes with uncontrollable power.");
                TypeText("\nENDING: Catastrophe.");

            }//Konec if-else
        }//Konec FinalDecision

        //--------------------COMBAT--------------------
        static bool FightKnight() {
            TypeText("\nThe duel begins!");

            int playerScore = playerPower + rng.Next(1, 7);
            int knightScore = 7 + rng.Next(1, 7);

            TypeText($"Your strength: {playerScore}");
            TypeText($"Knight's strength: {knightScore}");

            return playerScore > knightScore;

        }//Konec FightKnight

        //--------------------DEBUG--------------------
        static void DebugStats() {
            if (!debugMode) return;

            Console.WriteLine("\n----- DEBUG STATS -----");
            Console.WriteLine($"Morality: {morality}");
            Console.WriteLine($"Ambition: {ambition}");
            Console.WriteLine($"Power: {playerPower}");
            Console.WriteLine($"Knight Trust: {knightTrust}");
            Console.WriteLine($"Knight Alive: {knightAlive}");
            Console.WriteLine($"Villagers Saved: {villagersSaved}");
            Console.WriteLine($"Shadow Marked: {shadowMarked}");
            Console.WriteLine($"Entity Blessing: {entityBlessing}");
            Console.WriteLine("-----------------------\n");
        }//Konec DebugStats

        //--------------------UTIL--------------------
        static int GetChoice(int max) {
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > max)
                Console.WriteLine("Invalid choice.");

            return choice;

        }//Konec GetChoice

        static void TypeText(string text) {
            foreach (char c in text) {
                Console.Write(c);
                Thread.Sleep(8);

            }//Konec foreach
            Console.WriteLine();

        }//Konec TypeText
    }//Konec Program_Test
}//Konec namespace
