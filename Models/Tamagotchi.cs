using System.Collections.Generic;
using System.Threading;
using System;
using Helpers;

namespace TamagotchiGame.Models
{
    public class Tamagotchi
    {
        private Timer timer;

        private static List<Tamagotchi> _instances = new List<Tamagotchi> (){};
        public static Tamagotchi CurrentlySelected {get; private set;}

        public string Name {get; private set;}
        public int Hunger {get; private set;}
        public int Attention {get; private set;}
        public int Rest {get; private set;}
        public bool IsDead {get; private set;}
        public bool IsHungry {get; private set;}
        public bool NeedsAttention {get; private set;}
        public bool NeedsSleep {get; private set;}
        public int Id {get; private set;}

        public Tamagotchi(string name)
        {
            // Confusing timer stuff
            timer = new Timer(this.TimerTick, null, 0, 1000);

            Id = _instances.Count;
            Name = name;
            Hunger = 100;
            Attention = 100;
            Rest = 100;
            IsDead = false;
            IsHungry = false;
            NeedsAttention = false;
            NeedsSleep = false;
            _instances.Add(this);
        }

        public static List<Tamagotchi> GetAll()
        {
            return _instances;
        }

        public static void ClearAll()
        {
            _instances.Clear();
            CurrentlySelected = null;
        }

        public static Tamagotchi Select(int id)
        {
            CurrentlySelected = _instances[id];
            return CurrentlySelected;
        }

        // Timer makes this happen
        private void TimerTick(Object stateInfo)
        {
            PassTime();
        }

        public void PassTime()
        {
            Hunger = MathHelpers.Clamp(Hunger - 1, 0, 100);
            Attention = MathHelpers.Clamp(Attention - 1, 0, 100);
            Rest = MathHelpers.Clamp(Rest - 1, 0, 100);
            this.SetStatus();
        }

        private void SetStatus()
        {
            if (Hunger == 0 || Attention == 0 || Rest == 0)
            {
                IsDead = true;
            }
            else
            {
                if (Hunger <= 30)
                {
                    IsHungry = true;
                }
                else
                {
                    IsHungry = false;
                }
                if (Attention <= 30)
                {
                    NeedsAttention = true;
                }
                else
                {
                    NeedsAttention = false;
                }
                if (Rest <= 30)
                {
                    NeedsSleep = true;
                }
                else
                {
                    NeedsSleep = false;
                }
            }
        }

        public void Sleep()
        {
            Rest = MathHelpers.Clamp(Rest + 15, 0, 100);
            SetStatus();
        }

        public void Feed()
        {
            Hunger = MathHelpers.Clamp(Hunger + 10, 0, 100);
            SetStatus();
        }

        public void Pet()
        {
            Attention = MathHelpers.Clamp(Attention + 5, 0, 100);
            SetStatus();
        }
    }
}
