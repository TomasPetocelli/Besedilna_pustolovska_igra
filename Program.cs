using System;
using System.Collections.Generic;
using System.Threading;

class Program {
    static int morality = 0;     // + = good | - = evil
    static int ambition = 0;     // + = power-seeking | - = humble
    static List<string> inventory = new List<string>();

    static void Main() {
        Introduction();
        VillageScene();
        TransitionAfterVillage();
        ForestScene();
        TransitionAfterForest();
        KnightScene();
        TransitionAfterKnight();
        GateScene();
        Ending();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }//Main

    //Slow typing effect
    static void TypeWriter(string text) {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(15);
        }
        Console.WriteLine();
    }//TypeWriter

    static void Introduction() {
        Console.Clear();
        TypeWriter("=== SHADOWS OF ELARION ===\n");
        TypeWriter("The kingdom is dying.");
        TypeWriter("The Heart of Aethrys holds the balance between light and darkness.");
        TypeWriter("If it shatters... Elarion will fall.");
        TypeWriter("You are the only one close enough to reach it.\n");
    }//Introduction

    static int GetChoice() {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3) {
            Console.WriteLine("Enter 1, 2 or 3.");
        }//while
        return choice;
    }//GetChoice

    static void VillageScene() {
        TypeWriter("\n--- THE BURNING VILLAGE ---");
        TypeWriter("Screams break the evening silence.");
        TypeWriter("A village is under attack.");

        Console.WriteLine("\n1) Help the villagers");
        Console.WriteLine("2) Ignore them and continue");
        Console.WriteLine("3) Take advantage of the chaos");

        int choice = GetChoice();

        if (choice == 1) {
            morality++;
            ambition--;
            inventory.Add("Amulet of Light");
            TypeWriter("\nYou fight bravely. The raiders flee.");
            TypeWriter("A woman gives you an Amulet of Light.");
        } else if (choice == 2) {
            TypeWriter("\nYou grit your teeth and walk away.");
            TypeWriter("The screams fade behind you.");
        } else {
            morality--;
            ambition++;
            inventory.Add("Bag of Gold");
            TypeWriter("\nWhile the village burns, you fill your pockets.");
            TypeWriter("The flames illuminate your face... without remorse.");
        }//if-else
    }//VillageScene

    static void TransitionAfterVillage() {
        TypeWriter("\nThe road toward the ruins grows quieter.");

        if (morality > 0)
            TypeWriter("The memory of those you saved warms your heart.");
        else if (morality < 0)
            TypeWriter("The shadows seem to follow you... pleased.");
        else
            TypeWriter("The wind does not judge your actions.");
    }//TransitionAfterVillage

    static void ForestScene() {
        TypeWriter("\n--- THE FOREST OF ILLUSIONS ---");
        TypeWriter("Mist twists reality.");
        TypeWriter("A voice whispers: 'What do you truly desire?'");

        Console.WriteLine("\n1) To save Elarion");
        Console.WriteLine("2) To understand my destiny");
        Console.WriteLine("3) To claim the Heart's power");

        int choice = GetChoice();

        if (choice == 1) {
            morality++;
            ambition--;
            TypeWriter("\nThe mist parts slightly.");
        } else if (choice == 2) {
            TypeWriter("\nThe forest remains uncertain, like your soul.");
        } else {
            morality--;
            ambition++;
            TypeWriter("\nThe shadows bow to your ambition.");
        }//if-else
    }//ForestScene

    static void TransitionAfterForest() {
        TypeWriter("\nYou emerge from the forest changed.");

        if (ambition > 1)
            TypeWriter("A hunger for power grows inside you.");
        else if (ambition < 0)
            TypeWriter("Your steps feel lighter, free from greed.");
    }//TransitionAfterForest

    static void KnightScene() {
        TypeWriter("\n--- THE WOUNDED KNIGHT ---");
        TypeWriter("Another chosen lies bleeding on the ground.");

        Console.WriteLine("\n1) Help him");
        Console.WriteLine("2) Leave him");
        Console.WriteLine("3) Eliminate a future rival");

        int choice = GetChoice();

        if (choice == 1) {
            morality++;
            ambition--;
            TypeWriter("\nThe knight whispers: 'I owe you my life.'");
        } else if (choice == 2) {
            TypeWriter("\nYou avoid his gaze and move on.");
        } else {
            morality--;
            ambition++;
            TypeWriter("\nSilence falls after his final breath.");
        }//if-else
    }//KnightScene

    static void TransitionAfterKnight() {
        if (morality >= 2)
            TypeWriter("\nYou still feel human.");
        else if (morality <= -2)
            TypeWriter("\nSomething inside you is fading.");
        else
            TypeWriter("\nEvery step brings you closer to destiny.");
    }//TransitionAfterKnight

    static void GateScene() {
        TypeWriter("\n--- THE GATE OF THE HEART ---");
        TypeWriter("An ancient spirit rises from the light.");

        Console.WriteLine("\n1) I will sacrifice myself for the kingdom");
        Console.WriteLine("2) What is the price?");
        Console.WriteLine("3) The power will be mine");

        int choice = GetChoice();

        if (choice == 1) {
            morality++;
            ambition--;
        } else if (choice == 3) {
            morality--;
            ambition++;
        }//if-else
    }//GateScene

    static void Ending() {
        TypeWriter("\n--- THE HEART OF AETHRYS ---");

        if (morality >= 4) {
            TypeWriter("The knight you saved returns and sacrifices himself for you.");
            TypeWriter("SECRET ENDING: Legendary Hero.");
        } else if (morality <= -4) {
            TypeWriter("The Heart turns completely dark.");
            TypeWriter("SECRET ENDING: Lord of Shadows.");
        } else if (morality >= 2) {
            TypeWriter("You sacrifice yourself. The kingdom is reborn.");
            TypeWriter("ENDING: Radiant Martyr.");
        } else if (morality <= -2) {
            TypeWriter("You absorb the Heart's power and dominate Elarion.");
            TypeWriter("ENDING: Dark Tyrant.");
        } else {
            TypeWriter("The world survives... but remains fragile.");
            TypeWriter("ENDING: Fragile Balance.");
        }//if-else

        TypeWriter($"\nMorality: {morality}");
        TypeWriter($"Ambition: {ambition}");
    }//GateScene
}//class Program