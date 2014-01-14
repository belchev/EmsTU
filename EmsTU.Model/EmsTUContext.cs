using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EmsTU.Model.Models;

namespace EmsTU.Model.Models
{
    public partial class EmsTUContext : DbContext
    {
        static EmsTUContext()
        {
            Database.SetInitializer<EmsTUContext>(null);
        }

        public EmsTUContext()
            : base("Name=EmsTUContext")
        {
        }

        public DbSet<ActionErrorLog> ActionErrorLogs { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<AlbumPhoto> AlbumPhotos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<BuildingRequest> BuildingRequests { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Nom> Noms { get; set; }
        public DbSet<NomType> NomTypes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionErrorLogMap());
            modelBuilder.Configurations.Add(new ActionLogMap());
            modelBuilder.Configurations.Add(new AlbumPhotoMap());
            modelBuilder.Configurations.Add(new AlbumMap());
            modelBuilder.Configurations.Add(new BuildingRequestMap());
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new MenuCategoryMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new NomMap());
            modelBuilder.Configurations.Add(new NomTypeMap());
            modelBuilder.Configurations.Add(new OfferMap());
            modelBuilder.Configurations.Add(new RatingMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new VisitorMap());
        }
    }
}
