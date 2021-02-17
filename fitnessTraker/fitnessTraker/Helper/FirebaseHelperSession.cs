using Firebase.Database;
using Firebase.Database.Query;
using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitnessTraker.Helper
{
    class FirebaseHelperSession
    {
        FirebaseClient firebase = new FirebaseClient("https://fitness-7871f-default-rtdb.firebaseio.com/");

        public async Task<List<MaSession>> GetAllSessions()
        {

            return (await firebase
              .Child("Sessions")
              .OnceAsync<MaSession>()).Select(item => new MaSession
              {
                  _id = item.Object._id,
                  startPosition = item.Object.startPosition,
                  Pas = item.Object.Pas,
                  Distance = item.Object.Distance,
                  Temps = item.Object.Temps,
                  date = item.Object.date,
                  adresse = item.Object.adresse,
                  positions = item.Object.positions,
              }).ToList();
        }
        public async Task AddSession(MaSession sess)
        {

            await firebase
              .Child("Sessions")
              .PostAsync(sess) ;
        }
        public async Task<List<MaSession>> GetSessions(string id)
        {
            var allSessions= await GetAllSessions();
            await firebase
              .Child("Sessions")
              .OnceAsync<MaSession>();
            return allSessions.Where(a => a._id == id).ToList();
        }
        public async Task UpdateUtilisateur(string id, MaSession s)
        {
            var toUpdatePerson = (await firebase
              .Child("Sessions")
              .OnceAsync<MaSession>()).Where(a => a.Object._id == id).FirstOrDefault();

            await firebase
              .Child("Sessions")
              .Child(toUpdatePerson.Key)
              .PutAsync(s);
        }
        public async Task DeleteUtilisateur(string id)
        {
            var toDeletePerson = (await firebase
              .Child("Sessions")
              .OnceAsync<MaSession>()).Where(a => a.Object._id == id).FirstOrDefault();
            await firebase.Child("Sessions").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
