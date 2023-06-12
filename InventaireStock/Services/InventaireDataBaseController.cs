using InventaireStock.Models;
using SQLite;

namespace InventaireStock.Services
{
    class InventaireDatabaseController
    {
        static object locker = new object();
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath(), Constants.Flags);
        });
        static SQLiteAsyncConnection database => lazyInitializer.Value;
        static bool initialized = false;

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!database.TableMappings.Any(m => m.MappedType.Name == typeof(Inventaire).Name))
                {
                    await database.CreateTablesAsync(CreateFlags.None, typeof(Inventaire)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public InventaireDatabaseController()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public Task<Inventaire> GetInventaire()
        {
            lock (locker)
            {
                if (database.Table<Inventaire>().CountAsync().Result == 0)
                    return null;
                else
                    return database.Table<Inventaire>().FirstAsync();
            }
        }

        public int GetCountInventaireByNumImmo(string numimmo)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.CodeImmo != null && i.CodeImmo.ToLower().Contains(numimmo.ToLower())).CountAsync().Result;


            }
        }

        public int GetCountInventaireByNumImmoIsRead(string numimmo)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.CodeImmo != null && i.CodeImmo.ToLower().Contains(numimmo.ToLower()) && i.IsRead).CountAsync().Result;


            }
        }

        public int GetCountInventaireByNumImmoDescription(string keyword)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => (i.IsRead && i.CodeImmo != null && i.CodeImmo.ToLower().Contains(keyword.ToLower())) || (i.DescriptionPH != null && i.DescriptionPH.ToLower().Contains(keyword.ToLower()))).CountAsync().Result;


            }
        }

        public int GetCountInventaireByNumImmoDescriptionIsRead(string keyword)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => (i.IsRead && i.CodeImmo != null && i.CodeImmo.ToLower().Contains(keyword.ToLower())) || (i.DescriptionPH != null && i.DescriptionPH.ToLower().Contains(keyword.ToLower()))).CountAsync().Result;


            }
        }

        public int GetCountInventaireByDescriptionFilter(string Description)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.Description != null && i.Description.ToLower().Contains(Description.ToLower())).CountAsync().Result;


            }
        }
        public int GetCountInventaireByFamilleFilter(string FAMILLE)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.FAMILLE != null && i.FAMILLE.ToLower().Contains(FAMILLE.ToLower())).CountAsync().Result;


            }
        }
        public int GetCountInventaireBySFamilleFilter(string SFAMILLE)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.SFAMILLE != null && i.SFAMILLE.ToLower().Contains(SFAMILLE.ToLower())).CountAsync().Result;


            }
        }
        public int GetCountInventaireByMARQUEFilter(string MARQUE)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.MARQUE != null && i.MARQUE.ToLower().Contains(MARQUE.ToLower())).CountAsync().Result;


            }
        }
        public int GetCountInventaireByMODELEFilter(string MODELE)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.MODELE != null && i.MODELE.ToLower().Contains(MODELE.ToLower())).CountAsync().Result;


            }
        }
        public int GetCountInventaireBySITEFilter(string SITE)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.SITE.ToLower().Contains(SITE.ToLower())).CountAsync().Result;


            }
        }

        public int GetCountInventaireByEMPLFilter(string EMPL)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.EMPL != null && i.EMPL.ToLower().Contains(EMPL.ToLower())).CountAsync().Result;
            }
        }


        public int GetCountInventaireByBUREAUFilter(string BUREAU)
        {

            lock (locker)
            {
                return database.Table<Models.Inventaire>().Where(i => i.BUREAU != null && BUREAU != null && i.BUREAU.ToLower().Contains(BUREAU.ToLower())).CountAsync().Result;
            }
        }
        public Task<List<Inventaire>> GetAllInventaires()
        {
            return database.Table<Inventaire>().ToListAsync();
        }

        public Task<List<Inventaire>> GetAllInventairesByIsRead()
        {
            return database.Table<Inventaire>().Where(i => i.IsRead == true).ToListAsync();
        }

        public Task<List<Inventaire>> GetAllInventairesByDiscriptionDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT Description from Inventaire where Description is not null and Description <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
            }
        }

        public Task<List<Inventaire>> GetAllInventairesByFamilleDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT FAMILLE from Inventaire where FAMILLE is not null and FAMILLE <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
            }
        }

        public Task<List<Inventaire>> GetAllInventairesBySFamilleDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT SFAMILLE from Inventaire where SFAMILLE is not null and SFAMILLE <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
            }
        }
        public Task<List<Inventaire>> GetAllInventairesByMarqueDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT MARQUE from Inventaire where MARQUE is not null and MARQUE <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
            }
        }

        public Task<List<Inventaire>> GetAllInventairesByModeleDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT MODELE from Inventaire where MODELE is not null and MODELE <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
            }
        }

        public Task<List<Inventaire>> GetAllInventairesBySiteDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select DISTINCT SITE from Inventaire where SITE is not null and SITE <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        public Task<List<Inventaire>> GetAllInventairesByEmplDistinct(string site)
        {

            lock (locker)
            {
                string InventaireBySite = ("select distinct EMPL from Inventaire where SITE = ?");
                return database.QueryAsync<Inventaire>(InventaireBySite, site);
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        public Task<List<Inventaire>> GetAllInventairesEmplDistinct()
        {

            lock (locker)
            {
                string InventaireBySite = ("select distinct EMPL from Inventaire where EMPL is not null and EMPL <> ''");
                return database.QueryAsync<Inventaire>(InventaireBySite);
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }
        public Task<List<Inventaire>> GetAllInventairesByBureauDistinct(string BUREAU)
        {

            lock (locker)
            {
                string InventaireByEMPL = ("select distinct BUREAU from Inventaire where EMPL = ?");
                return database.QueryAsync<Inventaire>(InventaireByEMPL, BUREAU);
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        public Task<List<Inventaire>> GetAllInventairesBureauDistinct()
        {

            lock (locker)
            {
                string InventaireByEMPL = ("select distinct BUREAU from Inventaire where BUREAU is not null and BUREAU <> ''");
                return database.QueryAsync<Inventaire>(InventaireByEMPL);
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        public Task<List<Inventaire>> GetCodeImmoInventaires(string codeimmo)
        {
            string AllCodeImmoInventaire = ("select distinct CodeImmo from Inventaire");
            return database.QueryAsync<Inventaire>(AllCodeImmoInventaire, codeimmo);
        }
        public Task<Inventaire> GetInventaireById(string id)
        {

            lock (locker)
            {
                if (database.Table<Inventaire>().Where(i => i.CodeImmo == id).CountAsync().Result == 0)
                    return null;
                else
                    return database.Table<Inventaire>()
                            .Where(i => i.CodeImmo == id)
                            .FirstAsync();
            }
        }

        public int GetCountAllInventaires()
        {

            lock (locker)
            {
                return database.Table<Inventaire>().CountAsync().Result;

            }
        }

        public int GetCountAllInventairesByIsRead()
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.IsRead == true).CountAsync().Result;

            }
        }
        public int GetCountInventaire(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.CodeImmo == id).CountAsync().Result;

            }
        }

        public int GetCountInventaireByIsRead(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.CodeImmo == id && i.IsRead == true).CountAsync().Result;

            }
        }
        public int GetCountInventaireByDescription(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => !String.IsNullOrEmpty(i.Description) && i.Description == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireByFamille(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => !String.IsNullOrEmpty(i.FAMILLE) && i.FAMILLE == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireBySFamille(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => !String.IsNullOrEmpty(i.SFAMILLE) && i.SFAMILLE == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireByMarque(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => !String.IsNullOrEmpty(i.MARQUE) && i.MARQUE == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireByModele(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => !String.IsNullOrEmpty(i.MODELE) && i.MODELE == id).CountAsync().Result;

            }
        }
        public Task<List<Models.Inventaire>> GetInventaireByEmplCodeUB(string site, string empl, string bureau)
        {
            string AllInventaireBySiteEmpl = ("select distinct * from Inventaire Where SITE = ? and EMPL = ? and BUREAU = ? and IsRead = ?");
            return database.QueryAsync<Models.Inventaire>(AllInventaireBySiteEmpl, site, empl, bureau, 0);
        }
        public int GetCountInventaireBySite(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.SITE == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireByBureau(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.BUREAU == id).CountAsync().Result;

            }
        }
        public int GetCountInventaireByEmpl(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.EMPL == id).CountAsync().Result;

            }
        }

        public Task<Inventaire> GetInventaireByDescription(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.Description == id).FirstAsync();

            }
        }

        public Task<Inventaire> GetInventaireByFamille(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.FAMILLE == id).FirstAsync();

            }
        }

        public Task<Inventaire> GetInventaireBySFamille(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.SFAMILLE == id).FirstAsync();

            }
        }

        public Task<Inventaire> GetInventaireByMarque(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.MARQUE == id).FirstAsync();

            }
        }
        public Task<Inventaire> GetInventaireByModele(string id)
        {

            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.MODELE == id).FirstAsync();

            }
        }
        public Inventaire GetInventaireBySite(string id)
        {

            lock (locker)
            {
                string InventaireBySite = ("select distinct * from Inventaire where SITE = ?");
                return database.QueryAsync<Inventaire>(InventaireBySite, id).Result.First();
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        public Inventaire GetInventaireByEMPL(string id)
        {

            lock (locker)
            {
                string InventaireByEMPL = ("select distinct * from Inventaire where EMPL = ? ");
                return database.QueryAsync<Inventaire>(InventaireByEMPL, id).Result.First();
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        //public Inventaire GetInventaireByEmpl(string id)
        //{

        //    lock (locker)
        //    {
        //        string InventaireByEmpl = ("select distinct EMPLy from Inventaire where EMPL = ?");
        //        return database.QueryAsync<Inventaire>(InventaireByEmpl, id).Result.First();
        //        // return database.Table<Inventaire>().Where(i => i.EMPL == id).FirstAsync();

        //    }
        //}
        public Inventaire GetInventaireByBureau(string id)
        {

            lock (locker)
            {
                string InventaireByBUREAU = ("select distinct * from Inventaire where BUREAU = ?");
                return database.QueryAsync<Inventaire>(InventaireByBUREAU, id).Result.First();
                //  return database.Table<Inventaire>().Where(i => i.SITE == id).FirstAsync();

            }
        }

        //public Task<int> SaveInventaire(Inventaire Inventaire)
        //{
        //    lock (locker)
        //    {
        //        if (GetInventaireById(Inventaire.CodeImmo) != null)
        //        {
        //            return database.UpdateAsync(Inventaire);
        //        }
        //        else
        //            return database.InsertAsync(Inventaire);
        //    }
        //}
        //public Task<int> SaveInventaire(Models.Inventaire Inventaire)
        //{
        //    lock (locker)
        //    {
        //        if (GetInventaireById(Inventaire.CodeImmo) != null)
        //        {
        //            return database.UpdateAsync(Inventaire);
        //        }
        //        else
        //            return database.InsertAsync(Inventaire);
        //    }
        //}
        public Task<int> SaveInventaire(Models.Inventaire Inventaire)
        {
            lock (locker)
            {

              
                if (GetInventaireById(Inventaire.CodeImmo) != null)
                {
                    return database.UpdateAsync(Inventaire);
                }
                else
                    return database.InsertAsync(Inventaire);
            }
        }



        public Task<int> UpdateInventaire(Inventaire Inventaire)
        {
            lock (locker)
            {

                return database.UpdateAsync(Inventaire);

            }
        }
        public Task<int> DeleteInventaire(string code)
        {
            lock (locker)
            {
                return database.DeleteAsync<Inventaire>(code);
            }
        }

        public Task<int> DeleteAllInventairesByIsRead()
        {
            lock (locker)
            {
                return database.Table<Inventaire>().Where(i => i.IsRead == true).DeleteAsync();
            }
        }
        public Task<int> DeleteAllInventaires()
        {
            lock (locker)
            {
                return database.DeleteAllAsync<Inventaire>();
            }
        }
    }
}
