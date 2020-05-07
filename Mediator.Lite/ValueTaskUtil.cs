using System.Threading.Tasks;

namespace Mediator.Lite
{
    public class ValueTaskUtil
    {
        public static ValueTask Complete => new ValueTask(Task.CompletedTask);
    }
}