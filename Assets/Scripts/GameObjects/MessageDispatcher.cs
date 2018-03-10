using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iso
{

    /// <summary>
    /// 
    /// </summary>
    enum Message_Type: long
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public struct Telegram
    {
        //the entity that sent this telegram
        public int Sender;

        //the entity that will receive this telegram
        public int Receiver;

        //the msg itself
        public long Msg;

        //the time the msg should be dispatched
        public long DispatchTime;

        //what would be appropriate data structure for additional info??

        public Telegram(long delay, int sender, int receiver, long msg)
        {
            DispatchTime = delay;
            Sender = sender;
            Receiver = receiver;
            Msg = msg;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MessageDispatcher : MonoBehaviour
    {
        SortedSet<Telegram> PriorityQ;

        static MessageDispatcher Instance = null;

        private void Awake()
        {
            Instance = this;
            PriorityQ = new SortedSet<Telegram>();
        }

        /// <summary>
        /// Utilized by DispatchMessage or DispatchDelayedMessages.
        /// Calls the message handling member function of the receiving
        /// entity, receiver, with the newly created telegram.
        /// </summary>
        /// <param name="Receiver"></param>
        /// <param name="msg"></param>
        void Discharge(BaseEntity Receiver, Telegram msg)
        {

        }

        /// <summary>
        /// Send a message to another agent.
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <param name="msg"></param>
        public void DispatchMessage( long delay, int sender, int receiver, long msg)
        {
            BaseEntity Receiver = EntityManager.GetInstance().GetEntityFromID(receiver);
            Telegram telegram = new Telegram(delay, sender, receiver, msg);

            if(delay <= 0.0)
            {
                Discharge(Receiver, telegram);
            }
            else
            {
                long CurrentTime = System.DateTime.Now.Ticks;
                telegram.DispatchTime = CurrentTime + delay;

                PriorityQ.Add(telegram);
            }
        }
        
        /// <summary>
        /// Send out any delayed messages. This method is called each time through the main game loop.
        /// </summary>
        void DispatchDelayedMessages()
        {
            long CurrentTime = System.DateTime.Now.Ticks;

            while((PriorityQ.First().DispatchTime < CurrentTime) &&
                (PriorityQ.First().DispatchTime > 0))
            {
                Telegram telegram = PriorityQ.First();

                BaseEntity Receiver = EntityManager.GetInstance().GetEntityFromID(telegram.Receiver);

                Discharge(Receiver, telegram);
                PriorityQ.Remove(PriorityQ.First());
            }
        }
        public static MessageDispatcher GetInstance()
        {
            return Instance;
        }
    }
}
