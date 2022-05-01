using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IMangaCallback))]
    public interface IManga
    {
        [OperationContract]
        void Add(Manga newManga);

        [OperationContract]
        void Edit(int id, Manga manga);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        Manga[] GetAll();

        [OperationContract]
        Manga[] GetAllByAuthor(string author);

        [OperationContract(IsOneWay = true)]
        void Contains(Manga manga);
    }

    public interface IMangaCallback
    {
        [OperationContract(IsOneWay = true)]
        void ContainsResult(bool result);
    }


    [DataContract]
    public class Manga
    {

        [DataMember(IsRequired = false)]
        public int? Id { get; set; }
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

        public string description = "Manga";

        public override bool Equals(object obj)
        {
            if (obj is Manga)
            {
                var other = obj as Manga;

                if (String.Equals(other.Title,Title) && String.Equals(other.Author, Author))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
