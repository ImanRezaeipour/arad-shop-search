using Common.Domain.Model;
using Framework.Core;
using Framework.Core.ApplicationException;
using Framework.Persistance.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Common.Infrastructure.Persistance.EF
{
    //[DbConfigurationType(typeof(ModelConfiguration))]
    public class CommonDbContext : DbContext, IUnitOfWork
    {
        #region =================== Private Variable ======================

        private readonly Dictionary<EntityState, Action<BaseModel>> SetAuditFieldActionDic = new Dictionary<EntityState, Action<BaseModel>>();

        #endregion =================== Private Variable ======================

        #region =================== Public Property =======================

        public UserTicket UserLogin { get; set; }

        #endregion =================== Public Property =======================

        #region =================== Public Constructor ====================

        public CommonDbContext() : base("name=DBConnection")
        {
            Database.SetInitializer<CommonDbContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            //SetAuditFieldActionDic.Add(EntityState.Added, SetAuditAdded);
            //SetAuditFieldActionDic.Add(EntityState.Modified, SetAuditModified);
        }

        #endregion =================== Public Constructor ====================

        #region =================== Private Methods =======================

        IDbSet<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        //private void SetAuditAdded(BaseModel entity)
        //{
        //    var auditDate = DateTime.Now;
        //    if (!UserLogin.IsAuthenticated)
        //        throw new ViewableException("User Not Authenticated.");
        //    entity.CreatedDate = auditDate;
        //    entity.ModifiedDate = auditDate;
        //    entity.CreatedBy = UserLogin.Id;
        //    entity.ModeifiedById = UserLogin.Id;
        //}

        //private void SetAuditModified(BaseModel entity)
        //{
        //    var auditDate = DateTime.Now;
        //    entity.ModifiedDate = auditDate;
        //    entity.ModeifiedById = UserLogin.Id;
        //}

        //private void UpdateAuditFields()
        //{
        //    foreach (var entry in this.ChangeTracker.Entries<BaseModel>())
        //    {
        //        // Note: You must add a reference to assembly : System.Data.Entity
        //        if (SetAuditFieldActionDic.ContainsKey(entry.State))
        //        {
        //            SetAuditFieldActionDic[entry.State](entry.Entity);
        //        }
        //    }
        //}

        #endregion =================== Private Methods =======================

        #region ================== Protected Methods ======================

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(SchemaContexts.ERP);
     
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);

            //#region CustomMappings

            //modelBuilder.Entity<User>().ToTable("User", SchemaContexts.UserManagment);
            //modelBuilder.Entity<Role>().ToTable("Role", SchemaContexts.UserManagment);
            //modelBuilder.Entity<UserRole>().ToTable("UserRoles", SchemaContexts.UserManagment);
            //modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaContexts.UserManagment);
            //modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaContexts.UserManagment)
            //    .Property(a => a.LoginProvider)
            //    .HasMaxLength(128);

            //modelBuilder.Entity<UserLogin>().Property(a => a.ProviderKey)
            //    .HasMaxLength(128);

            //#endregion CustomMappings

        }

        #endregion ================== Protected Methods ======================

        #region =================== Public Methods ========================

        public override int SaveChanges()
        {
#if DEBUG
            if (UserLogin != null)
            {
                //UpdateAuditFields();
            }
#else
                //UpdateAuditFields();

#endif
            return base.SaveChanges();
        }

        public static CommonDbContext Create()
        {
            return new CommonDbContext();
        }

        #endregion =================== Public Methods ========================
    }
}