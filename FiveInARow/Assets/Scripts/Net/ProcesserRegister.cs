namespace Five
{
    public class ProcesserRegister:IProcesserRegister
    {
        public virtual void Regist(MessageProcesser processer)
        {
            var processers = new IProcesser[]
            {
                new PlayedProcesser()
            };
            foreach (var item in processers)
            {
                processer.Processers.Add(item.OpCode, item);
            }
          
        }
    }
}
