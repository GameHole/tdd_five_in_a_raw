using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Five
{
    public class ButtonEvent
    {
        private List<Action> actions = new List<Action>();

        public ButtonEvent(Button btn)
        {
             btn.onClick.AddListener(Invoke);
        }

        public int EventCount { get => actions.Count; }

        public void AddListener(Action action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action action)
        {
            actions.Remove(action);
        }

        public void Invoke()
        {
            foreach (var item in actions)
            {
                item.Invoke();
            }
        }
    }
}
