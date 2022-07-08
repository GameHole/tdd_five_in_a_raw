namespace Five
{
    public class MatchButtonEvents
    {
        public MatchButtonEvents(MatchView match, ASocket socket)
        {
            match.matchBtn.AddListener(() =>
            {
                socket.Send(new Message(MessageCode.RequestMatch));
            });
            match.cancelBtn.AddListener(() =>
            {
                socket.Send(new Message(MessageCode.RequestCancelMatch));
            });
        }
    }
}
