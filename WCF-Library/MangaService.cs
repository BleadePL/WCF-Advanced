using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WCF_Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class MangaService : IManga
    {
        public Dictionary<int, Manga> Mangas { get; set; }
        public IMangaCallback Callback { get; set; }


        public MangaService()
        {
            Mangas = new Dictionary<int, Manga>();
            Callback = OperationContext.Current.GetCallbackChannel<IMangaCallback>();
        }

        public void Add(Manga newManga)
        {
            newManga.Id = Mangas.Keys.LastOrDefault() + 1;
            Mangas.Add(newManga.Id.Value, newManga);
        }

        public void Delete(int id)
        {
            Mangas.Remove(id);
        }

        public void Edit(int id, Manga manga)
        {
            Mangas.Remove(id);
            Mangas.Add(id, manga);
        }

        public Manga[] GetAll()
        {
            return Mangas.Values.ToArray();
        }

        public Manga[] GetAllByAuthor(string author)
        {
            var mangas = Mangas.Values.ToList();
            return mangas.Where(m => m.Author == author).ToArray();
        }

        public void Contains(Manga manga)
        {
            Thread.Sleep(1000);
            Callback.ContainsResult(Mangas.ContainsValue(manga));
        }
    }
}
