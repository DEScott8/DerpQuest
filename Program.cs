using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerpQuest
{
   class Program
   {
      static void Main(string[] args)
      {
         String name; //String containing the player's name.
         String command = null, option = "no", room = "Weird Dream", answer = null; //Stores the player's commands or option.
         bool quitGame = false, playerDead = false, gameWon = false; //Various game state flags.
         bool riverCrossed = false, riddlesSolved = false, gateOpen = false; //Other game state flags.
         bool gotCoolFace = false, gotCoffee = false, madeCoffee = false, gotWallet = false; //First line of item variables.
         bool paddleFound = false, gotPaddle = false; //Second line of item variables.
         int gold = 0; //Gold total.
         int fee = 50; //The fee to get through the Troll Booth.
         Random rng = new Random();

         Console.Write("What is your name?: "); //Prompts the user to enter their name.
         name = Console.ReadLine(); //The player inputs their name here.
         Console.WriteLine("Greetings, " + name + "!"); //A message is displayed greeting the player.
         DescribeRoom(rng, command, room, gotCoolFace, gotWallet, madeCoffee, fee, ref paddleFound, gotPaddle, riverCrossed, riddlesSolved); //The player sees this when they enter the game
         do
         {
            Console.Write("What will you do?: "); //Prompts the user for a command
            command = Console.ReadLine(); //Reads input from the keyboard
            command = command.ToLower(); //Converts the command to the lowercase version of itself
            //The following nested if statements handle different commands.
            //The following are common commands shared by many rooms.
            if (command.Contains("look") == true)
               DescribeRoom(rng, command, room, gotCoolFace, gotWallet, madeCoffee, fee, ref paddleFound, gotPaddle, riverCrossed, riddlesSolved);
            else if (command.Contains("get") == true || command.Contains("take") == true)
               GetItem(command, room, ref gotCoolFace, ref gotWallet, ref gotCoffee, ref madeCoffee, ref gold, paddleFound, ref gotPaddle);
            else if (command.Contains("use") == true)
               UseItem(command, room, gotCoolFace, gotCoffee, ref madeCoffee, gotPaddle, ref gateOpen, fee, ref playerDead, ref riverCrossed, ref gameWon);
            else if (command.Contains("go") == true && command.Contains("gold") == false)
            {
               GoDirection(command, ref room, gotCoolFace, gotWallet, gotCoffee, madeCoffee, fee, ref gateOpen, riverCrossed, riddlesSolved);
            }//End else if
            else if (command.Contains("talk") == true)
               TalkToNPC(ref room, ref gold, fee, gateOpen, riverCrossed, name, answer, ref riddlesSolved);
            else if (command.Contains("help") == true)
            {
               Console.WriteLine("In order to play this game, the player must type a command, such as");
               Console.WriteLine("\"go up\" or \"make coffee\" to do a command. For example, let's say you're");
               Console.WriteLine("having some strange dream. Typing \"wake up\" would probably get you out of it.");
               Console.WriteLine("(Get the hint?) ;)");
            }//End else if
            else if (command.Contains("quit") == true)
            {
               Console.WriteLine("Are you sure you want to quit the game?");
               option = Console.ReadLine();
               option = option.ToLower(); //Converts the option to the lowercase version
               while (option != "yes" && option != "no")
               {
                  Console.WriteLine("Please type yes or no!");
                  Console.WriteLine("Are you sure you want to quit the game?");
                  option = Console.ReadLine();
               }//End while
               if (option == "yes")
                  quitGame = true;
            }//End else if
            //The following commands are specific to certain rooms
            else if (command == "wake up")
            {//This command wakes you up from the first room
               if (room == "Weird Dream")
               {
                  Console.WriteLine("You begin to wake up. You also don't want to have anything to");
                  Console.WriteLine("with clowns for the time being...");
                  room = "Upstairs Bedroom"; //The player moves to the next room
                  DescribeRoom(rng, command, room, gotCoolFace, gotWallet, madeCoffee, fee, ref paddleFound, gotPaddle, riverCrossed, riddlesSolved);
               }//End if
               else
                  Console.WriteLine("You're already awake"); //This should be here, or else the next statement can't be written
            }//End else if
            else if (command == "make coffee")
            {
               if (room == "Downstairs")
               {
                  if (gotCoffee == false && madeCoffee == false)
                  {
                     Console.WriteLine("You put the grounds and water in the coffee maker.");
                     Console.WriteLine("The coffee begins to percolate and before long it's done!");
                     Console.WriteLine("You made coffee! What could possibly go wrong now?");
                     madeCoffee = true;
                  }//End if
                  else if (madeCoffee == true)
                     Console.WriteLine("There's already hot coffee in the coffee pot!");
                  else
                     Console.WriteLine("You already have coffee! No need to make another cup!");
               }//End if
            }//End else if
            else if (command == "put on coolface")
            {
               if (gotCoolFace == true)
               {
                  if (room == "Troll Booth")
                  {
                     Console.WriteLine("You put on your cool face. You walk up to the booth. The Troll waves.");
                     Console.WriteLine("\"Hey Steve! I didn't know it was you! C'mon through!\" he says, and");
                     Console.WriteLine("Opens up the gate for you. Well done, you outsmarted the Troll!");
                     gateOpen = true;
                  }//End if
                  else
                  {
                     Console.WriteLine("You put it on and you look like a jerk. You also begin laughing");
                     Console.WriteLine("Like a jerk and if we didn't know better we'd say you were one.");
                     Console.WriteLine("After looking like a jerk you put it away.");
                  }//End else
               }//End if
               else
                  Console.WriteLine("You would put on your coolface but you left it at home, so you can't.");
            }//End else if
            else if (command.Contains("pay") == true)
            {
               if (room == "Troll Booth")
               {
                  if (gold >= fee)
                  {
                     Console.WriteLine("You pay the troll his due and he chuckles.");
                     Console.WriteLine("\"Problem player?\" he says. You look back at him and simply");
                     Console.WriteLine("Tell him to GTFO.");
                     gold -= fee;
                     gateOpen = true;
                  }//End if
                  else
                  {
                     Console.WriteLine("A disembodied head appears in your mind and says");
                     Console.WriteLine("\"You don't have enough money. Stupid.\" before vanishing");
                     Console.WriteLine("In a puff of imaginary smoke.");
                  }//End else
               }//End if
               else if (room == "Valley") {
                  if (command.Contains("50") == true) {
                     if (gold >= 50) {
                        Console.WriteLine("The hint booth man clears his throat.");
                        Console.WriteLine("\"What's in a name? A rose by any other name");
                        Console.WriteLine("would smell as sweet...\" You look puzzled at him and he");
                        Console.WriteLine("looks back at you. \"No refunds.\" Well, there goes 50 gold.");
                     }//End if
                     else {
                        Console.WriteLine("A disembodied head appears in your mind and says");
                        Console.WriteLine("\"You don't have enough money. Stupid.\" before vanishing");
                        Console.WriteLine("In a puff of imaginary smoke.");
                     }//End else
                  }//End if
                  else if (command.Contains("100") == true) {
                     if (gold == 100) {
                        Console.WriteLine("The hint man clears his throat. \"Okay, you see those rosebushes");
                        Console.WriteLine("over there? They've got a paddle in them that you need to get");
                        Console.WriteLine("across S*** Creek. In any case, up that mountain over there is");
                        Console.WriteLine("a wizard, but he's guarded by a Sphinx. Word is he stole all");
                        Console.WriteLine("his riddles from that movie 'Monty Python's Quest for the Holy");
                        Console.WriteLine("Grail! If you've seen that movie you should be able to answer him");
                        Console.WriteLine("easily! Anyway, thanks for your business!");
                     }//End if
                     else {
                        Console.WriteLine("A disembodied head appears in your mind and says");
                        Console.WriteLine("\"You don't have enough money. Stupid.\" before vanishing");
                        Console.WriteLine("In a puff of imaginary smoke.");
                     }//End else
                  }//End else if
                  else
                     Console.WriteLine("How much will you pay him?");
               }//End else if
               else
                  Console.WriteLine("Um, what are you trying to do?");
            }//End else if
            else if (command.Contains("fight") == true)
            {
               if (room == "Troll Booth")
               {
                  if (fee < 100)
                  {
                     Console.WriteLine("You try to fight your way past the troll. He gets you into a hold,");
                     Console.WriteLine("sits on your face, and farts on you. He also demands 100 gold now");
                     Console.WriteLine("instead of his usual 50. Now you REALLY feel embarassed. And smelly.");
                     fee = 100;
                  }//End if
                  else
                     Console.WriteLine("You don't want to make him madder and more expensive.");
               }//End if
               else
                  Console.WriteLine("You can't fight anything here!");
            }//End else if
            else if (command == "cross creek" || command == "cross river")
            {
               if (room == "Valley")
               {
                  if (gotPaddle == true)
                  {
                     if (riverCrossed == false)
                     {
                        Console.WriteLine("You use your paddle to cross the river and");
                        Console.WriteLine("get to the other side. The boat still seems fine.");
                        riverCrossed = true;
                     }//End if
                     else
                     {
                        Console.WriteLine("You cross back over the river to where you came from.");
                        riverCrossed = false;
                     }//End else
                  }//End if
                  else
                  {
                     Console.WriteLine("You attempt the idiotic and try to use your boat without a paddle.");
                     Console.WriteLine("BIG MISTAKE! The boat capsizes and you are thrown into the rapids.");
                     Console.WriteLine("You see the Sierra Mountains before you drown and the last thought");
                     Console.WriteLine("That enters you head is \"Damn you Mr. and Mrs. Williams...\"");
                     playerDead = true;
                  }//End else
               }//End if
               else
                  Console.WriteLine("Um, what are you trying to do?");
            }//End else if
            else if (command == "drink coffee")
            {
               if (gotCoffee == true)
                  if (room == "Wizard's Basement")
                  {
                     Console.WriteLine("You being to feel uneasy and take a drink of your coffee.");
                     Console.WriteLine("Just then you see a flash of light in front of you and your");
                     Console.WriteLine("coffee slips out from your hands and spills all over the Wizard!");
                     Console.WriteLine("He begins to vent and fume until he regains his composure.");
                     Console.WriteLine("\"Oh, so you got hot coffee on me! What are you going to do?");
                     Console.WriteLine("Sue me? Hahahahahahaha!\" So you scurry to one of the magic");
                     Console.WriteLine("books, leaf through it and find a 'Summon Lawyer spell'.");
                     Console.WriteLine("You cast it and magical lawyers whisk the wizard away!");
                     Console.WriteLine("\"You won't get away with this!\" he shouts, as he is carried");
                     Console.WriteLine("off. You then notice someone come from the corner. It's a beautiful");
                     Console.WriteLine("princess! You help her to her feet and she thanks you for saving");
                     Console.WriteLine("her from the wizard who happens to be named Rodney.");
                     Console.WriteLine("Later, you find out you are granted custody of the tower and you");
                     Console.WriteLine("and princess generica live happily ever after and have a zillion");
                     Console.WriteLine("babies. The end!");
                     gameWon = true;
                  }//End if
                  else
                  {
                     Console.WriteLine("You try to drink the coffee but it burns your tongue.");
                     Console.WriteLine("How the heck does it stay hot like this!?");
                  }//End else
               else
                  Console.WriteLine("You don't even have coffee...");
            }
            else
               Console.WriteLine("Um, what are you trying to do?"); //This happens if the player puts in an invalid command
            command = null;
         } while (quitGame == false && playerDead == false && gameWon == false);
         if (gameWon == true) //If the player wins the game, a special message is displayed upon closing.
         {
            Console.WriteLine("A message from the author:");
            Console.WriteLine("Congratulations on beating the game!");
            Console.WriteLine("I hope you enjoyed playing this game as much as I enjoyed making it!");
            Console.WriteLine("In the future I hope to make more advanced games that other people");
            Console.WriteLine("Can enjoy! Until then...");
         }//End if
         Console.WriteLine("Goodbye!"); //This message displays no matter what.
      }//End main

      //Method DescribeRoom describes the room the player is in
      public static void DescribeRoom(Random rng, String command, String room, bool gotCoolFace, bool gotWallet, bool madeCoffee, int fee, ref bool paddleFound, bool gotPaddle, bool riverCrossed, bool riddlesSolved) {
         switch(room) {
            case "Weird Dream":
               int scenario = rng.Next(3);
               if (scenario == 0)
               {
                  Console.WriteLine("You are a a rodeo when suddenly the Bull Rider falls off the Bull.");
                  Console.WriteLine("You are a Rodeo Clown. You and the other clowns entertain the audience");
                  Console.WriteLine("while paramedics get the stretcher.");
               }//End if
               else if (scenario == 1)
               {
                  Console.WriteLine("You are standing under the Tree of Destiny where you are destined to find");
                  Console.WriteLine("your one true love. A pretty girl suddenly approaches you. She smiles");
                  Console.WriteLine("shyly and you close in for a kiss. She then takes her face off and reveals");
                  Console.WriteLine("herself to be a clown!");
               }//End if else
               else
               {
                  Console.WriteLine("You are watching the presidential election on TV. It appears to be");
                  Console.WriteLine("a close race between Donald Trump and Bernie Sanders. You become");
                  Console.WriteLine("excited to find out if the candidate you voted for will win.");
                  Console.WriteLine("Finally, the ballots are counted and the winner is...");
                  Console.WriteLine("Ronald McDonald!?");
               }//End else
               break;
            case "Upstairs Bedroom":
               Console.WriteLine("You are in the upstairs bedroom of your house.");
               Console.WriteLine("There is a bed for you to sleep on, as well as");
               Console.WriteLine("a wooden dresser and nightstand with a couple drawers in it.");
               if(gotCoolFace == false)
                  Console.WriteLine("You keep your coolface on the nightstand");
               if(gotWallet == false)
                  Console.WriteLine("Your wallet is on the dresser");
               break;
            case "Downstairs":
               Console.WriteLine("You are downstairs. There is the living room which is mostly empty");
               Console.WriteLine("except for a chair and some books, but you don't feel like reading");
               Console.WriteLine("them at the moment. There is also a kitchen with a coffee maker.");
               if (madeCoffee == true)
                  Console.WriteLine("There is coffee in the coffee maker.");
               else
                  Console.WriteLine("The coffee maker is currently empty.");
               break;
            case "Outside of House":
               Console.WriteLine("You are now standing outside of your house. There is a log, some");
               Console.WriteLine("wood, and some woodcutting tools. Your house is a simple 2 story");
               Console.WriteLine("cottage out on a plain. There is a forest to the North.");
               break;
            case "Lost Forest 1":
               if (command == "look tree" || command == "look at tree" || command == "look carving" || command == "look at carving")
               {
                  Console.WriteLine("You look at the carving and see a cryptic message. It reads;");
                  Console.WriteLine("The path through the forest is NEW.");
               }//End if
               else
               {
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End else
               break;
            case "Lost Forest 2":
               Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
               Console.WriteLine("giving the earth below a ghastly, angelic glow.");
               break;
            case "Lost Forest 3":
               Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
               Console.WriteLine("giving the earth below a ghastly, angelic glow.");
               break;
            case "Troll Booth":
               Console.WriteLine("You see a booth run by a smug looking troll. He grins and talks to you");
               Console.WriteLine("\"That'll be " + fee + " gold. You want to cross the bridge over the lake.");
               break;
            case "Valley":
               if (command == "look sign" || command == "look at sign") {
                  if (riverCrossed == false) {
                     Console.WriteLine("The sign reads \"S*** Creek. Crossing without a paddle could prove fatal.\"");
                     Console.WriteLine("My, how lovely...");
                  }//End if
                  else
                     Console.WriteLine("You can't read it from this side!");
               }//End if
               else if (command == "look bush" || command == "look at bush" || command == "look rosebush" || command == "look at rosebush")
               {
                  if (riverCrossed == false)
                  {
                     if (gotPaddle == false)
                     {
                        Console.WriteLine("You look at the bush. Now you realize what's suspicious about it!");
                        Console.WriteLine("It seems as though someone has left a paddle in this bush!");
                        if (paddleFound == false)
                           paddleFound = true;
                     }//End if
                     else
                        Console.WriteLine("This is where you found your paddle.");
                  }//End if
                  else
                     Console.WriteLine("It's all the way across the river. Looks normal from here.");
               }//End else if
               else if (command == "look booth" || command == "look at booth" || command == "look hint booth" || command == "look at hint booth")
               {
                  if (riverCrossed == false)
                  {
                     Console.WriteLine("There is a hint booth here. You see a balding man");
                     Console.WriteLine("running this booth. He is sharply dressed.");
                  }//End if
                  else
                     Console.WriteLine("You see the booth across the river. Looks the same as last time.");
               }//End else if
               else if (command == "look boat" || command == "look at boat")
               {
                  Console.WriteLine("You see a rickety looking boat on the dock. It looks like it's");
                  Console.WriteLine("ill-suited for travel against the strong waters of this creek.");
               }//End else if
               else {
                  Console.WriteLine("You make your way to a valley. You see a large mountain with");
                  Console.WriteLine("a path leading upwards to the North. There is also a rapidly");
                  Console.WriteLine("rushing creek blocking the way, with a docked boat. There is a sign near the creek.");
                  Console.WriteLine("There is a suspicious Rosebush on the south end of the creek.");
                  Console.WriteLine("There is also a hint booth here...");
               }//End else
               break;
            case "Ravaging Riddle Ridge":
               Console.WriteLine("You are at the Ravaging Riddle Ridge. You see an ominious,");
               Console.WriteLine("important looking tower looming over the other side of a bridge.");
               if (riddlesSolved == false)
                  Console.WriteLine("There is a Sphinx blocking your path.");
               break;
            case "Wizard's Tower":
               Console.WriteLine("You are in the tower of an unknown Wizard. You see a crumbling staircase");
               Console.WriteLine("Leading upward. You see another staircase leading downwards");
               break;
            case "Wizard's Basement":
               Console.WriteLine("The Wizard keeps an assortment of fancy books and trinkets in his");
               Console.WriteLine("basement. The Wizard looks at you as though he is ready to cast a spell.");
               break;
         }//End switch
      }//End DescribeRoom

      //Method GetItem allows the player to take items in the game world
      public static void GetItem(String command, String room, ref bool gotCoolFace, ref bool gotWallet, ref bool gotCoffee, ref bool madeCoffee, ref int gold, bool paddleFound, ref bool gotPaddle)
      {
         switch (room)
         {
            case "Upstairs Bedroom":
               if (command == "take wallet" || command == "get wallet")
               {
                  if (gotWallet == false) //If any gotItem variable is false, this sets it as true
                  {
                     Console.WriteLine("Okay, you grab your wallet.");
                     Console.WriteLine("You are delighted to find that it contains 100 gold!");
                     gotWallet = true;
                     gold += 100; //The 100 gold is found in the wallet
                  }//End if
                  else
                     Console.WriteLine("You already took your wallet!");
               }//End if
               else if (command == "take coolface" || command == "get coolface")
               {
                  if (gotCoolFace == false)
                  {
                     Console.WriteLine("Okay, you grab your coolface and put it in your pocket.");
                     gotCoolFace = true;
                  }//End if
                  else
                     Console.WriteLine("You already have your coolface!");
               }//End else if
               else
                  Console.WriteLine("You can't take that!");
               break;
            case "Downstairs":
               if (command == "get coffee" || command == "take coffee")
               {
                  if (gotCoffee == false)
                  {
                     if (madeCoffee == true)
                     {
                        Console.WriteLine("You grab a nearby thermos and pour some coffee into it.");
                        Console.WriteLine("You also manage to scald your hands in the process. Derp!");
                        gotCoffee = true;
                        madeCoffee = false;
                     }//End if
                     else
                        Console.WriteLine("You would but you forgot to make it. Derp!");
                  }//End if
                  else
                     Console.WriteLine("You already got coffee and somehow it's STILL hot! Weird...");
               }//End if
               break;
            case "Valley":
               if (command == "get paddle" || command == "take paddle")
               {
                  if (paddleFound == true)
                  {
                     if (gotPaddle == false)
                     {
                        Console.WriteLine("You reach into the rosebush and grab the paddle lodged into it.");
                        Console.WriteLine("After reaching in and getting it out, your hand is slightly bleeding");
                        Console.WriteLine("from the thorns in the bush. You... Wait are you smelling your hand?");
                        Console.WriteLine("Okay stop smelling your hand. Stop it. CUT IT OUT!");
                        gotPaddle = true;
                     }
                     else
                        Console.WriteLine("You already got the paddle.");
                  }//End if
                  else
                     Console.WriteLine("You don't see a paddle here.");
               }//End if
               break;
            default:
               Console.WriteLine("There is nothing to take here!");
               break;
         }//End switch
      }//End GetItem

      //Method UseItem allows the player to use a specific item
      public static void UseItem(String command, String room, bool gotCoolFace, bool gotCoffee, ref bool madeCoffee, bool gotPaddle, ref bool gateOpen, int fee, ref bool playerDead, ref bool riverCrossed, ref bool gameWon)
      {
         switch (command)
         {
            case "use coffee":
               if (gotCoffee == true)
                  if (room == "Wizard's Basement") {
                     Console.WriteLine("You being to feel uneasy and take a drink of your coffee.");
                     Console.WriteLine("Just then you see a flash of light in front of you and your");
                     Console.WriteLine("coffee slips out from your hands and spills all over the Wizard!");
                     Console.WriteLine("He begins to vent and fume until he regains his composure.");
                     Console.WriteLine("\"Oh, so you got hot coffee on me! What are you going to do?");
                     Console.WriteLine("Sue me? Hahahahahahaha!\" So you scurry to one of the magic");
                     Console.WriteLine("books, leaf through it and find a 'Summon Lawyer spell'.");
                     Console.WriteLine("You cast it and magical lawyers whisk the wizard away!");
                     Console.WriteLine("\"You won't get away with this!\" he shouts, as he is carried");
                     Console.WriteLine("off. You then notice someone come from the corner. It's a beautiful");
                     Console.WriteLine("princess! You help her to her feet and she thanks you for saving");
                     Console.WriteLine("her from the wizard who happens to be named Rodney.");
                     Console.WriteLine("Later, you find out you are granted custody of the tower and you");
                     Console.WriteLine("and princess generica live happily ever after and have a zillion");
                     Console.WriteLine("babies. The end!");
                     gameWon = true;
                  }//End if
                  else {
                  Console.WriteLine("You try to drink the coffee but it burns your tongue.");
                  Console.WriteLine("How the heck does it stay hot like this!?");
                  }//End else
               else
                  Console.WriteLine("You don't even have coffee...");
               break;
            case "use coffee maker":
               if (gotCoffee == false && madeCoffee == false)
               {
                  Console.WriteLine("You put the grounds and water in the coffee maker.");
                  Console.WriteLine("The coffee begins to percolate and before long it's done!");
                  Console.WriteLine("You made coffee! What could possibly go wrong now?");
                  madeCoffee = true;
               }//End if
               else if (madeCoffee == true)
                  Console.WriteLine("There's already hot coffee in the coffee pot!");
               else
                  Console.WriteLine("You already have coffee! No need to make another cup!");
               break;
            case "use coolface":
               if (gotCoolFace == true)
               {
                  if (room == "Troll Booth")
                  {
                     Console.WriteLine("You put on your cool face. You walk up to the booth. The Troll waves.");
                     Console.WriteLine("\"Hey Steve! I didn't know it was you! C'mon through!\" he says, and");
                     Console.WriteLine("Opens up the gate for you. Well done, you outsmarted the Troll!");
                     gateOpen = true;
                  }//End if
                  else
                  {
                     Console.WriteLine("You put it on and you look like a jerk. You also begin laughing");
                     Console.WriteLine("Like a jerk and if we didn't know better we'd say you were one.");
                     Console.WriteLine("After looking like a jerk you put it away.");
                  }//End else
               }//End if
               else
                  Console.WriteLine("You would put on your coolface but you left it at home, so you can't.");
               break;
            case "use boat":
               if (room == "Valley")
               {
                  if (gotPaddle == true)
                  {
                     if (riverCrossed == false)
                     {
                        Console.WriteLine("You use your paddle to cross the river and");
                        Console.WriteLine("get to the other side. The boat still seems fine.");
                        riverCrossed = true;
                     }//End if
                     else
                     {
                        Console.WriteLine("You cross back over the river to where you came from.");
                        riverCrossed = false;
                     }//End else
                  }//End if
                  else
                  {
                     Console.WriteLine("You attempt the idiotic and try to use your boat without a paddle.");
                     Console.WriteLine("BIG MISTAKE! The boat capsizes and you are thrown into the rapids.");
                     Console.WriteLine("You see the Sierra Mountains before you drown and the last thought");
                     Console.WriteLine("That enters you head is \"Damn you Mr. and Mrs. Williams...\"");
                     playerDead = true;
                  }//End else
               }//End if
               else
                  Console.WriteLine("Um, what are you trying to do?");
               break;
            case "use paddle":
               if (gotPaddle == true)
                  {
                  if (room == "Valley") {
                     if (riverCrossed == false)
                     {
                        Console.WriteLine("You use your paddle to cross the river and");
                        Console.WriteLine("get to the other side. The boat still seems fine.");
                        riverCrossed = true;
                     }//End if
                     else
                     {
                        Console.WriteLine("You cross back over the river to where you came from.");
                        riverCrossed = false;
                     }//End else
                  }//End if
                  else
                     Console.WriteLine("You wack something with your paddle. Brilliant!");
               }//End if
               else
                  Console.WriteLine("But you don't have a paddle!");
               break;
            default:
               Console.WriteLine("You can't use that!");
               break;
         }//End switch
      }//End UseItem

      //Method GoDirection allows the player to change rooms
      public static void GoDirection(String command, ref String room, bool gotCoolFace, bool gotWallet, bool gotCoffee, bool madeCoffee, int fee, ref bool gateOpen, bool riverCrossed, bool riddlesSolved)
      {
         switch (room)
         {
            case "Upstairs Bedroom":
               if (command == "go down" || command == "go downstairs")
               {
                  Console.WriteLine("You clumsily fall down the stairs! Well, at least you got");
                  Console.WriteLine("to where you wanted to go.");
                  room = "Downstairs";
                  Console.WriteLine("You are downstairs. There is the living room which is mostly empty");
                  Console.WriteLine("except for a chair and some books, but you don't feel like reading");
                  Console.WriteLine("them at the moment. There is also a kitchen with a coffee maker.");
                  if (madeCoffee == true)
                     Console.WriteLine("There is coffee in the coffee maker.");
                  else
                     Console.WriteLine("The coffee maker is currently empty.");
               }//End if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Downstairs":
               if (command == "go up" || command == "go upstairs")
               {
                  Console.WriteLine("You gracefully fall back up the stairs and end up back in your bedroom.");
                  room = "Upstairs Bedroom";
                  Console.WriteLine("You are in the upstairs bedroom of your house.");
                  Console.WriteLine("There is a bed for you to sleep on, as well as");
                  Console.WriteLine("a wooden dresser and nightstand with a couple drawers in it.");
                  if (gotCoolFace == false)
                     Console.WriteLine("You keep your coolface on the nightstand");
                  if (gotWallet == false)
                     Console.WriteLine("Your wallet is on the dresser");
               }//End if
               else if (command == "go north" || command == "go outside")
               {
                  Console.WriteLine("You open the door and step outside of your house.");
                  Console.WriteLine("It begins to dawn on you that you actually have a life!");
                  room = "Outside of House";
                  Console.WriteLine("You are now standing outside of your house. There is a log, some");
                  Console.WriteLine("wood, and some woodcutting tools. Your house is a simple 2 story");
                  Console.WriteLine("cottage out on a plain. There is a forest to the North.");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Outside of House":
               if(command == "go south" || command == "go inside") {
                  Console.WriteLine("You either are afraid of living, or you left something at home");
                  Console.WriteLine("like some sort of bonehead. In any case, you retreat to the comfy");
                  Console.WriteLine("walls of your living room/kitchen.");
                  room = "Downstairs";
                  Console.WriteLine("You are downstairs. There is the living room which is mostly empty");
                  Console.WriteLine("except for a chair and some books, but you don't feel like reading");
                  Console.WriteLine("them at the moment. There is also a kitchen with a coffee maker.");
                  if (madeCoffee == true)
                     Console.WriteLine("There is coffee in the coffee maker.");
                  else
                     Console.WriteLine("The coffee maker is currently empty.");
               }//End if
               else if (command == "go north")
               {
                  Console.WriteLine("You take a friend's advice and decide to get lost. Specifically,");
                  Console.WriteLine("You decide to wander around in the forest to the North. Yeah,");
                  Console.WriteLine("Real smart...");
                  room = "Lost Forest 1";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End if else
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Lost Forest 1":
               if (command == "go south")
               {
                  Console.WriteLine("You go down a path in the forest and end up back outside your house.");
                  room = "Outside of House";
                  Console.WriteLine("You are now standing outside of your house. There is a log, some");
                  Console.WriteLine("wood, and some woodcutting tools. Your house is a simple 2 story");
                  Console.WriteLine("cottage out on a plain. There is a forest to the North.");
               }//End if
               else if (command == "go north") {
                  Console.WriteLine("You go down a path in the forest.");
                  room = "Lost Forest 2";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow.");
               }//End else if
               else if (command == "go east" || command == "go west")
               {
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Lost Forest 2":
               if (command == "go east")
               {
                  Console.WriteLine("You go down a path in the forest.");
                  room = "Lost Forest 3";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow.");
               }//End if
               else if(command == "go north" || command == "go west") {
                  Console.WriteLine("You go down a path in the forest.");
                  room = "Lost Forest 1";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End else if
               else if (command == "go south")
               {
                  Console.WriteLine("You go down a path in the forest and end up back outside your house.");
                  room = "Outside of House";
                  Console.WriteLine("You are now standing outside of your house. There is a log, some");
                  Console.WriteLine("wood, and some woodcutting tools. Your house is a simple 2 story");
                  Console.WriteLine("cottage out on a plain. There is a forest to the North.");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Lost Forest 3":
               if (command == "go west")
               {
                  Console.WriteLine("Congratulations! As you go down the forest path, you see a clearing.");
                  Console.WriteLine("You soon find yourself out of the woods!");
                  room = "Troll Booth";
                  Console.WriteLine("You see a booth run by a smug looking troll. He grins and talks to you");
                  Console.WriteLine("\"That'll be " + fee + " gold. You want to cross the bridge over the lake.");
               }//End if
               else if(command == "go north" || command == "go east") {
                  room = "Lost Forest 1";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End else if
               else if(command == "go south") {
                  Console.WriteLine("You go down a path in the forest and end up back outside your house.");
                  room = "Outside of House";
                  Console.WriteLine("You are now standing outside of your house. There is a log, some");
                  Console.WriteLine("wood, and some woodcutting tools. Your house is a simple 2 story");
                  Console.WriteLine("cottage out on a plain. There is a forest to the North.");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Troll Booth":
               if (command == "go west")
               {
                  if (gateOpen == true)
                  {
                     Console.WriteLine("You go across the bridge. The Troll closes the gate behind you,");
                     Console.WriteLine("To try to sucker someone else out of their money.");
                     gateOpen = false;
                     room = "Valley";
                     Console.WriteLine("You make your way to a valley. You see a large mountain with");
                     Console.WriteLine("a path leading upwards to the North. There is also a rapidly");
                     Console.WriteLine("rushing creek blocking the way. A suspicious looking bush");
                     Console.WriteLine("is noticeable on the south end of the creek. There is also a");
                     Console.WriteLine("hint booth here...");
                  }//End if
                  else
                  {
                     Console.WriteLine("You try to cross over the gate but the Troll farts and it");
                     Console.WriteLine("smells really bad! Looks like you have to pay him or fine a");
                     Console.WriteLine("different way to get past him.");
                  }//End else
               }//End if
               else if (command == "go east") {
                  Console.WriteLine("You go back into the woods. Why you would want to do that is");
                  Console.WriteLine("anyone's guess...");
                  room = "Lost Forest 1";
                  Console.WriteLine("You are in the forest. Beams of light dance from above the forest");
                  Console.WriteLine("giving the earth below a ghastly, angelic glow. There seems to be");
                  Console.WriteLine("some writing on a nearby tree.");
               }//End if else
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Valley":
               if (command == "go north")
               {
                  if (riverCrossed == true)
                  {
                     Console.WriteLine("You make your way up to the mountain path...");
                     room = "Ravaging Riddle Ridge";
                     Console.WriteLine("You are at the Ravaging Riddle Ridge. You see an ominious,");
                     Console.WriteLine("important looking tower looming over the other side of a bridge.");
                     if (riddlesSolved == false)
                        Console.WriteLine("There is a Sphinx blocking your path.");
                  }//End if
                  else
                     Console.WriteLine("You must cross the river first!");
               }//End if
               else if (command == "go east") {
                  Console.WriteLine("You take a secret path back to the entrance of the Troll booth.");
                  room = "Troll Booth";
                  Console.WriteLine("You see a booth run by a smug looking troll. He grins and talks to you");
                  Console.WriteLine("\"That'll be " + fee + " gold. You want to cross the bridge over the lake.");
               }//End if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Ravaging Riddle Ridge":
               if (command == "go north")
               {
                  if (riddlesSolved == true)
                  {
                     Console.WriteLine("After an exciting intermission you cross the bridge and enter the tower.");
                     room = "Wizard's Tower";
                     Console.WriteLine("You are in the tower of an unknown Wizard. You see a crumbling staircase");
                     Console.WriteLine("Leading upward. You see another staircase leading downwards");
                  }//End if
                  else
                     Console.WriteLine("The Sphinx is blocking your path, you can't go that way yet!");
               }//End if
               else if (command == "go south")
               {
                  Console.WriteLine("You go back down the mountain path to the Valley.");
                  room = "Valley";
                  Console.WriteLine("You make your way to a valley. You see a large mountain with");
                  Console.WriteLine("a path leading upwards to the North. There is also a rapidly");
                  Console.WriteLine("rushing creek blocking the way. A suspicious looking bush");
                  Console.WriteLine("is noticeable on the south end of the creek. There is also a");
                  Console.WriteLine("hint booth here...");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Wizard's Tower":
               if (command == "go down" || command == "go downstairs")
               {
                  Console.WriteLine("Predictably, you fall down the stairs and shout out,");
                  Console.WriteLine("\"It's happening again!\" You fall to the bottom of the stairs when");
                  Console.WriteLine("You notice a wiry Wizard looking down at you. He chuckles wryly.");
                  Console.WriteLine("\"I told you about stairs, bro. I warned you, dog!");
                  Console.WriteLine("You crawl back on your feet. \"Well, you are invading my territory");
                  Console.WriteLine("\"and I wish not to be interrupted. You must leave now!\"");
                  Console.WriteLine("With that he raises his hands and starts doing magicky things.");
                  room = "Wizard's Basement";
               }//End if
               else if (command == "go south")
               {
                  Console.WriteLine("You go out the door of the tower and back out onto the ridge.");
                  room = "Ravaging Riddle Ridge";
                  Console.WriteLine("You are at the Ravaging Riddle Ridge. You see an ominious,");
                  Console.WriteLine("important looking tower looming over the other side of a bridge.");
               }//End else if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            case "Wizard's Basement":
               if (command == "go up" || command == "go upstairs")
               {
                  Console.WriteLine("The wizard chuckles. \"No need to worry yourself with fleeing.");
                  Console.WriteLine("Let me do the honors!\" He blasts you with magic and you fall upstairs.");
                  Console.WriteLine("You hear him yell \"Now, go on! Get outta here!\"");
                  room = "Wizard's Tower";
                  Console.WriteLine("You are in the tower of an unknown Wizard. You see a crumbling staircase");
                  Console.WriteLine("Leading upward. You see another staircase leading downwards");
               }//End if
               else
                  Console.WriteLine("You can't go that way!");
               break;
            default:
               Console.WriteLine("There's nowhere you can go from here...");
               break;
         }//End switch
      }//End GoDirection

      //Method TalkToNPC allows the player to talk to various NPCs on his quest
      public static void TalkToNPC(ref String room, ref int gold, int fee, bool gateOpen, bool riverCrossed, String name, String answer, ref bool riddlesSolved)
      {
         switch (room)
         {
            case "Troll Booth":
               if (gateOpen == false)
               {
                  Console.WriteLine("\"That'll be " + fee + " gold!\" he chuckles, before letting off a");
                  Console.WriteLine("nasty fart. You really don't want to go anywhere near him now.");
               }//End if
               else
                  Console.WriteLine("The troll motions for you to go on through.");
               break;
            case "Valley":
               if (riverCrossed == false)
               {
                  Console.WriteLine("The man speaks: \"Now wait a minute youngster!");
                  Console.WriteLine("I could give you a good hint for 50 gold! Or if you want,");
                  Console.WriteLine("I could give you a REALLY good hint for 100 gold!\"");
               }//End if
               else
                  Console.WriteLine("You shout over at the man. He waves at you and goes about his business.");
               break;
            case "Ravaging Riddle Ridge":
               if (riddlesSolved == false)
               {
                  Console.WriteLine("The Sphinx gets up from its pearch. Its voice is loud as thunder!");
                  Console.WriteLine("\"HALT! Those who wish to see the wizard must answer me these questions 3!");
                  Console.Write("Question first! What is your name?\" ");
                  name = name.ToLower();
                  answer = Console.ReadLine();
                  answer = answer.ToLower();
                  if (answer != name)
                  {
                     Console.WriteLine("The Sphinx glares at you. \"How dare you lie to me!\"");
                     Console.WriteLine("He blows you back to the Valley.");
                     room = "Valley";
                  }//End if
                  else
                  {
                     Console.Write("\"Very good! Question second! What is your quest?\" ");
                     answer = Console.ReadLine();
                     Console.WriteLine("\"So I see! But that makes me wonder what brought you here...");
                     Console.Write("Question third! What is the air-speed velocity of an unladen swallow?\" ");
                     answer = Console.ReadLine();
                     answer = answer.ToLower();
                     if (answer.Contains("african or european") == true || answer.Contains("which") == true)
                     {
                        Console.WriteLine("\"I... Don't kno- AAAAAAAAAH!\"");
                        Console.WriteLine("With that utterance the Sphinx is blown into the chasm beneath");
                        Console.WriteLine("the bridge. You are now able to cross.");
                        riddlesSolved = true;
                     }//End if
                     else
                     {
                        Console.WriteLine("\"That is incorrect!\"");
                        Console.WriteLine("With that you are blown back into the valley!");
                        room = "Valley";
                     }//End else
                  }//End else
               }//End if
               else
                  Console.WriteLine("You try to call out to the Sphinx. He utters a profanity at you very loudly.");
               break;
            case "Wizard's Basement":
               Console.WriteLine("You attempt to speak to the Wizard, but he's busy murmuring magic");
               Console.WriteLine("words... Words that sound remarkably like chicken sounds.");
               break;
            default:
               Console.WriteLine("There is no one to talk to here!");
               break;
         }//End switch
      }//End TalkToNPC
   }//End class
}//End namespace
