using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Respository
{
	public interface IJobRespository
	{
		Task<IEnumerable<Job>> GetAllAsync();
		Task<Job> GetByIdAsync(int id);
		Task AddAsync(Job job);
		Task UpdateAsync(Job job);
		Task DeleteAsync(int id);

		List<SearchViewModel> Search(string stringSearch);


		List<ChoeseSearchSkill> ChoeseSearchSkills(int ID);

		List<ChoeseSearchType> ChoeseSearchType(int ID);

		List<ChoeseSearchRank> ChoeseSearchRank(int ID);
		List<ChoeseSearchLocation> ChoeseSearchLocation(int ID);

        List<Details_Job> Details_Job(int ID);

    }
}
