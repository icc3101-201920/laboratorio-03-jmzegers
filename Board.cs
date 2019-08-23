using Laboratorio_2_OOP_201902.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_2_OOP_201902
{
    public class Board
    {
        //Constantes
        private const int DEFAULT_NUMBER_OF_PLAYERS = 2;

        //Atributos
        //private List<CombatCard>[] meleeCards;

        private Dictionary<string, List<Card>>[] playerCards;

        private List<SpecialCard> weatherCards;

        //Propiedades

        public List<Dictionary>[] playerCards
        {
            get
            {

            }
            set
            {

            }
        }

        public List<CombatCard>[] MeleeCards
        {
            get
            {
                return this.meleeCards;
            }
        }
        public List<CombatCard>[] RangeCards
        {
            get
            {
                return this.rangeCards;
            }
        }
        public List<CombatCard>[] LongRangeCards
        {
            get
            {
                return this.longRangeCards;
            }
        }
        public SpecialCard[] SpecialMeleeCards
        {
            get
            {
                return this.specialMeleeCards;
            }
        }
        public SpecialCard[] SpecialRangeCards
        {
            get
            {
                return this.specialRangeCards;
            }
        }
        public SpecialCard[] SpecialLongRangeCards
        {
            get
            {
                return this.specialLongRangeCards;
            }
        }
        public SpecialCard[] CaptainCards
        {
            get
            {
                return this.captainCards;
            }
        }
        public List<SpecialCard> WeatherCards
        {
            get
            {
                return this.weatherCards;
            }
        }


        //Constructor
        public Board()
        {
            this.weatherCards = new List<SpecialCard>();
            this.playerCards = new Dictionary<string, List<Card>>[
                DEFAULT_NUMBER_OF_PLAYERS]; // Inicializa el arreglo de diccionarios.
            this.playerCards[0] = new Dictionary<string, List<Card>>(); //Inicializa los diccionarios.
            this.playerCards[1] = new Dictionary<string, List<Card>>(); //Inicializa los diccionarios.
        }



        //Metodos

        public void AddCard(Card card, int playerId = -1, string buffType= null)
        {
            // Revisar si la carta recibida en el parametro es Combat o Special
            if (card.GetType().Name == nameof(CombatCard))
            {
                //En caso de que sea de combate , agregarla al diccionario
                //del jugador correspondiente , revisando el parametro playerId.
                if (playerId == 0 || playerId == 1)
                {
                    //Se revisa si el diccionario ya tiene alguna carta
                    //del tipo que se quiere agregar, por ejemplo melee.
                    //
                    // Constainskey revisa si existe la llave en el diccionario.
                    // En caso de existir retorna true.
                    // Por ejemplo , si la carta es de tipo "melee",
                    // el metodo busca si existe la llave "melee" en el diccionario.

                    if (playerCards[playerId].ContainsKey(card.Type))
                    {
                        //Si es que existe no es necesario crear la lista
                        //de cartas, por lo que se agrega directamente
                        playerCards[playerId][card.Type].Add(card);
                    }
                    else
                    {
                        //Si no existe , se agrega un nuevo par "Llave ": Valor al diccionario.
                        //Este par tiene como "Llave" el tipo de la carta, ejemplo "melee".
                        //Este par tiene como valor la lista de cartas, con la primera carta que se va a agregar.
                        playerCards[playerId].Add(card.Type, new List<Card>() { card });
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("No player ID given.");
                }
            }
            else
            {
                if (card.GetType().Name == nameof(CaptainCards))
                {
                    if (playerId == 0 || playerId == 1)
                    {
                        if (playerCards[playerId][CaptainCards].containsKey(CaptainCards))
                        {
                            throw new IndexOutOfRangeException("Hand already has Captain Card");
                        }
                        else
                        {
                            playerCards[playerId][CaptainCards].Add(CaptainCards)
                        }
                    }
                }
                else
                {
                    if (card.GetType().Name == nameof(buffType))
                    {
                        if (playerId == 0 || playerId == 1)
                        {
                            if (playerCards[playerId][combatCard].containsKey(buffType))
                            {
                                throw new IndexOutOfRangeException("Hand already has buffer");
                            }
                            else
                            {
                                playerCards[playerId][combatCard].Add(buffType);
                            }
                        }
                    }
                }
            }
        }













        public void AddCombatCard(int playerId, CombatCard combatCard)
        {
            if (combatCard.Type == "melee")
            {
                meleeCards[playerId].Add(combatCard);
            }
            else if (combatCard.Type == "range")
            {
                rangeCards[playerId].Add(combatCard);
            }
            else
            {
                longRangeCards[playerId].Add(combatCard);
            }
            
        }
        public void AddSpecialCard(SpecialCard specialCard, int playerId = -1, string buffType = null)
        {
            if (specialCard.Type == "captain")
            {
                if (playerId == 0 || playerId == 1)
                {
                    captainCards[playerId] = specialCard;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else if (specialCard.Type == "weather")
            {
                weatherCards.Add(specialCard);
            }
            else
            {
                if (buffType != null)
                {
                    if (playerId == 0 || playerId == 1)
                    {
                        if (buffType == "melee")
                        {
                            specialMeleeCards[playerId] = specialCard;
                        }
                        else if (buffType == "range")
                        {
                            specialRangeCards[playerId] = specialCard;
                        }
                        else
                        {
                            specialLongRangeCards[playerId] = specialCard;
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }   
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        public void DestroyCards()
        {
            //Guardar las cartas de capitan en una variable temporal.
            List<Card>[] captainCards = new List<Card>[]
            {
                new List<Card>(playerCards[0]["Captain"])
                new List<Card>(playerCards[1]["Captain"])
            };
        }

        public int[] GetMeleeAttackPoints()
        {
            //Debe sumar todos los puntos de ataque de las cartas melee y retornar los valores por jugador.
            int[] totalAttack = new int[] { 0, 0 };
            for (int i=0; i < 2; i++)
            {
                foreach (CombatCard meleeCard in meleeCards[i])
                {
                    totalAttack[i] += meleeCard.AttackPoints;
                }
            }
            return totalAttack;
            
        }
        public int[] GetRangeAttackPoints()
        {
            //Debe sumar todos los puntos de ataque de las cartas range y retornar los valores por jugador.
            int[] totalAttack = new int[] { 0, 0 };
            for (int i = 0; i < 2; i++)
            {
                foreach (CombatCard rangeCard in rangeCards[i])
                {
                    totalAttack[i] += rangeCard.AttackPoints;
                }
            }
            return totalAttack;
        }
        public int[] GetLongRangeAttackPoints()
        {
            //Debe sumar todos los puntos de ataque de las cartas longRange y retornar los valores por jugador.
            int[] totalAttack = new int[] { 0, 0 };
            for (int i = 0; i < 2; i++)
            {
                foreach (CombatCard longRangeCard in longRangeCards[i])
                {
                    totalAttack[i] += longRangeCard.AttackPoints;
                }
            }
            return totalAttack;
        }
    }
}
