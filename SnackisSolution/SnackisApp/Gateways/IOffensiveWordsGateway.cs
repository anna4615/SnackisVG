using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Gateways
{
    public interface IOffensiveWordsGateway
    {
        Task<List<OffensiveWord>> GetWords();
        Task<OffensiveWord> GetWord(int id);
        Task<OffensiveWord> PostWord(OffensiveWord word);
        Task PutWord(int editId, OffensiveWord word);
        Task DeleteWord(int deleteId);

        Task<string> GetCheckedText(string text);

    }
}
