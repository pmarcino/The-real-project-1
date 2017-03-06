using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectAdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game G = new Game();  //initialize Player, Enemy, Item and Technique fields
            Menu Mn = new Menu();  //create a new Menu
            Player P = new Player();  //create a new Player
            Map mapCheck = new Map();  //create a new Map
            Movement movePlayer = new Movement();   //create a new Movement
            Event eventChecker = new Event();  //create a new Event

            Enemy[] Enemies = new Enemy[37];  //create an array of Enemy(ies)
            Item[] Items = new Item[25];  //create an array of Item(s)
            Item[] eItems = new Item[25];  //create an array of Enemy Item(s)
            Room[,] map = new Room[11, 20];  //create a multidimensional array of Rooms(s)

            mapCheck.initializeMap(map);  //set pre-determined Map fields
            G.InitializePlayer(P, map);  //set pre-determined Player fields
            G.InitializeEnemy(Enemies);  //set pre-determined Enemy and Enemy Item fields
            G.InitializeItem(Items, eItems);  //set pre-determined Item fields
            G.InitializeTechnique(Items, eItems);  //set pre-determined Technique fields

            Console.WriteLine("Prepare to embark on ...");
            Console.WriteLine();
            Console.WriteLine("                           \\   /       ____                     |      /\\        |                   |                 ");
            Console.WriteLine("                            \\ / ___   |       __  __   ___      |     /  \\       |       ___   ___  _|_        __  ___ ");
            Console.WriteLine("                             | |___|  |  __  |   ___| |   |  ___|    /____\\   ___| \\  / |___| |   |  |  |   | |   |___|");
            Console.WriteLine("                             | |___   |____| |  |___| |   | |___|   /      \\ |___|  \\/  |___  |   |  |  |___| |   |___ ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        Imagined and Coded by Patrick Marcino and Sasha Dorval");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("What beith your name great warrior?");

            P.PlayerName = Console.ReadLine();
            Console.Clear();

            string selementPick;
            int elementPick;
            int Parser;

            Console.WriteLine("Which Element will embrace your inner being?");
            do
            {  //while a legitimate Element has not been selected
                Console.WriteLine("1. Fire");
                Console.WriteLine("2. Air");
                Console.WriteLine("3. Earth");
                Console.WriteLine("4. Water");
                selementPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(selementPick, out Parser) == false)  //if string variable doesn't Parse to an int..
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    elementPick = 0;  //set int variable to default case
                }
                else  //string variable is an int
                {
                    elementPick = int.Parse(selementPick);  //set int variable to parsed string variable
                }
                switch (elementPick)  //set PlayerElement based on input
                {
                    case 1:
                        P.PlayerElement = "Fire";
                        break;
                    case 2:
                        P.PlayerElement = "Air";
                        break;
                    case 3:
                        P.PlayerElement = "Earth";
                        break;
                    case 4:
                        P.PlayerElement = "Water";
                        break;
                    default:
                        Console.WriteLine("Thy must attempt said grande feat again!");
                        Console.WriteLine();
                        break;
                }
            } while (elementPick < 1 || elementPick > 4);
            Console.Clear();

            Console.WriteLine("Ooh, {0} the {1} Adventurer be a cringeworthy candidate for this great escapade.", P.PlayerName, P.PlayerElement);
            Console.WriteLine();
            Console.WriteLine("With what weapon does {0} stain the earth with most success?", P.PlayerName);
            Console.WriteLine();

            string sweaponPick;
            int weaponPick;
            do
            {
                Console.WriteLine("1. Ze Rusty Sword of Dull Edges");
                Console.WriteLine("2. Ze Worn Bow of Not-so-Elasticity");
                Console.WriteLine("3. Ze Overly Heavy Mace of Frilly Tassels");
                sweaponPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(sweaponPick, out Parser) == false)
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    weaponPick = 0;
                }
                else
                {
                    weaponPick = int.Parse(sweaponPick);
                }
                switch (weaponPick)
                {
                    case 1:
                        P.PlayerInventory.Add(Items[0]);
                        break;
                    case 2:
                        P.PlayerInventory.Add(Items[1]);
                        break;
                    case 3:
                        P.PlayerInventory.Add(Items[2]);
                        break;
                    default:
                        Console.WriteLine("Thy must attempt said grande feat again!");
                        Console.WriteLine();
                        break;
                }
            } while (weaponPick < 1 || weaponPick > 3);
            Console.Clear();

            P.PlayerInventory[0].ItemEquipped = true;
            P.PlayerInventory[0].ItemTechnique[0].TechniqueEquipped = true;

            Console.WriteLine("{0} shall wield {1}! It has been equipped as your current weapon.", P.PlayerName, P.PlayerInventory[0].ItemName);
            Console.ReadKey();
            Console.Clear();

            while (P.hasEscaped != true || P.PlayerCurrentHealth > 0) //do while the Exit condition is not true in the player class 
            {
                Mn.Interactions(P, map, mapCheck, movePlayer, Items);
                eventChecker.determineEvent(G, map, P, Enemies, eItems, Items);
            }
            Console.WriteLine("Congratulations on Ye Grande Adventure!");
            Console.ReadKey();
        }
    }
    class Game
    {
        public void InitializePlayer(Player P, Room[,] M)
        {
            P.PlayerFullHealth = 100;
            P.PlayerCurrentHealth = 100;
            P.PlayerInventory = new List<Item>();
            P.Xcoordinate = M[0, 0].Xcoordinate;
            P.Ycoordinate = M[0, 0].Ycoordinate;
            P.roomNum = M[0, 0].roomNum;
        }
        public void InitializeEnemy(Enemy[] Enemies)
        {
            string[] EnemyName = { "Average Joe", "Chipmunk", "Buncha Snakes", "Sharp-Toothed Fish", "Condor", "Lava Lizard", "Living Avalanche", "NastyGuy", "EvilThing", "Flan", "Stinky Wheel of Cheese", "Skeleton Dragon", "Minatour", "Lich", "Mud Troll", "Harpy", "Chucky", "Super Mega Dragon", "Sentient Bomb", "Jack-The-Ripper", "Lurch", "Rat King", "Pin Wheel", "Spiky Urchin", "Demon Cyborg", "Rotten Turkey", "Lizard Man", "Skeleton", "Bunch of Assassins", "Gang of Spiders", "Spider Queen", "Medusa", "Hell Hound", "Blue Demon", "Red Demon", "Black Demon", "Gilgamesh" };
            string[] EnemyElement = { "Non-Elemental", "Earth", "Earth", "Water", "Air", "Fire", "Earth", "Air", "Water", "Air", "Water", "Air", "Earth", "Water", "Earth", "Air", "Earth", "Fire", "Fire", "Fire", "Earth", "Water", "Water", "Water", "Fire", "Earth", "Water", "Water", "Air", "Non-Elemental", "Earth", "Water", "Fire", "Water", "Fire", "Earth", "Non-Elemental" };
            int[] EnemyHealth = { 30, 10, 60, 26, 55, 60, 120, 100, 220, 60, 22, 40, 50, 33, 10, 35, 55, 88, 120, 250, 33, 245, 27, 53, 88, 56, 10, 100, 12, 120, 44, 38, 45, 100, 140, 250, 400 };
            int[] EnemyAgility = { 1, 3, 4, 2, 6, 5, 8, 9, 6, 10, 8, 3, 6, 1, 3, 5, 2, 7, 10, 9, 6, 2, 4, 4, 6, 4, 6, 8, 9, 10, 3, 4, 2, 8, 10, 2, 10 };

            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = new Enemy();
                Enemies[i].EnemyName = EnemyName[i];
                Enemies[i].EnemyElement = EnemyElement[i];
                Enemies[i].EnemyFullHealth = EnemyHealth[i];
                Enemies[i].EnemyCurrentHealth = EnemyHealth[i];
                Enemies[i].EnemyAgility = EnemyAgility[i];
                Enemies[i].EnemyID = i;
            }
        }
        public void InitializeItem(Item[] Items, Item[] eItems)  //sets fields of each Item
        {
            //initialize Player Items
            string[] ItemName = { "Ze Rusty Sword of Dull Edges", "Ze Worn Bow of Not-so-Elasticity", "Ze Overly Heavy Mace of Frilly Tassels", "Cup of Brimming Liquids", "Fish", "Beer", "Health Charm", "Liquid Breastplate", "Shield of Flaming Bits", "Health Potion", "Sword of Truth", "Slingshot", "Book of Blasphemy", "Healthy Snack", "26", "Unforgiving Twang", "SkullCrusher", "Shield of Glass", "Book of Incredibly Accurate Lightning Strike", "The Eagles", "Book of Defensive Tactics", "Suit of Heavy Metal", "Lava in a Bottomless Bucket", "Advil", "Book of Exemplary Healing" };  //item name array
            bool[] ItemDurability = { true, true, true, true, false, false, true, true, true, false, true, true, true, false, false, true, true, true, true, true, true, true, true, false, true };  //item durability array      'one-time-use' Durability = false    'permanent' Durability = true
            bool ItemActive = true;  //item active variable              'out of play' Active = false         'item in play' Active = true                                                                                                                                >>//TECHNIQUES must be created for THESE ITEMS >>
            bool ItemEquipped = false;  //item equipped variable         'not equipped' Equipped = false      'equipped' Equipped = true

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new Item();
                Items[i].ItemName = ItemName[i];
                Items[i].ItemDurability = ItemDurability[i];
                Items[i].ItemActive = ItemActive;
                Items[i].ItemEquipped = ItemEquipped;
            }
            //initialize Enemy Items
            string[] eItemName = { "Spikey Mace", "Bow of the Winds", "A Sharp Rock", "Flamethrower", "Chinese Water Torture", "Sun Sword", "The Meaning of Life", "Tornado Staff", "Rain Dance", "Bag of Bombs", "Raunchy Smells in a Jar", "Bone Flail", "Claws", "Magical Inconvenience", "Pillar of Flame", "Long Blade", "Pokey Bits", "Feces Fling", "Tazer", "Multi-Blade", "Slap Chop", "Lotsa Legs", "Stink Eye", "Ultimate Attack", "Disappointment" };
            bool eItemDurability = true;
            bool eItemActive = true;
            bool eItemEquipped = false;

            for (int i = 0; i < eItems.Length; i++)
            {
                Item eItem = new Item();
                eItem.ItemName = eItemName[i];
                eItem.ItemID = i;
                eItem.ItemDurability = eItemDurability;
                eItem.ItemActive = eItemActive;
                eItem.ItemEquipped = eItemEquipped;
                eItems[i] = eItem;  //add new Item to eItems array
            }
        }
        public void InitializeTechnique(Item[] Items, Item[] eItems)
        {
            //initialize Player Item Techniques  
            string[] TechniqueName = { "LockJaw Flesh Wound", "Split Toenail", "Rainblow", "Wet Face", "Fish-Slap", "Beers & Cards", "Heal", "WaterDefend", "FireDefend", "Heal", "Rage", "Pelt", "Shred", "Numnumnum", "Drink Up Me Hearties", "Thwump", "Bits o' Head", "Fractured Defence", "Harry Notter", "THE EAGLES ARE COMING!", "300", "Iron Giant", "Globular Pain", "Pain & Sinus Relief", "Health+" };
            int[] TechniqueValue = { 20, 20, 20, 1, 2, 150, 50, 10, 20, 100, 123, 25, 250, 175, 25, 90, 135, 5, 260, 500, 40, 50, 66, 320, 111 };  //Technique value array
            string[] TechniqueElement = { "Fire", "Air", "Water", "Water", "Water", "Non-Elemental", "Non-Elemental", "Fire", "Fire", "Non-Elemental", "Air", "Air", "Earth", "Non-Elemental", "Fire", "Water", "Earth", "Air", "Air", "Air", "Earth", "Water", "Fire", "Non-Elemental", "Non-Elemental" }; //Technique element array
            string[] TechniqueType = { "Damage", "Damage", "Damage", "Damage", "Damage", "Heal", "Heal", "Defend", "Defend", "Heal", "Damage", "Damage", "Damage", "Heal", "Heal", "Damage", "Damage", "Defend", "Damage", "Damage", "Defend", "Defend", "Damage", "Heal", "Heal" };  //Technique type array  Damage, Heal, Defend, (Stat?)
            int[] TechniqueCoolDown = { 1, 1, 1, 1, 1, 1, 2, 1, 1, 3, 1, 1, 5, 1, 1, 2, 2, 1, 4, 8, 1, 1, 1, 3, 1 };
            bool TechniqueEquipped = false;

            for (int i = 0; i < Items.Length; i++)
            {
                Technique itemTechnique = new Technique();
                itemTechnique.TechniqueName = TechniqueName[i];
                itemTechnique.TechniqueValue = TechniqueValue[i];
                itemTechnique.TechniqueElement = TechniqueElement[i];
                itemTechnique.TechniqueType = TechniqueType[i];
                itemTechnique.TechniqueCoolDown = TechniqueCoolDown[i];
                itemTechnique.CurrentCoolDown = TechniqueCoolDown[i];
                itemTechnique.TechniqueEquipped = TechniqueEquipped;
                Items[i].ItemTechnique = new List<Technique>();
                Items[i].ItemTechnique.Add(itemTechnique);
            }
            //initialize Enemy Item Techniques
            string[] eTechniqueName = { "Crunchy Crunch", "Biting Breeze", "Rib Jab", "Bonfire", "Drip", "Solar Fling", "Smoke Monster", "Tornado", "Unexpected Hail Storm", "Light the Bag of Bombs", "Stench", "Pointy Bone Slicer", "Jagged Rip", "10,000 Super Weak Water Balls", "Inferno", "Lengthly Cut", "Jabby Jab", "Crap", "Zzap", "Multi-Cuts", "Two Free Graty's!", "Many Kicks", "Poo with a View", "Insta-Screwed", "Disatisfaction" };
            string[] eTechniqueElement = { "Non-Elemental", "Air", "Earth", "Fire", "Water", "Fire", "Non-Elemental", "Air", "Water", "Fire", "Earth", "Earth", "Non-Elemental", "Water", "Fire", "Non-Elemental", "Non-Elemental", "Earth", "Air", "Air", "Fire", "Earth", "Earth", "Water", "Water" };
            int[] eTechniqueValue = { 8, 7, 4, 32, 9, 48, 42, 32, 18, 86, 12, 34, 36, 4, 46, 28, 13, 62, 88, 99, 3, 14, 33, 160, 1 };
            int[] eTechniqueCoolDown = { 1, 1, 1, 2, 1, 2, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 1, 1, 1, 2, 1 };
            int[] eCurrentCoolDown = { 1, 1, 1, 2, 1, 2, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 1, 1, 1, 2, 1 };
            string eTechniqueType = "Damage";

            for (int i = 0; i < eItems.Length; i++)
            {
                Technique eTechnique = new Technique();
                eTechnique.TechniqueName = eTechniqueName[i];
                eTechnique.TechniqueElement = eTechniqueElement[i];
                eTechnique.TechniqueValue = eTechniqueValue[i];
                eTechnique.TechniqueType = eTechniqueType;
                eTechnique.TechniqueCoolDown = eTechniqueCoolDown[i];
                eTechnique.CurrentCoolDown = eCurrentCoolDown[i];
                eItems[i].ItemTechnique = new List<Technique>();  //create new list of Technique(s) for new Item
                eItems[i].ItemTechnique.Add(eTechnique);  //add new Technique to Technique list
            }
        }
        public void InitializeBattle(Player P, Enemy[] Enemies, Item[] eItems)
        {
            Battle B = new Battle();
            Random R = new Random();

            //set Player variables
            B.currentPlayer = P;
            B.currentItem = B.currentPlayer.PlayerInventory.Single(Item => Item.ItemEquipped == true && Item.ItemTechnique[0].TechniqueType == "Damage");
            B.currentTechnique = B.currentItem.ItemTechnique.Single(Technique => Technique.TechniqueEquipped == true);
            //set Enemy variables
            Enemy cEnemy = Enemies[R.Next(10)];  //select a random Enemy
            B.currentEnemy = cEnemy;
            B.currentEnemy.EnemyItem = eItems[R.Next(10)];  //select a random Item for the Enemy
            B.currentEnemy.EnemyTechnique = B.currentEnemy.EnemyItem.ItemTechnique[R.Next(B.currentEnemy.EnemyItem.ItemTechnique.Count)];  //select a random Technique of the selected Item

            int[] enemyAgility = new int[10];  //create an array for Enemy agility
            int count;

            Console.WriteLine("All of a sudden, ye come in contact with {0} the {1} type wielding {2}! PREPARE YOURSELF!", B.currentEnemy.EnemyName, B.currentEnemy.EnemyElement, B.currentEnemy.EnemyItem.ItemName);
            Console.ReadKey();

            //
            for (count = 0; count < B.currentEnemy.EnemyAgility; count++)  //fill enemyAgility with the number of 1's that correspond with the Enemies EnemyAgility
            {
                enemyAgility[count] = 1;
            }
            for (int count2 = count; count2 < enemyAgility.Length - 1; count2++)  //fill the remainder of enemyAgility with 0's
            {
                enemyAgility[count2] = 0;
            }
            if (enemyAgility[R.Next(10)] == 1)  //if 1 is selected..
            {
                //the enemy attacks first
                Console.WriteLine("{0} is faster than you and makes the first move!", B.currentEnemy.EnemyName);
                Console.ReadKey();
                Console.Clear();
                B.Turn = false;
                B.EnemyAttack();
            }
            else  //0 is selected
            {
                //the player attacks first
                Console.WriteLine("You're on your game and make the first move!");
                Console.ReadKey();
                Console.Clear();
                B.Turn = true;
                B.PlayerOptions();
            }
        }
    }
    class Menu
    {
        public void Interactions(Player P, Room[,] M, Map Mp, Movement Mv, Item[] Items)
        {
            string sinteractionPick;
            int interactionPick;
            int Parser;

            do
            {
                Console.WriteLine("What beith thy decision {0}?", P.PlayerName);
                Console.WriteLine("1. Move to a new room");
                Console.WriteLine("2. Rummage through thy Inventory");
                Console.WriteLine("3. View ze Map");

                sinteractionPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(sinteractionPick, out Parser) == false)
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    interactionPick = 0;
                }
                else
                {
                    interactionPick = int.Parse(sinteractionPick);
                }
                switch (interactionPick)
                {
                    case 1:
                        Mv.Move(M, P);
                        break;
                    case 2:
                        InventoryAccess(P, Items);
                        break;
                    case 3:
                        Mp.printMap(M, P);
                        break;
                    default:
                        Console.WriteLine("Thy must attempt said grande feat again!");
                        Console.WriteLine();
                        break;
                }
            } while (interactionPick != 1);
            Console.Clear();
        }
        public void InventoryAccess(Player P, Item[] Items)
        {
            Item selectedItem = new Item();
            string sitemPick;
            int itemPick;
            int Parser;

            do
            {
                Console.WriteLine("Which item doth thou wish to use?");
                int count = 1;
                foreach (Item I in P.PlayerInventory)
                {
                    Console.WriteLine("{0}. {1}", count, I.ItemName);
                    count++;
                }
                sitemPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(sitemPick, out Parser) == false)
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    itemPick = 0;
                }
                else
                {
                    itemPick = int.Parse(sitemPick);
                }
                try
                {
                    selectedItem = P.PlayerInventory[itemPick - 1];  //selected Item = menu number - 1 of Player Inventory
                }
                catch
                {
                    Console.WriteLine("You do not have that many items in your inventory!");
                }
            }
            while (itemPick < 1 || itemPick > P.PlayerInventory.Count);
            Console.Clear();

            if (selectedItem.ItemTechnique[0].TechniqueType == "Damage" || selectedItem.ItemTechnique[0].TechniqueType == "Defend")
            {
                string sitemEquip;
                int itemEquip;

                do
                {
                    if (selectedItem.ItemEquipped == true)
                    {
                        Console.WriteLine("Would you like to unequip {0}?", selectedItem.ItemName);
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                    }
                    else
                    {
                        Console.WriteLine("Would you like to equip {0}?", selectedItem.ItemName);
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                    }
                    sitemEquip = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(sitemEquip, out Parser) == false)
                    {
                        Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                        Console.WriteLine();
                        itemEquip = 0;
                    }
                    else
                    {
                        itemEquip = int.Parse(sitemEquip);
                        if (itemEquip == 1 && selectedItem.ItemEquipped == false)
                        {
                            if (selectedItem.ItemTechnique[0].TechniqueType == "Damage")
                            {
                                Item I = P.PlayerInventory.Single(Item => Item.ItemEquipped == true && Item.ItemTechnique[0].TechniqueType == "Damage");  //set variable to placeholder of equipped "Damage"Item
                                I.ItemTechnique.Single(Technique => Technique.TechniqueEquipped == true).TechniqueEquipped = false;  //set currently equipped Technique to unequipped
                                I.ItemEquipped = false;  //set currently equipped Item to unequipped
                                selectedItem.ItemEquipped = true;  //equip selected Item
                                selectedItem.ItemTechnique[0].TechniqueEquipped = true;  //equip first Technique of equipped Item
                            }
                            else if (selectedItem.ItemTechnique[0].TechniqueType == "Defend")
                            {
                                selectedItem.ItemEquipped = true;
                            }
                        }
                        else if (itemEquip == 1 && selectedItem.ItemEquipped == true)
                        {
                            if (selectedItem.ItemTechnique[0].TechniqueType == "Damage")
                            {
                                selectedItem.ItemEquipped = false;
                                foreach (Technique T in selectedItem.ItemTechnique)
                                {
                                    T.TechniqueEquipped = false;  //unequip Technique of selected Item
                                }
                                Console.WriteLine("For ye own safety, {0} has been equipped..", P.PlayerInventory[0].ItemName);
                                P.PlayerInventory[0].ItemEquipped = true;
                                P.PlayerInventory[0].ItemTechnique[0].TechniqueEquipped = true;
                                Console.ReadKey();
                            }
                            else if (selectedItem.ItemTechnique[0].TechniqueType == "Defend")
                            {
                                selectedItem.ItemEquipped = false;
                            }
                        }
                    }
                }
                while (itemEquip < 1 || itemEquip > 2);
                Console.Clear();
            }
            else if (selectedItem.ItemTechnique[0].TechniqueType == "Heal")
            {
                string sitemHeal;
                int itemHeal;

                do
                {
                    Console.WriteLine("Would you like to heal {0} Health with {1}?", selectedItem.ItemTechnique[0].TechniqueValue, selectedItem.ItemName);
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    sitemHeal = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(sitemHeal, out Parser) == false)
                    {
                        Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                        Console.WriteLine();
                        itemHeal = 0;
                    }
                    else
                    {
                        itemHeal = int.Parse(sitemHeal);
                        if (itemHeal == 1)
                        {
                            P.PlayerCurrentHealth = P.PlayerCurrentHealth + selectedItem.ItemTechnique[0].TechniqueValue;
                            if (selectedItem.ItemDurability == false)
                            {
                                Console.WriteLine("{0} is used up and has been tossed from your inventory.", selectedItem.ItemName);
                                P.PlayerInventory.Remove(selectedItem);  //remove selected Item
                            }
                            if (P.PlayerCurrentHealth > P.PlayerFullHealth)
                            {
                                P.PlayerCurrentHealth = P.PlayerFullHealth;
                            }
                            Console.WriteLine("{0}'s Health is now {1}/{2}.", P.PlayerName, P.PlayerCurrentHealth, P.PlayerFullHealth);
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                while (itemHeal < 1 || itemHeal > 2);
                Console.Clear();
            }
        }
    }
    class Player
    {
        private string pName;  //holds the Player's name
        private string pElement;  //holds the Player's element
        private int pFullHealth;  //holds the Player's maximum health
        private int pCurrentHealth;  //holds the Player's remaining health
        private int xCoordinate, yCoordinate, roomPosition;  //holds the Player's current location variables
        private bool Escaped;  //holds the Player's freedom status
        public List<Item> PlayerInventory;  //holds the Player's inventory of Items

        public string PlayerName  //gets or sets the player Name field
        {
            get { return pName; }
            set { pName = value; }
        }
        public string PlayerElement  //gets or sets the player Element field
        {
            get { return pElement; }
            set { pElement = value; }
        }
        public int PlayerFullHealth  //gets or sets the player's maximum health via the FullHealth field
        {
            get { return pFullHealth; }
            set { pFullHealth = value; }
        }
        public int PlayerCurrentHealth  //gets or sets the player's CurrentHealth field
        {
            get { return pCurrentHealth; }
            set { pCurrentHealth = value; }
        }
        public int Xcoordinate  //gets or sets the player's current X coordinate on the map
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }
        public int Ycoordinate  //gets or sets the player's current Y coordinate on the map
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }
        public int roomNum  //gets or sets the player's current room number on the map
        {
            get { return roomPosition; }
            set { roomPosition = value; }
        }
        public bool hasEscaped  //gets or sets the player's current freedom status 
        {
            get { return Escaped; }
            set { Escaped = value; }
        }
    }
    class Enemy
    {
        private string eName;
        private string eElement;
        private int eFullHealth;
        private int eCurrentHealth;
        private int eID;  //holds a value referenced by the roomSection class to be picked for a specific room direction
        private int eAgility;  //holds the chance out of 10 that an Enemy will attack before the Player
        private Item eItem;  //holds the current Item used by an Enemy
        private Technique eTechnique;  //holds the current Technique used by an Enemy(ies) current Item

        public string EnemyName
        {
            get { return eName; }
            set { eName = value; }
        }
        public string EnemyElement
        {
            get { return eElement; }
            set { eElement = value; }
        }
        public int EnemyFullHealth
        {
            get { return eFullHealth; }
            set { eFullHealth = value; }
        }
        public int EnemyCurrentHealth
        {
            get { return eCurrentHealth; }
            set { eCurrentHealth = value; }
        }
        public int EnemyID
        {
            get { return eID; }
            set { eID = value; }
        }
        public int EnemyAgility
        {
            get { return eAgility; }
            set { eAgility = value; }
        }
        public Item EnemyItem
        {
            get { return eItem; }
            set { eItem = value; }
        }
        public Technique EnemyTechnique
        {
            get { return eTechnique; }
            set { eTechnique = value; }
        }
    }
    class Item
    {
        private string iName;  //holds the name of an item
        private int iID;  //holds a value referenced by the RoomSection class to be picked for a specific room direction 
        private bool iDurability;  //if an item is considered 'one-time-use', Durability = false, if an item is 'permanent', Durability = true
        private bool iActive;  //if an item is in play, Active = true, if an item is out of play (ie. 'one-time-use' and used up), Active = false
        private bool iEquipped;  //if an item is equipped by the Player, Equipped = true, otherwise Equipped = false
        private List<Technique> iTechnique;  //holds the Technique a particular Item can invoke

        public string ItemName  //sets the Name field
        {
            get { return iName; }
            set { iName = value; }
        }
        public int ItemID  //sets the Location field
        {
            get { return iID; }
            set { iID = value; }
        }
        public bool ItemDurability  //sets the Durability field
        {
            get { return iDurability; }
            set { iDurability = value; }
        }
        public bool ItemActive  //sets the Active field
        {
            get { return iActive; }
            set { iActive = value; }
        }
        public bool ItemEquipped
        {
            get { return iEquipped; }
            set { iEquipped = value; }
        }
        public List<Technique> ItemTechnique
        {
            get { return iTechnique; }
            set { iTechnique = value; }
        }
    }
    class Technique
    {
        private string tName;  //holds the name of an Attack
        private int tValue;  //holds the value a Technique can remove from health (Damage), give to health (Heal) or remove from enemy damage (Defend) 
        private string tElement;  //holds the element of a Technique ("Fire", "Air", "Water" or "Earth")
        private string tType;  //holds the type of a Technique ("Damage", "Heal", "Defend" or "Stat")
        private bool tEquipped;  //holds 'true' when currently selected as the Item(s) active Technique, 'false' otherwise
        private int tCoolDown;  //holds the time (ie. Rounds) a Technique takes before it can be used again
        private int tcurrentCoolDown;  //holds the comparable value of tCoolDown

        public string TechniqueName
        {
            get { return tName; }
            set { tName = value; }
        }
        public int TechniqueValue
        {
            get { return tValue; }
            set { tValue = value; }
        }
        public string TechniqueElement
        {
            get { return tElement; }
            set { tElement = value; }
        }
        public string TechniqueType
        {
            get { return tType; }
            set { tType = value; }
        }
        public bool TechniqueEquipped
        {
            get { return tEquipped; }
            set { tEquipped = value; }
        }
        public int TechniqueCoolDown
        {
            get { return tCoolDown; }
            set { tCoolDown = value; }
        }
        public int CurrentCoolDown
        {
            get { return tcurrentCoolDown; }
            set { tcurrentCoolDown = value; }
        }
    }
    class Event
    {
        public void determineEvent(Game G, Room[,] map, Player P, Enemy[] Enemies, Item[] eItems, Item[] Items)
        {
            Random R = new Random();
            if (map[P.Xcoordinate, P.Ycoordinate].markRoomVisited == false)
            {
                switch (map[P.Xcoordinate, P.Ycoordinate].Event)
                {
                    case 0:
                        Console.WriteLine("YE find yerself in an empty room");
                        Console.ReadKey();
                        break;
                    case 1:
                        G.InitializeBattle(P, Enemies, eItems);
                        break;
                    case 2:
                        itemEvent(P, Items);
                        break;
                }
            }
            map[P.Xcoordinate, P.Ycoordinate].markRoomVisited = true;
        }
        public void itemEvent(Player P, Item[] I)
        {
            string sitemPick;
            int itemPick;
            int Parser;

            Random R = new Random();
            Item foundItem = I[R.Next(I.Length - 1)];
            do
            {
                Console.WriteLine("Ho there likkle Item! Looks like ye hath stumbled upon a {0}! Doth thou wish to pick it up?", foundItem.ItemName);
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                sitemPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(sitemPick, out Parser) == false)
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    itemPick = 0;
                }
                else
                {
                    itemPick = int.Parse(sitemPick);
                }
                switch (itemPick)
                {
                    case 1:
                        P.PlayerInventory.Add(foundItem);
                        Console.WriteLine("{0} hath been added to your inventory.", foundItem.ItemName);
                        break;
                    case 2:
                        Console.WriteLine("{0} sheds a tear as you make a foolish mistake..", foundItem.ItemName);
                        break;
                    default:
                        Console.WriteLine("Thy must attempt said grande feat again!");
                        Console.WriteLine();
                        break;
                }
            } while (itemPick < 1 || itemPick > 2);
            Console.ReadKey();
            Console.Clear();
        }
    }
    class Movement
    {
        bool directionCheck = false;  //ensures a Player has moved to a valid room
        public void Move(Room[,] map, Player P)
        {
            int x = P.Xcoordinate;  //Record the players X coordinate before they select an action
            int y = P.Ycoordinate;  //Record the players Y coordinate before they select an action

            do
            {
                Console.Clear();
                Console.WriteLine("Which direction are ye going to move?");
                Console.Write("[W]:Up   [S]:Down   [A]:Left  [D]:Right   [X]:Cancel \t ::"); //player can type either the single 
                string moveChoice = Console.ReadLine();                                      //character representing a movement 
                try                                                                          //direction or type in the direction they wish to go
                {
                    if (moveChoice.ToUpper() == "W" || moveChoice.ToUpper() == "UP")
                    {
                        P.Xcoordinate = P.Xcoordinate + 1;
                        moveCheck(map, P, x, y);
                    }
                    else if (moveChoice.ToUpper() == "S" || moveChoice.ToUpper() == "DOWN")
                    {
                        P.Xcoordinate = P.Xcoordinate - 1;
                        moveCheck(map, P, x, y);
                    }
                    else if (moveChoice.ToUpper() == "A" || moveChoice.ToUpper() == "LEFT")
                    {
                        P.Ycoordinate = P.Ycoordinate - 1;
                        moveCheck(map, P, x, y);
                    }
                    else if (moveChoice.ToUpper() == "D" || moveChoice.ToUpper() == "RIGHT")
                    {
                        P.Ycoordinate = P.Ycoordinate + 1;
                        moveCheck(map, P, x, y);
                    }
                    else if (moveChoice == "X") { } //return to the menu 
                    else
                    {
                        Console.WriteLine("What did ye say?");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Ye walk into a wall!");
                    P.Xcoordinate = x;
                    P.Ycoordinate = y;
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (directionCheck == false);

        }
        public void moveCheck(Room[,] map, Player P, int x, int y)
        {
            if (map[P.Xcoordinate, P.Ycoordinate].wallCheck == true)
            {
                Console.WriteLine("Ye walk into a wall!");
                P.Ycoordinate = y;
                P.Xcoordinate = x;
                Console.ReadKey();
                Console.Clear();
            }
            else  //wallCheck = false
            {
                P.roomNum = map[P.Xcoordinate, P.Ycoordinate].roomNum;
                P.hasEscaped = map[P.Xcoordinate, P.Ycoordinate].Exit;
                directionCheck = true;
            }
        }
    }
    class Room
    {
        private bool visitedRoom;
        private bool isWall;
        private int eventId; //0 = nothing     1=item       2=battle
        private bool isExit;
        private int xCoordinate, yCoordinate, roomPosition;
        public int roomNum
        {
            get { return roomPosition; }
            set { roomPosition = value; }
        }
        public int Xcoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }
        public int Ycoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }
        public bool wallCheck
        {
            get { return isWall; }
            set { isWall = value; }
        }
        public bool markRoomVisited
        {
            get { return visitedRoom; }
            set { visitedRoom = value; }
        }
        public bool Exit
        {
            get { return isExit; }
            set { isExit = value; }
        }
        public int Event
        {
            get { return eventId; }
            set { eventId = value; }
        }
    }
    class Map
    {
        public void initializeMap(Room[,] map)
        {
            int count = 0;
            Random D = new Random();
            for (int x = 0; x < map.GetLength(0); x++)    //populating the room array with rooms
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Room R = new Room();
                    map[x, y] = R;
                }
            }

            setWalls(map);    //Initializing the dungeon walls
            for (int x = 0; x < map.GetLength(0); x++)
            {
                //for (int y = M.GetLength(1) - 1; y >= 0; y--)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].wallCheck != true)
                    {
                        map[x, y].wallCheck = false;
                        map[x, y].markRoomVisited = false;
                        map[x, y].Xcoordinate = x;
                        map[x, y].Ycoordinate = y;
                        map[x, y].roomNum = count;
                        setEvent(map[x, y], D);
                        count++;
                    }
                }
            }
        }
        public void setWalls(Room[,] map)
        {
            map[10, 19].Exit = true;    //Initialize the dungeon exit
            map[0, 2].wallCheck = true;
            map[1, 2].wallCheck = true;
            map[1, 5].wallCheck = true;
            map[1, 6].wallCheck = true;
            map[1, 8].wallCheck = true;
            map[1, 9].wallCheck = true;
            map[1, 10].wallCheck = true;
            map[1, 11].wallCheck = true;
            map[1, 12].wallCheck = true;
            map[1, 13].wallCheck = true;
            map[1, 14].wallCheck = true;
            map[1, 15].wallCheck = true;
            map[1, 16].wallCheck = true;
            map[1, 17].wallCheck = true;
            map[2, 2].wallCheck = true;
            map[2, 3].wallCheck = true;
            map[2, 4].wallCheck = true;
            map[2, 5].wallCheck = true;
            map[2, 6].wallCheck = true;
            map[2, 8].wallCheck = true;
            map[3, 8].wallCheck = true;
            map[3, 11].wallCheck = true;
            map[3, 12].wallCheck = true;
            map[3, 13].wallCheck = true;
            map[3, 14].wallCheck = true;
            map[3, 15].wallCheck = true;
            map[3, 16].wallCheck = true;
            map[3, 17].wallCheck = true;
            map[3, 18].wallCheck = true;
            map[3, 19].wallCheck = true;
            map[4, 0].wallCheck = true;
            map[4, 1].wallCheck = true;
            map[4, 2].wallCheck = true;
            map[4, 3].wallCheck = true;
            map[4, 4].wallCheck = true;
            map[4, 5].wallCheck = true;
            map[4, 6].wallCheck = true;
            map[4, 8].wallCheck = true;
            map[5, 0].wallCheck = true;
            map[5, 5].wallCheck = true;
            map[5, 6].wallCheck = true;
            map[5, 8].wallCheck = true;
            map[5, 9].wallCheck = true;
            map[5, 10].wallCheck = true;
            map[5, 11].wallCheck = true;
            map[5, 13].wallCheck = true;
            map[5, 14].wallCheck = true;
            map[5, 15].wallCheck = true;
            map[5, 17].wallCheck = true;
            map[5, 18].wallCheck = true;
            map[6, 0].wallCheck = true;
            map[6, 2].wallCheck = true;
            map[6, 3].wallCheck = true;
            map[6, 4].wallCheck = true;
            map[6, 5].wallCheck = true;
            map[6, 6].wallCheck = true;
            map[6, 8].wallCheck = true;
            map[7, 8].wallCheck = true;
            map[7, 9].wallCheck = true;
            map[7, 11].wallCheck = true;
            map[7, 12].wallCheck = true;
            map[7, 14].wallCheck = true;
            map[7, 15].wallCheck = true;
            map[7, 16].wallCheck = true;
            map[7, 18].wallCheck = true;
            map[7, 19].wallCheck = true;
            map[8, 2].wallCheck = true;
            map[8, 3].wallCheck = true;
            map[8, 4].wallCheck = true;
            map[8, 5].wallCheck = true;
            map[8, 6].wallCheck = true;
            map[8, 7].wallCheck = true;
            map[8, 8].wallCheck = true;
            map[8, 9].wallCheck = true;
            map[8, 14].wallCheck = true;
            map[9, 2].wallCheck = true;
            map[9, 6].wallCheck = true;
            map[9, 10].wallCheck = true;
            map[9, 14].wallCheck = true;
            map[9, 18].wallCheck = true;
            map[9, 19].wallCheck = true;
            map[10, 4].wallCheck = true;
            map[10, 8].wallCheck = true;
            map[10, 12].wallCheck = true;
            map[10, 16].wallCheck = true;
        }
        public void setEvent(Room map, Random D)
        {
            int[] randBank = { 0, 0, 0, 0, 0, 1, 1, 1, 2, 2 };
            map.Event = randBank[D.Next(10)];
        }
        public void printMap(Room[,] map, Player P)
        {
            Console.WriteLine("[x]=[YOU] \t [0]=[Visited Room] \t [+] = wall/unvisited room");
            for (int x = map.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = 0; y <= map.GetLength(1) - 1; y++)
                {
                    if (map[x, y].wallCheck == true)    //will print a '+' if wallCheck comes back true
                    {
                        Console.Write("W");
                    }
                    else if (P.roomNum == map[x, y].roomNum)  //will print out a 'X' if the players room number matches with the roomms room number
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("+");
                    }

                }
                Console.WriteLine();
            }
        }
    }
    class Battle
    {
        private Player P;  //holds the current Player doing battle
        private Enemy E;  //holds the current Enemy doing battle
        private Item PItem;  //holds the currently equipped Item a Player is using
        private Technique PTechnique;  //holds the currently used Technique of currentEquipped
        private bool bTurn;  //states whether the Player is attacking (bTurn = true) or the Enemy is attacking (bTurn = false)
        private int currentDamage;  //holds the current damage being done

        public Player currentPlayer
        {
            get { return P; }
            set { P = value; }
        }
        public Item currentItem
        {
            get { return PItem; }
            set { PItem = value; }
        }
        public Technique currentTechnique
        {
            get { return PTechnique; }
            set { PTechnique = value; }
        }
        public Enemy currentEnemy
        {
            get { return E; }
            set { E = value; }
        }
        public bool Turn
        {
            get { return bTurn; }
            set { bTurn = value; }
        }
        public int Damage
        {
            get { return currentDamage; }
            set { currentDamage = value; }
        }
        public void BattleStatus()
        {
            if (P.PlayerCurrentHealth > 0)  //Player has won
            {
                Console.WriteLine("{0} has defeated {1}. HUZZAH!", P.PlayerName, currentEnemy.EnemyName);
                Console.WriteLine();
                //Reset Enemy Health incase Enemy is chosen again
                currentEnemy.EnemyCurrentHealth = currentEnemy.EnemyFullHealth;

                P.PlayerCurrentHealth = P.PlayerFullHealth;
                Console.WriteLine("Thou feels so grand that thy positive chakra hath healed your gaping wounds!");
                Console.WriteLine();
            }
            else  //Player has died
            {
                Console.WriteLine("{0} hath defeated {1}. GAME OVER.", currentEnemy.EnemyName, P.PlayerName);
            }
            Console.ReadKey();
        }
        public void PlayerOptions()
        {
            int Parser;  //used throughout menus to TryParse menu inputs
            int optionPick;
            string soptionPick;

            do
            {
                if (currentTechnique.CurrentCoolDown < currentTechnique.TechniqueCoolDown)  //if currentTechnique is cooling down..
                {
                    currentTechnique.TechniqueEquipped = false;
                    if (currentItem.ItemTechnique.Count > 1)  //if current Item has multiple techniques..
                    {
                        foreach (Technique T in currentItem.ItemTechnique)
                        {
                            if (T.CurrentCoolDown == T.TechniqueCoolDown)
                                currentTechnique = T;
                            T.TechniqueEquipped = true;
                            Console.WriteLine("Technique {0} of {1} has been selected because the previously selected technique is cooling down!", currentTechnique.TechniqueName, currentItem.ItemName);
                            Console.WriteLine();
                            break;
                        }
                    }
                    else  //current Item has only one Technique.. and it's cooling down
                    {
                        currentItem.ItemEquipped = false;
                        currentTechnique.TechniqueEquipped = false;
                        foreach (Item I in P.PlayerInventory)  //check each Item in Player Inventory
                        {
                            foreach (Technique T in I.ItemTechnique)  //check each Technique of Item
                            {
                                if (T.CurrentCoolDown == T.TechniqueCoolDown)  //if Technique isn't cooling down..
                                    currentItem = I;  //set new currentItem
                                I.ItemEquipped = true;
                                currentTechnique = T;  //set new currentTechnique
                                T.TechniqueEquipped = true;
                                Console.WriteLine("{0} and technique {1} have been selected because all of the previously selected item's techniques are cooling down!", currentItem.ItemName, currentTechnique.TechniqueName);
                                Console.WriteLine();
                                break;
                            }
                            if (currentTechnique.CurrentCoolDown == currentTechnique.TechniqueCoolDown)
                                break;
                        }
                    }
                }
                Console.WriteLine("What would you like to do?");
                Console.WriteLine();
                if (currentTechnique.TechniqueType == "Damage")
                {
                    Console.WriteLine("1. Attack {0} with {1} using {2}", E.EnemyName, currentItem.ItemName, currentTechnique.TechniqueName);
                }
                else if (currentTechnique.TechniqueType == "Heal")
                {
                    Console.WriteLine("1. Heal {0} Health with {1} using {2}", currentTechnique.TechniqueValue, currentItem.ItemName, currentTechnique.TechniqueName);
                }
                Console.WriteLine("2. Use a different technique of {0}", currentItem.ItemName);
                Console.WriteLine("3. Select a new Item from your Inventory");
                soptionPick = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(soptionPick, out Parser) == false)
                {
                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                    Console.WriteLine();
                    optionPick = 0;
                }
                else
                {
                    optionPick = int.Parse(soptionPick);
                }
                switch (optionPick)
                {
                    case 1:    //return to Battle!
                        if (currentTechnique.TechniqueType == "Damage")
                        {
                            PlayerAttack();
                        }
                        else if (currentTechnique.TechniqueType == "Heal")
                        {
                            PlayerHeal();
                        }
                        break;

                    case 2:    //write out the names of each Technique of the current Item for Technique selection
                        string stechniquePick;
                        int techniquePick;
                        if (currentItem.ItemTechnique.Count < 2 && currentItem.ItemTechnique[0].CurrentCoolDown < currentItem.ItemTechnique[0].TechniqueCoolDown)
                        {
                            Console.WriteLine("{0} has no techniques that are currently cooled down!", currentItem.ItemName);
                        }
                        else
                        {
                            currentTechnique.TechniqueEquipped = false;
                            Console.WriteLine("Chooseth which technique you wish to use:");
                            do
                            {
                                for (int count = 0; count < currentItem.ItemTechnique.Count; count++)
                                {
                                    if (currentItem.ItemTechnique[count].CurrentCoolDown < currentItem.ItemTechnique[count].TechniqueCoolDown)
                                    {
                                        Console.WriteLine("{0}. {1} must cool down for {2} more rounds", count + 1, currentItem.ItemTechnique[count].TechniqueName, currentItem.ItemTechnique[count].TechniqueCoolDown - currentItem.ItemTechnique[count].CurrentCoolDown);
                                    }
                                    else
                                    {
                                        Console.WriteLine("{0}. Name: {1}  Element: {2}  Type: {3}", count + 1, currentItem.ItemTechnique[count].TechniqueName, currentItem.ItemTechnique[count].TechniqueElement, currentItem.ItemTechnique[count].TechniqueType);
                                    }
                                }
                                stechniquePick = Console.ReadLine();
                                Console.Clear();
                                if (int.TryParse(stechniquePick, out Parser) == false)
                                {
                                    Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                                    Console.WriteLine();
                                    techniquePick = 0;
                                }
                                else
                                {
                                    techniquePick = int.Parse(stechniquePick);
                                }
                                if (techniquePick > 0 && techniquePick < currentItem.ItemTechnique.Count + 1)
                                {
                                    try
                                    {
                                        if (currentItem.ItemTechnique[techniquePick - 1].CurrentCoolDown < currentItem.ItemTechnique[techniquePick - 1].TechniqueCoolDown)
                                        {
                                            Console.WriteLine("{0} IS STILL TOO HOT TO USE!!1!", currentItem.ItemTechnique[techniquePick - 1].TechniqueName);
                                            Console.WriteLine();
                                            if (P.PlayerCurrentHealth > 20)
                                            {
                                                P.PlayerCurrentHealth = P.PlayerCurrentHealth - 5;
                                                Console.WriteLine("Your own stupidity lowered your Health to {0}/{1}", P.PlayerCurrentHealth, P.PlayerFullHealth);
                                                Console.WriteLine();
                                            }
                                            techniquePick = 0;
                                        }
                                        currentTechnique = currentItem.ItemTechnique[techniquePick - 1];
                                        currentTechnique.TechniqueEquipped = true;
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("You have selected a technique that is not available..");
                                        Console.WriteLine("Thy must attempt said grande feat again!");
                                        Console.WriteLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Thy must attempt said grande feat again!");
                                    Console.WriteLine();
                                }
                            } while (techniquePick < 1 || techniquePick > currentItem.ItemTechnique.Count);
                            Console.Clear();
                        }
                        break;

                    case 3:
                        //write out the names of each Item in Player's Inventory for Item selection
                        string sitemPick;
                        int itemPick;

                        currentItem.ItemEquipped = false;
                        currentTechnique.TechniqueEquipped = false;
                        Console.WriteLine("Chooseth which item you wish to use:");
                        do
                        {
                            int count = 1;  //keeps track of current Menu list number
                            foreach (Item I in P.PlayerInventory)
                            {
                                int hotTechnique = 0;  //keeps track of Techniques within an Item that haven't cooled down
                                for (int i = 0; i < I.ItemTechnique.Count; i++)  //loop through each Technique of Item
                                {
                                    if (I.ItemTechnique[i].CurrentCoolDown < I.ItemTechnique[i].TechniqueCoolDown)  //if Technique hasn't cooled down..
                                    {
                                        hotTechnique++;  //add 1 to variable
                                    }
                                }
                                if (hotTechnique == I.ItemTechnique.Count)  //if all Techniques aren't cooled down..
                                {
                                    Console.WriteLine("{0}. {1} has no techniques that are currently cooled down.", count, I.ItemName);
                                    foreach (Technique T in I.ItemTechnique)
                                    {
                                        int CoolDown = T.TechniqueCoolDown - T.CurrentCoolDown;
                                        if (CoolDown == 1)
                                        {
                                            Console.WriteLine("        {0} must cool down for {1} more round", T.TechniqueName, CoolDown);
                                        }
                                        else
                                        {
                                            Console.WriteLine("        {0} must cool down for {1} more rounds", T.TechniqueName, CoolDown);
                                        }
                                    }
                                }
                                else if (I.ItemTechnique[0].TechniqueType == "Defend")  //if Item has a "Defend" technique (ie. Shield)
                                {
                                    Console.WriteLine("{0}. {1} can not be equipped during battle", count, I.ItemName);
                                }
                                else
                                {
                                    Console.WriteLine("{0}. Name: {1}  Techniques: {2}", count, I.ItemName, I.ItemTechnique.Count);
                                }
                                count++;
                            }
                            sitemPick = Console.ReadLine();
                            Console.Clear();
                            if (int.TryParse(sitemPick, out Parser) == false)
                            {
                                Console.WriteLine("Alas, ye must comply with numeric values to proceed!");
                                Console.WriteLine();
                                itemPick = 0;
                            }
                            else
                            {
                                itemPick = int.Parse(sitemPick);
                            }
                            if (itemPick > 0 && itemPick < P.PlayerInventory.Count + 1)
                            {
                                try
                                {
                                    int hotTechnique = 0;  //keeps track of Techniques within an Item that haven't cooled down
                                    foreach (Technique T in P.PlayerInventory[itemPick - 1].ItemTechnique)  //loop through each Technique of Item
                                    {
                                        if (T.CurrentCoolDown < T.TechniqueCoolDown)  //if Technique hasn't cooled down..
                                        {
                                            hotTechnique++;  //add 1 to variable
                                        }
                                    }
                                    if (hotTechnique == P.PlayerInventory[itemPick - 1].ItemTechnique.Count)  //if all Techniques aren't cooled down..
                                    {
                                        Console.WriteLine("{0} DOES NOT HAVE TECHNIQUES THAT AREN'T COOLING DOWN!!1!", P.PlayerInventory[itemPick - 1].ItemName);
                                        Console.WriteLine();
                                        if (P.PlayerCurrentHealth > 20)
                                        {
                                            P.PlayerCurrentHealth = P.PlayerCurrentHealth - 5;
                                            Console.WriteLine("Your own stupidity lowered your Health to {0}/{1}", P.PlayerCurrentHealth, P.PlayerFullHealth);
                                            Console.WriteLine();
                                        }
                                        itemPick = 0;
                                    }
                                    else if (P.PlayerInventory[itemPick - 1].ItemTechnique[0].TechniqueType == "Defend")
                                    {
                                        Console.WriteLine("{0} CAN NOT BE EQUIPPED DURING BATTLE!!!1!", P.PlayerInventory[itemPick - 1].ItemName);
                                        Console.WriteLine();
                                        if (P.PlayerCurrentHealth > 20)
                                        {
                                            P.PlayerCurrentHealth = P.PlayerCurrentHealth - 5;
                                            Console.WriteLine("Your own stupidity lowered your Health to {0}/{1}", P.PlayerCurrentHealth, P.PlayerFullHealth);
                                            Console.WriteLine();
                                        }
                                        itemPick = 0;
                                    }
                                    currentItem = P.PlayerInventory[itemPick - 1];  //if Item didn't meet criteria, error is caught
                                    currentItem.ItemEquipped = true;
                                    foreach (Technique T in P.PlayerInventory[itemPick - 1].ItemTechnique)  //loop through each Technique of Item
                                    {
                                        if (T.CurrentCoolDown == T.TechniqueCoolDown)  //if Technique isn't cooling down
                                        {
                                            currentTechnique = T;  //set to current Technique
                                            currentTechnique.TechniqueEquipped = true;
                                            break;  //first Technique that isn't cooling down is selected
                                        }
                                    }
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine("Ye do not have that many items in your inventory!", currentItem.ItemName);
                                    Console.WriteLine("Thy must attempt said grande feat again!");
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Thy must attempt said grande feat again!");
                                Console.WriteLine();
                            }
                        } while (itemPick < 1 || itemPick > P.PlayerInventory.Count);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Thy must attempt said grande feat again!");
                        Console.WriteLine();
                        break;
                }
            } while (optionPick != 1);
        }
        public void EnemyAttack()
        {
            Random R = new Random();
            int tPick;
            do
            {
                tPick = R.Next(currentEnemy.EnemyItem.ItemTechnique.Count);
            } while (E.EnemyItem.ItemTechnique[tPick].CurrentCoolDown < E.EnemyItem.ItemTechnique[tPick].TechniqueCoolDown);
            currentEnemy.EnemyTechnique = E.EnemyItem.ItemTechnique[tPick];
            if (currentEnemy.EnemyTechnique.TechniqueName == "Smoke Monster")
            {
                Damage = currentEnemy.EnemyTechnique.TechniqueValue;  //Smoke Monster only ever does 42 damage.. :)
            }
            else
            {
                Damage = currentEnemy.EnemyTechnique.TechniqueValue + R.Next(-4, 4);
            }
            DefenceVariance(Damage);
            ElementalVariance(Damage);
            P.PlayerCurrentHealth = P.PlayerCurrentHealth - Damage;
            Console.WriteLine("{0} does {1} damage using technique '{2}'", E.EnemyName, Damage, currentEnemy.EnemyTechnique.TechniqueName);
            Console.WriteLine();
            if (P.PlayerCurrentHealth < 0)
            {
                P.PlayerCurrentHealth = 0;
            }
            Console.WriteLine("{0}'s Health is now at {1}/{2}.", P.PlayerName, P.PlayerCurrentHealth, P.PlayerFullHealth);
            Console.ReadKey();
            Console.Clear();
            if (currentEnemy.EnemyTechnique.CurrentCoolDown < currentEnemy.EnemyTechnique.TechniqueCoolDown)
            {
                currentEnemy.EnemyTechnique.CurrentCoolDown++;
            }
            bTurn = true;
            if (P.PlayerCurrentHealth == 0)
            {
                BattleStatus();
            }
            else
            {
                PlayerOptions();
            }
        }
        public void PlayerAttack()
        {
            Random R = new Random();

            if (currentTechnique.CurrentCoolDown == currentTechnique.TechniqueCoolDown)
            {
                currentTechnique.CurrentCoolDown = 0;
            }
            Damage = currentTechnique.TechniqueValue + R.Next(-4, 4);
            DefenceVariance(Damage);
            ElementalVariance(Damage);
            E.EnemyCurrentHealth = E.EnemyCurrentHealth - Damage;
            Console.WriteLine("{0} has done {1} damage using technique '{2}'", P.PlayerName, Damage, currentTechnique.TechniqueName);
            Console.WriteLine();
            if (E.EnemyCurrentHealth < 0)
            {
                E.EnemyCurrentHealth = 0;
            }
            Console.WriteLine("{0}'s Health is now at {1}/{2}.", E.EnemyName, E.EnemyCurrentHealth, E.EnemyFullHealth);
            if (currentItem.ItemDurability == false)
            {
                Console.WriteLine("{0} is used up and has been tossed from your inventory.", currentItem.ItemName);
                P.PlayerInventory.Remove(currentItem);  //remove selected Item
            }
            Console.ReadKey();
            Console.Clear();
            foreach (Item I in P.PlayerInventory)  //increase current cooldown of any Item cooling down
            {
                foreach (Technique T in I.ItemTechnique)
                {
                    if (T.CurrentCoolDown < T.TechniqueCoolDown)
                    {
                        T.CurrentCoolDown++;
                    }
                }
            }
            bTurn = false;
            if (currentEnemy.EnemyCurrentHealth == 0)
            {
                int eItem = R.Next(10);  //1 in 10 chance of obtaining the enemies Item
                if (eItem == 5)
                {
                    P.PlayerInventory.Add(currentEnemy.EnemyItem);  //add Item to Inventory
                    Console.WriteLine("Rather than the enemy weapon evaporating as per usual, it is left upon the reeking remains of {0}.", currentEnemy.EnemyName);
                    Console.WriteLine("{0} hath been added to thy inventory!", currentEnemy.EnemyItem.ItemName);
                }
                BattleStatus();
            }
            else
            {
                EnemyAttack();
            }
        }
        public void PlayerHeal()
        {
            if (currentTechnique.CurrentCoolDown == currentTechnique.TechniqueCoolDown)
            {
                currentTechnique.CurrentCoolDown = 0;
            }
            P.PlayerCurrentHealth = P.PlayerCurrentHealth + currentTechnique.TechniqueValue;
            if (P.PlayerCurrentHealth > P.PlayerFullHealth)
            {
                P.PlayerCurrentHealth = P.PlayerFullHealth;
            }
            Console.WriteLine("{0}'s Health is now {1}/{2}.", P.PlayerName, P.PlayerCurrentHealth, P.PlayerFullHealth);
            if (currentItem.ItemDurability == false)
            {
                Console.WriteLine("{0} is used up and has been tossed from your inventory.", currentItem.ItemName);
                P.PlayerInventory.Remove(currentItem);  //remove selected Item
            }
            Console.ReadKey();
            Console.Clear();

            EnemyAttack();
        }
        public void DefenceVariance(int Damage)
        {
            int defencePercent = 0;  //holds the total values for all Techniques of "Defend" type
            int defencePass = 0;
            string atechniqueElement;  //Element of attacker's Technique
            List<string> dtechniqueElement = new List<string>();  //holds all Elements of defender's "Defend" Technique(s)

            if (bTurn == true)  //if Player is attacking
            {
                atechniqueElement = currentTechnique.TechniqueElement;

                foreach (Technique T in E.EnemyItem.ItemTechnique)
                {
                    if (T.TechniqueType == "Defend")
                    {
                        defencePercent = defencePercent + T.TechniqueValue;
                        dtechniqueElement.Add(T.TechniqueElement);
                    }
                }
            }
            else // Enemy is attacking
            {
                atechniqueElement = E.EnemyTechnique.TechniqueElement;

                foreach (Item I in P.PlayerInventory)
                {
                    foreach (Technique T in I.ItemTechnique)
                    {
                        if (T.TechniqueType == "Defend")
                        {
                            defencePercent = defencePercent + T.TechniqueValue;
                            dtechniqueElement.Add(T.TechniqueElement);
                        }
                    }
                }
            }
            dtechniqueElement = dtechniqueElement.Distinct().ToList();

            switch (atechniqueElement)
            {
                case "Fire":
                    foreach (string E in dtechniqueElement)
                    {
                        if (E == "Fire")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.3);
                        }
                        if (E == "Air")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Earth")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.05);
                        }
                        if (E == "Water")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.15);
                        }
                    }
                    break;

                case "Air":
                    foreach (string E in dtechniqueElement)
                    {
                        if (E == "Fire")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Air")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.3);
                        }
                        if (E == "Earth")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Water")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.05);
                        }
                    }
                    break;

                case "Earth":
                    foreach (string E in dtechniqueElement)
                    {
                        if (E == "Fire")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.05);
                        }
                        if (E == "Air")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Earth")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.3);
                        }
                        if (E == "Water")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.15);
                        }
                    }
                    break;

                case "Water":
                    foreach (string E in dtechniqueElement)
                    {
                        if (E == "Fire")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Air")
                        {
                            defencePass = defencePass - Convert.ToInt32(defencePercent * 0.05);
                        }
                        if (E == "Earth")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.15);
                        }
                        if (E == "Water")
                        {
                            defencePass = defencePass + Convert.ToInt32(defencePercent * 0.3);
                        }
                    }
                    break;

                default:
                    break;
            }
            defencePercent = defencePercent + defencePass;
            if (defencePercent > 0)
            {
                decimal dDamage = Convert.ToDecimal(Damage);
                decimal dDefencePercent = Convert.ToDecimal(defencePercent);

                Damage = Damage - Convert.ToInt32(Math.Round(dDamage * (dDefencePercent / 100)));
            }
        }
        public void ElementalVariance(int Damage)
        {
            string attackerElement;  //Element of attacker's Technique
            string defenderElement;  //Element of defender

            if (bTurn == true)  //if Player is attacking
            {
                attackerElement = currentTechnique.TechniqueElement;
                defenderElement = E.EnemyElement;
            }
            else  //Enemy is attacking
            {
                attackerElement = E.EnemyTechnique.TechniqueElement;
                defenderElement = P.PlayerElement;
            }
            switch (attackerElement)
            {
                case "Fire":
                    switch (defenderElement)
                    {
                        case "Air":
                            Damage = Damage + Convert.ToInt32(Damage * 0.15);  //15% gain
                            break;

                        case "Earth":
                            Damage = Damage + Convert.ToInt32(Damage * 0.05);  //5% gain
                            break;

                        case "Water":
                            Damage = Damage - Convert.ToInt32(Damage * 0.15);  //15% loss
                            break;

                        default:
                            break;
                    }
                    break;

                case "Air":
                    switch (defenderElement)
                    {
                        case "Fire":
                            Damage = Damage - Convert.ToInt32(Damage * 0.15);  //15% loss
                            break;

                        case "Earth":
                            Damage = Damage + Convert.ToInt32(Damage * 0.15);  //15% gain
                            break;

                        case "Water":
                            Damage = Damage + Convert.ToInt32(Damage * 0.05);  //5% gain
                            break;

                        default:
                            break;
                    }
                    break;

                case "Earth":
                    switch (defenderElement)
                    {
                        case "Fire":
                            Damage = Damage + Convert.ToInt32(Damage * 0.05);  //5% gain
                            break;

                        case "Air":
                            Damage = Damage - Convert.ToInt32(Damage * 0.05);  //15% loss
                            break;

                        case "Water":
                            Damage = Damage + Convert.ToInt32(Damage * 0.15);  //15% gain
                            break;

                        default:
                            break;
                    }
                    break;

                case "Water":
                    switch (defenderElement)
                    {
                        case "Fire":
                            Damage = Damage + Convert.ToInt32(Damage * 0.15);  //15% gain
                            break;

                        case "Air":
                            Damage = Damage + Convert.ToInt32(Damage * 0.05);  //5% gain
                            break;

                        case "Earth":
                            Damage = Damage - Convert.ToInt32(Damage * 0.15);  //15% loss
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}

//  LocationRandomizer: sets random values between 0 and Array.Length that are unique from one another (ie. no repeated values) 

//public void LocationRandomizer(Item[] Items)
//{
//    int[] RandomIndex = new int[Items.Length];  //array to hold Indexi (Indexes..? nah)
//    bool Match = false;  //true when the Index matches a value in the RandomIndex array
//    bool Index0 = false;  //true when a position in RandomIndex array stores value 0
//    int Index;  //variable to hold Random number between 0 and RandomIndex array length - 1
//    int i = 0;  //variable to keep track of write-to position in RandomIndex array
//    Random R = new Random();  //creating new instance of Random class

//    while (i != RandomIndex.Length)  //while RandomIndex array hasn't been fully written to..
//    {
//        Index = R.Next(RandomIndex.Length);  //set Index to random number between 0 and 9

//        //Because default value in RandomIndex is 0, separate statement must be used so Index 0 can be added
//        if (Index == 0 && Index0 == false)  //if Index is 0 and 0 hasn't been previously set..
//        {
//            RandomIndex[i] = Index;  //add Index to RandomIndex array
//            i++;  //increment array position
//            Index0 = true;  //0 has now been set
//        }
//        foreach (int I in RandomIndex)  //loop through each stored Index
//        {
//            if (I == Index)  //if a stored Index = the current Index..
//            {
//                Match = true;  //the Index has already been added
//                break;  //might as well exit at this point..
//            }
//        }
//        if (Match == false)  //if Index has not been added..
//        {
//            RandomIndex[i] = Index;  //add Index to RandomIndex array
//            i++;  //increment array position
//        }
//        Match = false;  //reset matching check
//    }
//    for (int count = 0; count < Items.Length; count++)
//    {
//        Items[count].ItemLocation = RandomIndex[count];  //set each value in RandomIndex array to Item location
//    }
//}