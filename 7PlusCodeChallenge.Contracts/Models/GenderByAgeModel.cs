using System.Collections.Generic;

namespace _7PlusCodeChallenge.Contracts.Models
{
    public class GenderByAgeCollectionModel
    {
        public IList<GenderByAgeModel> Models { get; set; }
    }
    public class GenderByAgeModel
    {
        public int Age { get; set; }
        public int Female { get; set; }
        public int Male { get; set; }
    }
}
