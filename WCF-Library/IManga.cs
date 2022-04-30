using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IManga
    {
        [OperationContract]
        void Add(int id, Manga newManga);

        [OperationContract]
        void Edit(int id, Manga manga);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        Manga[] GetAll();

        [OperationContract]
        Manga[] GetAllByAuthor(string author);
    }




    [DataContract]
    public class Manga
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public float Rating { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public DateTime ReleaseDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Manga)
            {
                var other = obj as Manga;

                if (other.Id == Id && other.Title == Title && other.Author == Author)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
