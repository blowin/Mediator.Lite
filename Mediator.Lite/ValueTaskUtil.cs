using System.Threading.Tasks;

namespace Mediator.Lite
{
    public static class ValueTaskUtil
    {
        public static ValueTask Complete => new ValueTask(Task.CompletedTask);
        
        public static ValueTask<Void> CompleteVoid => new ValueTask<Void>(Void.Instance);
    }
}