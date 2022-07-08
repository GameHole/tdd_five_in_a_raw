namespace Five
{
    public class Ip
    {
        public string address;
        public int port;
        public override string ToString()
        {
            return $"{address}:{port}";
        }
    }
}
