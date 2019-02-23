using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    using System;

    public class Program
    {
        public static void Main()
        {
            decimal playerCharacterWins = 0, monsterWins = 0, numberCombats = 1000000, totalNumberTurns = 0;
            Random random = new Random();

            for (int i = 0; i <= numberCombats; i++)
            {
                Character playerCharacter = new Character(6, 12, 0, 0, 0);
                Character monster = new Character(6, 12, 0, 0, 0);

                int playerInit, monsterInit, playerAttack, monsterAttack,
                    playerDamage, monsterDamage, numberTurns = 0;
                bool playerTurn;

                do
                {
                    playerInit = playerCharacter.rollInitiative(random);
                    monsterInit = monster.rollInitiative(random);
                } while (playerInit == monsterInit);

                if (playerInit > monsterInit)
                    playerTurn = true;
                else
                    playerTurn = false;

                while (playerCharacter.hitPoints > 0 && monster.hitPoints > 0)
                {
                    if (playerTurn)
                    {
                        playerAttack = playerCharacter.rollAttack(random);
                        if (playerAttack >= monster.armorClass)
                        {
                            playerDamage = playerCharacter.rollDamage(random);
                            monster.hitPoints -= playerDamage;
                        }

                        playerTurn = false;
                    }
                    else
                    {
                        monsterAttack = monster.rollAttack(random);
                        if (monsterAttack >= playerCharacter.armorClass)
                        {
                            monsterDamage = monster.rollDamage(random);
                            playerCharacter.hitPoints -= monsterDamage;
                        }

                        playerTurn = true;
                    }

                    numberTurns++;
                }

                totalNumberTurns += numberTurns;

                if (playerCharacter.hitPoints <= 0)
                    monsterWins++;
                else
                    playerCharacterWins++;
            }

            Console.Write($"Player Character win: {Math.Round((playerCharacterWins / numberCombats * 100), 2).ToString()}%, Monster win: {Math.Round((monsterWins / numberCombats * 100), 2).ToString()}%, Average turns: {Math.Round((totalNumberTurns / numberCombats), 2).ToString()}");
            Console.ReadKey();
        }
    }

    public class Character
    {
        private int _hitPoints;
        private int _armorClass;
        private int _initiativeBonus;
        private int _attackBonus;
        private int _damageBonus;

        public Character(int hitPoints, int armorClass, int initiativeBonus, int attackBonus, int damageBonus)
        {
            _hitPoints = hitPoints;
            _armorClass = armorClass;
            _initiativeBonus = initiativeBonus;
            _attackBonus = attackBonus;
            _damageBonus = damageBonus;
        }

        public int hitPoints
        {
            get => _hitPoints;
            set => _hitPoints = value;
        }

        public int armorClass
        {
            get => _armorClass;
            set => _armorClass = value;
        }

        public int initiativeBonus
        {
            get => _initiativeBonus;
            set => _initiativeBonus = value;
        }

        public int attackBonus
        {
            get => _attackBonus;
            set => _attackBonus = value;
        }

        public int damageBonus
        {
            get => _damageBonus;
            set => _damageBonus = value;
        }

        public int rollInitiative(Random random)
        {
            return random.Next(1, 21) + _initiativeBonus;
        }

        public int rollAttack(Random random)
        {
            return random.Next(1, 21) + _attackBonus;
        }

        public int rollDamage(Random random)
        {
            return random.Next(1, 7) + _damageBonus;
        }
    }
}
