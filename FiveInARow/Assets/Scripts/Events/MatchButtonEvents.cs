namespace Five
{
    public class MatchButtonEvents
    {
        public MatchButtonEvents(MatchView match, ASocket socket)
        {
            match.matchBtn.onClick.AddListener(() =>
            {
                socket.Send(new Message(MessageCode.RequestMatch));
            });
            match.cancelBtn.onClick.AddListener(() =>
            {
                socket.Send(new Message(MessageCode.RequestCancelMatch));
            });
        }
    }
}
