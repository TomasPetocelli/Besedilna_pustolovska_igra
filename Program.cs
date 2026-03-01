using System;
using System.Threading;

class Program {
    // ===== GLOBAL STATS =====
    static int morality = 0;      // negative = evil, positive = good
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
        VillageScene();
        CrossroadScene();
        FortressScene();
        RuinsScene();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }//Main

    // ===== INTRO =====
    static void Intro() {
        TypeText("The kingdom of Eryndor stands on the brink of collapse.");
        TypeText("The Heart of Shadow has resurfaced from the ruins.");
        TypeText("Its power can save the world... or destroy it.");
        TypeText("You have chosen to depart.");
    }//Intro

    // ===== VILLAGE =====
    static void VillageScene() {
        TypeText("\nA village burns in the night.");

        Console.WriteLine("\n1) Save the villagers");
        Console.WriteLine("2) Ignore them");
        Console.WriteLine("3) Plunder");

        int choice = GetChoice(3);

        if (choice == 1) {
            villagersSaved = true;
            morality += 2;
            playerPower += 1;
            TypeText("You save as many lives as possible.");
        } else if (choice == 2) {
            ambition++;
            TypeText("It is not your battle.");
        } else {
            morality -= 2;
            playerPower += 2;
            shadowMarked = true;
            TypeText("You take advantage of the chaos.");
        }//if-else

        KnightFirstMeeting();
    }//VillageScene

    // ===== KNIGHT =====
    static void KnightFirstMeeting() {
        TypeText("\nA Royal Knight emerges from the smoke.");

        if (morality > 0) {
            knightTrust += 2;
            TypeText("He looks at you with respect.");
        } else if (morality < 0){
            knightTrust -= 2;
            TypeText("His gaze is cold and stern.");
        }//if-esle

        Console.WriteLine("\n1) Offer alliance");
        Console.WriteLine("2) Remain neutral");
        Console.WriteLine("3) Provoke him");

        int choice = GetChoice(3);

        if (choice == 1) {
            knightTrust++;
            morality++;
        } else if (choice == 3) {
            knightTrust -= 2;
            morality--;
        }//if-esle
    }//KnightFirstMeeting

    // ===== CROSSROAD =====
    static void CrossroadScene() {
        TypeText("\nThree roads open before you.");

        Console.WriteLine("\n1) Monks' Temple");
        Console.WriteLine("2) Stone Bridge");
        Console.WriteLine("3) Whispering Forest");

        int choice = GetChoice(3);

        if (choice == 1) TempleScene();
        else if (choice == 2) BridgeScene();
        else ForestScene();
    }//CrossroadScene

    // ===== TEMPLE =====
    static void TempleScene() {
        TypeText("\nYou enter the Monks' Temple.");

        if (morality >= 0) {
            morality++;
            playerPower += 2;
            TypeText("You receive their blessing.");
        } else {
            playerPower -= 1;
            TypeText("The sacred light repels you.");
        }//if-esle
    }//TempleScene

    // ===== BRIDGE =====
    static void BridgeScene() {
        TypeText("\nA guardian watches you from the bridge.");

        if (morality >= 0) {
            playerPower += 1;
            TypeText("He allows you to pass.");
        } else {
            playerPower += 1;
            TypeText("He lets you pass, though with suspicion.");
        }//if-esle
    }//BridgeScene

    // ===== FOREST =====
    static void ForestScene() {
        TypeText("\nThe Whispering Forest vibrates with energy.");

        Console.WriteLine("\n1) Accept the power of the Shadow");
        Console.WriteLine("2) Resist");
        Console.WriteLine("3) Seek the source");

        int choice = GetChoice(3);

        if (choice == 1) {
            if (morality < 0) {
                playerPower += 2;
            } else {
                playerPower -= 1;
            }//if-esle

            morality--;
            shadowMarked = true;
            TypeText("The Shadow flows through you.");
        } else if (choice == 2) {
            morality++;
            TypeText("You resist.");
        } else {
            entityBlessing = true;
            playerPower += 1;
            TypeText("A neutral entity watches you.");
        }//if-else
    }//ForestScene

    // ===== FORTRESS =====
    static void FortressScene() {
        TypeText("\nYou reach a ruined fortress.");

        Console.WriteLine("\n1) Explore");
        Console.WriteLine("2) Search for relics");
        Console.WriteLine("3) Rest");

        int choice = GetChoice(3);

        if (choice == 1) {
            playerPower += 2;
        } else if (choice == 2) {
            morality++;
        } else {
            TypeText("You decide to rest...");

            if (morality < 0 && knightAlive) {
                TypeText("In the dead of night, the Knight attacks you.");

                // 80% chance knight wins
                int roll = rng.Next(100);
                if (roll < 80) {
                    TypeText("You are caught off guard and slain.");
                    TypeText("\nENDING: The Knight Savior.");
                    Environment.Exit(0);
                } else {
                    TypeText("You miraculously manage to react.");
                    knightAlive = false;
                }//if-esle
            }//if
        }//if-else
    }//FortressScene

    // ===== RUINS =====
    static void RuinsScene() {
        TypeText("\nThe Temple Ruins stand before you.");
        TypeText("The Heart of Shadow pulses.");

        if (morality < 0 && knightAlive) {
            TypeText("\nThe Knight confronts you.");

            bool win = FightKnight();

            if (!win) {
                TypeText("The Knight defeats you.");
                TypeText("He destroys the Heart.");
                TypeText("\nENDING: The Knight Savior.");
                return;
            }//if

            knightAlive = false;
            morality--;
        }//if

        FinalDecision();
    }//RuinsScene

    // ===== FINAL =====
    static void FinalDecision() {
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
            TypeText("You seal the Heart.");
            TypeText("\nENDING: Keeper of Balance.");
        } else {
            TypeText("The power explodes.");
            TypeText("\nENDING: Catastrophe.");
        }//if-else
    }//FinalDecision

    // ===== COMBAT =====
    static bool FightKnight() {
        TypeText("\n The duel begins!");

        int playerScore = playerPower + rng.Next(1, 7);
        int knightScore = 7 + rng.Next(1, 7);

        TypeText($"Your strength: {playerScore}");
        TypeText($"Knight's strength: {knightScore}");

        return playerScore > knightScore;
    }//FightKnight

    // ===== UTIL =====
    static int GetChoice(int max) {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > max)
            Console.WriteLine("Invalid choice.");
        return choice;
    }// GetChoice

    static void TypeText(string text) {
        foreach (char c in text) {
            Console.Write(c);
            Thread.Sleep(10);
        }//foreach
        Console.WriteLine();
    }//TypeText
}//Program