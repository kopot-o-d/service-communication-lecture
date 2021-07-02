using System.Threading.Tasks;

namespace LectureAPI.Interfaces
{
	public interface IQueueService
	{
		Task<bool> PostValue(string value);
	}
}