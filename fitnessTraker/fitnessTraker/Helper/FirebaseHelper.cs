using Firebase.Database;
using Firebase.Database.Query;
using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitnessTraker
{
    class FirebaseHelper
    {

        FirebaseClient firebase = new FirebaseClient("https://fitness-7871f-default-rtdb.firebaseio.com/");
        
        public async Task<List<Utilisateur>> GetAllUtilisateurs()
        {

            return (await firebase
              .Child("Utilisateurs")
              .OnceAsync<Utilisateur>()).Select(item => new Utilisateur
              {
                  _id = item.Object._id,
                  nom = item.Object.nom,
                  email = item.Object.email,
                  password = item.Object.password,
                  sex = item.Object.sex,
                  date= item.Object.date,
                  topDistance= item.Object.topDistance
              }).ToList();
        }
        public async Task AddUtilisateur(Utilisateur u)
        {

            await firebase
              .Child("Utilisateurs")
              .PostAsync(u); ;
        }
        public async Task<Utilisateur> GetUtilisateur(string email)
        {
            var allPersons = await GetAllUtilisateurs();
            await firebase
              .Child("Utilisateurs")
              .OnceAsync<Utilisateur>();
            return allPersons.Where(a => a.email == email).FirstOrDefault();
        }
        public async Task UpdateUtilisateur(string id, Utilisateur u)
        {
            var toUpdatePerson = (await firebase
              .Child("Utilisateurs")
              .OnceAsync<Utilisateur>()).Where(a => a.Object._id == id).FirstOrDefault();

            await firebase
              .Child("Utilisateurs")
              .Child(toUpdatePerson.Key)
              .PutAsync(u);
        }
        public async Task DeleteUtilisateur(string id)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Utilisateur>()).Where(a => a.Object._id == id).FirstOrDefault();
            await firebase.Child("Utilisateurs").Child(toDeletePerson.Key).DeleteAsync();

        }

    }
}
