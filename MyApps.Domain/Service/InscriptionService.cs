using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyApps.Domain.Service
{
    public class InscriptionService 
    {
        public static void Create(ModuleInscription inscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.ModuleInscriptions.Add(inscription);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Find(id);
                db.ModuleInscriptions.Remove(inscription);
                db.SaveChanges();
            }
        }

        public static List<ModuleInscription> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Include(a => a.Participant).Include(a => a.SiteModule);
                return inscription.ToList();
            }
        }

        public static ModuleInscription GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Find(id);
                return inscription;
            }
        }

        public static void Update(ModuleInscription inscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(inscription).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static string GetNomModule(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdSiteModule = db.SiteModules.Find(IdSiteModule);
                var idmodule= GetIdSiteModule.IdModule;
                var nomModule = db.Modules.Find(idmodule);
                return nomModule.NomModule; 


            }
        }
        public static string GetNomParticipant(int? IdParticipant) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Participants.Find(IdParticipant);
                return GetName.NomParticipant;   

            }
        }
        public static long? GetIdNational(int? IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdNational = db.Participants.Find(IdParticipant);
                return GetIdNational.IdNational; 

            }
        }
        public static List<ModuleInscription> GetListModulePerParticipant(int IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdParticipant == IdParticipant).ToList();
                return moduleParticipant;
            }
        }
        public static List<ModuleInscription> GetListParticipantPerModule(int IdSiteModule) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                //var siteplanning = db.SiteModules.Find(IdSiteModule);
               // var idSiteModule = siteplanning.IdSiteModule;
                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule && a.EstSurListeAttente==false).ToList();
                return moduleParticipant;
            }
        }
        public static List<ModuleInscription> GetListAttenteParticipantPerModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                //var module =db.SiteModules.Find(IdSiteModule);

                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule && a.EstSurListeAttente ==true).ToList();
                return moduleParticipant;
            }
        }
        public static int GetNbreParticipantParModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
              
                var TotalParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule);
                 int total = TotalParticipant.Where(a => a.EstSurListeAttente == true).Count();
               
                return total;
            }
        }
    }
}
