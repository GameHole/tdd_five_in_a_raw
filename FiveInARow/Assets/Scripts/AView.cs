using UnityEngine;

namespace Five
{
    public abstract class AView
    {
        public GameObject View { get; protected set; }
        public AView()
        {
            View = GenrateView();
        }
        protected abstract GameObject GenrateView();
        public virtual void Open()
        {
            View.SetActive(true);
        }

        public virtual void Close()
        {
            View.SetActive(false);
        }
    }
}
