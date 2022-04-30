using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MangaService : IManga
    {
        Dictionary<int, Manga> Mangas { get; set; }


        public MangaService()
        {
            Mangas = new Dictionary<int, Manga>();
        }

        public void Add(int id, Manga newManga)
        {
            Mangas.Add(id, newManga);
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

        public bool Contains(Manga manga)
        {
            return Mangas.Values.ToList().Contains(manga);
        }
    }
}
